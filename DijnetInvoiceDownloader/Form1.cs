using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DijnetInvoiceDownloader
{
  public partial class Form1 : Form
  {
    [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern bool InternetGetCookieEx(string pchURL, string pchCookieName, StringBuilder pchCookieData, ref uint pcchCookieData, int dwFlags, IntPtr lpReserved);
    const int INTERNET_COOKIE_HTTPONLY = 0x00002000;

    public static string GetGlobalCookies(string uri)
    {
      uint datasize = 1024;
      StringBuilder cookieData = new StringBuilder((int)datasize);
      if (InternetGetCookieEx(uri, null, cookieData, ref datasize, INTERNET_COOKIE_HTTPONLY, IntPtr.Zero)
          && cookieData.Length > 0)
      {
        return cookieData.ToString();
      }
      else
      {
        return null;
      }
    }

    const string dijnet_hu = "dijnet.hu";
    const string url_format = "{0}://{1}{2}{3}";

    const string login_path = "/ekonto/control/login";
    const string login_check_password_path = "/ekonto/login/login_check_password";
    const string szamla_search_path = "/ekonto/control/szamla_search";
    const string szamla_search_submit_path = "/ekonto/control/szamla_search_submit";
    const string szamla_select_path = "/ekonto/control/szamla_select";
    const string szamla_select_query_format = "?vfw_coll=szamla_list&vfw_coll_index=0&vfw_rowid={0}&vfw_colid=szamlaszam%7Cegyenleginfo_0";
    const string szamla_letolt_path = "/ekonto/control/szamla_letolt";
    const string szamla_list_path = "/ekonto/control/szamla_list";
    const string logout_path = "/ekonto/control/logout";

    const string filePath = @".";

    Dictionary<string, string> services = new Dictionary<string, string>();
    Dictionary<string, string>.Enumerator servicesKeyEnumerator;
    XDocument invoices = new XDocument();
    int[][] missingInvoiceIndexes = null;

    bool filterByService = false;
    int rowIndex = 0;
    int rowCount = 0;

    public Form1()
    {
      InitializeComponent();
      txtPath.Text = filePath;
      BuildRepository(txtPath.Text);
    }

    private bool BuildRepository(string path)
    {
      invoices.RemoveNodes();
      invoices.Add(new XElement("Invoices"));

      try
      {
        DirectoryInfo repo = new DirectoryInfo(path);

        foreach (DirectoryInfo di in repo.EnumerateDirectories())
        {
          foreach (DirectoryInfo idi in di.EnumerateDirectories())
          {
            //string invoice_key = fi.Name.Split("_.".ToCharArray()).Skip(1).Reverse().Skip(1).Reverse().Select((v, i) => new { Index = i, Value = v }).Where(p => !p.Value.All(char.IsLetter)).Select(p => p.Value).Aggregate((x, y) => x + "_" + y);
            string invoice_key = Uri.UnescapeDataString(idi.Name);
            if (invoice_key.Length > 8 && invoice_key[8] == '_')
            {
              string bemdat = string.Format("{0}.{1}.{2}", invoice_key.Substring(0, 4), invoice_key.Substring(4, 2), invoice_key.Substring(6, 2));
              string szamlaszam = invoice_key.Substring(9, invoice_key.Length - 9);

              XElement invoice;
              invoices.Root.Add(invoice = new XElement("Invoice"));

              invoice.Add(new XAttribute("szamlaszam", szamlaszam));
              invoice.Add(new XAttribute("bemdat", bemdat));

              foreach (FileInfo fi in idi.EnumerateFiles())
              {
                invoice.Add(new XElement("File", fi.Name));
              }

              int file_count = invoice.Elements().Count();
              invoice.Add(new XAttribute("fajlok", file_count));
            }
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error selecting repository!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }

      return true;
    }

    private void AddLog(string info)
    {
      lstLog.Items.Add(info);

      int visibleItemCount = lstLog.﻿DisplayRectangle.Height / lstLog.ItemHeight;

      if (visibleItemCount < lstLog.Items.Count - lstLog.TopIndex)
      {
        lstLog.TopIndex = lstLog.Items.Count - visibleItemCount + 1;
      }
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
      bool repoBuilt = false;

      do
      {
        folderBrowserDialog1.SelectedPath = new DirectoryInfo(txtPath.Text).FullName;

        if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
        {
          break;
        }
      }
      while (!(repoBuilt = BuildRepository(folderBrowserDialog1.SelectedPath)));

      if (repoBuilt)
      {
        txtPath.Text = folderBrowserDialog1.SelectedPath;
      }
    }

    private void btnStart_Click(object sender, EventArgs e)
    {
      btnStart.Enabled = false;

      services.Clear();

      lstServices.Items.Clear();
      lstLog.Items.Clear();

      BuildRepository(txtPath.Text);

      webBrowser1.Navigate(dijnet_hu);
    }

    private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
    {
      if (e.Url.AbsolutePath.Equals(login_path))
      {
        HtmlElementCollection inputs = webBrowser1.Document.Forms["loginform"].GetElementsByTagName("input");

        foreach (HtmlElement i in inputs)
        {
          switch (i.Name)
          {
            case "username":
              i.InnerText = txtUserName.Text;
              break;

            case "password":
              i.InnerText = txtPassword.Text;
              break;

            default:
              if (i.GetAttribute("value").Equals("Belépek"))
              {
                i.InvokeMember("click");
              }
              break;
          }
        }
      }
      else if (e.Url.AbsolutePath.Equals(login_check_password_path))
      {
        webBrowser1.Navigate(string.Format(url_format, e.Url.Scheme, e.Url.DnsSafeHost, szamla_search_path, string.Empty));
      }
      else if (e.Url.AbsolutePath.Equals(szamla_search_path))
      {
        HtmlElement form = webBrowser1.Document.Forms[0];
        HtmlElementCollection selects = form.GetElementsByTagName("select");

        foreach (HtmlElement s in selects)
        {
          switch (s.Name)
          {
            case "regszolgid":
              foreach (HtmlElement o in s.Children)
              {
                if (o.InnerText != null)
                {
                  services.Add(o.GetAttribute("value"), o.InnerText);
                  lstServices.Items.Add(o.InnerText);
                }
              }
              servicesKeyEnumerator = services.GetEnumerator();
              break;
          }
        }

        if (services.Count == 0 || (filterByService && !NextService()))
        {
          btnStart.Enabled = true;
        }
        else
        {
          RunQuery();
        }
      }
      else if (e.Url.AbsolutePath.Equals(szamla_search_submit_path))
      {        
        HtmlElement listframetable_N1015_scroll_outer = webBrowser1.Document.GetElementById("listframetable_N1015A_scroll");
        if (listframetable_N1015_scroll_outer == null)
        {
          listframetable_N1015_scroll_outer = webBrowser1.Document.GetElementById("listframetable_N1015B_scroll");
          if (listframetable_N1015_scroll_outer == null)
          {
            listframetable_N1015_scroll_outer = webBrowser1.Document.GetElementById("listframetable_N1015C_scroll");
          }
        }
        if (listframetable_N1015_scroll_outer != null)
        {
          HtmlElement listframetable_N1015_scroll_outer_next_div = listframetable_N1015_scroll_outer.NextSibling;
          if (listframetable_N1015_scroll_outer_next_div != null)
          {
            HtmlElement listframetable_N1015_scroll = listframetable_N1015_scroll_outer_next_div.FirstChild;
            if (listframetable_N1015_scroll != null)
            {
              HtmlElementCollection rows = listframetable_N1015_scroll.GetElementsByTagName("tr");

              rowCount = 0;
              rowIndex = 0;

              if (invoices.Root != null && invoices.Root.Name != "Invoices")
              {
                invoices.RemoveNodes();
              }
              if (invoices.Root == null)
              {
                invoices.Add(new XElement("Invoices"));
              }

              invoices.Root.Elements("Invoice").Attributes("index").Remove();

              try
              {
                int i = 0;
                foreach (HtmlElement r in rows)
                {
                  Dictionary<string, string> row = new Dictionary<string, string>();
                  foreach (HtmlElement anchor in r.GetElementsByTagName("a"))
                  {
                    Uri href = new Uri(anchor.GetAttribute("href"));
                    Dictionary<string, string> query_parts = href.Query.TrimStart('?').Split('&').Select(x => x.Split('=')).ToDictionary(x => x[0], x => (x.Length > 1) ? x[1] : "");
                    row.Add(Uri.UnescapeDataString(query_parts["vfw_colid"]).Split('|')[0].Trim(), anchor.InnerText.Trim());
                  }

                  XElement xrow = invoices.Root.Elements("Invoice").Where(x => x.Attribute("szamlaszam").Value == row["szamlaszam"]).FirstOrDefault();
                  if (xrow == null)
                  {
                    invoices.Root.Add(xrow = new XElement("Invoice"));
                  }

                  Dictionary<string, string>.Enumerator columnEnum = row.GetEnumerator();
                  while (columnEnum.MoveNext())
                  {
                    XAttribute a;
                    if ((a = xrow.Attribute(columnEnum.Current.Key)) == null)
                    {
                      xrow.Add(a = new XAttribute(columnEnum.Current.Key, columnEnum.Current.Value));
                    }
                    else
                    {
                      a.SetValue(columnEnum.Current.Value);
                    }
                  }

                  xrow.Add(new XAttribute("index", i++));
                }

                missingInvoiceIndexes = invoices.Root.Elements("Invoice").Select((element, index) => new { Element = element, Index = index }).Where(x => x.Element.Attribute("fajlok") == null || x.Element.Attribute("fajlok").Value == "0").Select(x => new int[] { x.Index, Convert.ToInt32(x.Element.Attribute("index").Value) } ).ToArray();
                rowCount = missingInvoiceIndexes.Length;
                AddLog(string.Format("{0} item(s) will need to be updated!", rowCount));
              }
              catch (Exception ex)
              {
                AddLog("Hiba: " + ex.Message);
              }

              if (rowCount > 0)
              {
                webBrowser2.Navigate(string.Format(url_format, e.Url.Scheme, e.Url.DnsSafeHost, szamla_select_path, string.Format(szamla_select_query_format, missingInvoiceIndexes[rowIndex++][1])));
              }
              else if (!filterByService || !NextService())
              {
                webBrowser1.Navigate(string.Format(url_format, e.Url.Scheme, e.Url.DnsSafeHost, logout_path, string.Empty));
              }
            }
          }
        }
      }
      else if (e.Url.AbsolutePath.Equals(logout_path))
      {
        btnStart.Enabled = true;
      }
    }

    private bool NextService()
    {
      if (servicesKeyEnumerator.MoveNext())
      {
        HtmlElement form = webBrowser1.Document.Forms[0];

        HtmlElementCollection selects = form.GetElementsByTagName("select");

        foreach (HtmlElement s in selects)
        {
          switch (s.Name)
          {
            case "regszolgid":
              s.SetAttribute("value", servicesKeyEnumerator.Current.Key);
              break;
          }
        }

        HtmlElementCollection inputs = form.GetElementsByTagName("input");

        foreach (HtmlElement i in inputs)
        {
          if (i.GetAttribute("value").Equals("Keresés"))
          {
            i.InvokeMember("click");
            break;
          }
        }

        return true;
      }
      else
      {
        return false;
      }
    }

    private bool RunQuery()
    {
      if (webBrowser1.Document.Forms.Count > 0)
      {
        foreach (HtmlElement i in webBrowser1.Document.Forms[0].GetElementsByTagName("input"))
        {
          if (i.GetAttribute("value").Equals("Keresés"))
          {
            i.InvokeMember("click");
            return true;
          }
        }
      }

      return false;
    }

    private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
    {
      if (e.Url.AbsolutePath.Equals(szamla_select_path))
      {
        webBrowser2.Navigate(string.Format(url_format, e.Url.Scheme, e.Url.DnsSafeHost, szamla_letolt_path, string.Empty));
      }
      else if (e.Url.AbsolutePath.Equals(szamla_letolt_path))
      {
        HtmlElementCollection anchors = webBrowser2.Document.GetElementById("tab_szamla_letolt").FirstChild.GetElementsByTagName("a");

        foreach (HtmlElement a in anchors)
        {
          Uri href = new Uri(a.GetAttribute("href"));

          if (href.DnsSafeHost.Equals(e.Url.DnsSafeHost))
          {
            WebClient wc = new WebClient();
            wc.Headers.Add(HttpRequestHeader.Cookie, GetGlobalCookies(webBrowser2.Document.Url.AbsoluteUri));

            byte[] fileData = wc.DownloadData(href);

            string[] content_disposition = wc.ResponseHeaders["Content-Disposition"].Split(';');
            if (content_disposition != null && content_disposition.Length > 1 && content_disposition[0] == "attachment")
            {
              string[] filename_attr = content_disposition[1].Split('=');
              if (filename_attr != null && filename_attr.Length > 1 && filename_attr[0] == "filename")
              {
                string path;
                if (filterByService)
                {
                  path =
                    txtPath.Text
                  + @"\"
                  + servicesKeyEnumerator.Current.Value
                  + @"\"
                  + invoices.Root.Elements("Invoice")
                    .Select(x => string.Format(@"{0}_{1}", x.Attribute("bemdat").Value.Replace(".", ""), Uri.EscapeDataString(x.Attribute("szamlaszam").Value)))
                    .ElementAt(missingInvoiceIndexes[rowIndex - 1][0]);
                }
                else
                {
                  path =
                    txtPath.Text
                  + @"\"
                  + invoices.Root.Elements("Invoice")
                    .Select(x => string.Format(@"{0}\{1}_{2}", services.Where(kv => kv.Value.StartsWith(x.Attribute("ugyfelazon").Value)).Select(kv => kv.Value).FirstOrDefault(), x.Attribute("bemdat").Value.Replace(".", ""), Uri.EscapeDataString(x.Attribute("szamlaszam").Value)))
                    .ElementAt(missingInvoiceIndexes[rowIndex - 1][0]);
                }
                path = path.Replace(@"\\", @"\");
                string filename = path + @"\" + filename_attr[1];

                Directory.CreateDirectory(path);

                try
                {
                  Stream file = new FileStream(filename, FileMode.Create);
                  try
                  {
                    file.Write(fileData, 0, fileData.Length);
                    file.Flush();
                    file.Close();

                    AddLog(filename);
                  }
                  finally
                  {
                    file.Dispose();
                  }
                }
                catch (IOException ex)
                {
                  AddLog("Error saving file: " + ex.Message);
                }                    
              }
            }
          }
        }

        webBrowser2.Navigate(string.Format(url_format, e.Url.Scheme, e.Url.DnsSafeHost, szamla_list_path, string.Empty));
      }
      else if (e.Url.AbsolutePath.Equals(szamla_list_path))
      {
        if (rowIndex < rowCount)
        {
          webBrowser2.Navigate(string.Format(url_format, e.Url.Scheme, e.Url.DnsSafeHost, szamla_select_path, string.Format(szamla_select_query_format, missingInvoiceIndexes[rowIndex++][0])));
        }
        else if (!filterByService || !NextService())
        {
          webBrowser1.Navigate(string.Format(url_format, e.Url.Scheme, e.Url.DnsSafeHost, logout_path, string.Empty));
        }
      }
    }
  }
}
