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
    const string repository_path_format = @"\{0}\{1}_{2}";

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

    Dictionary<string, string> contracts = new Dictionary<string, string>();
    Dictionary<string, string>.Enumerator contractsKeyEnumerator;
    XDocument invoices = new XDocument();
    int[][] missingInvoiceIndexes = null;

    bool groupByContracts = false;
    int rowIndex = 0;
    int rowCount = 0;

    public Form1()
    {
      InitializeComponent();
      
      btnStop.Enabled = false;
#if !DEBUG
      tabControl1.TabPages.Remove(tabPage2);
#endif
      txtPath.Text = filePath;

      chkGroupByContracts.Checked = groupByContracts;
      
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

    private void Start()
    {
      btnStart.Enabled = false;
      btnStop.Enabled = true;
      btnBrowse.Enabled = false;
      txtUserName.ReadOnly = true;
      txtPassword.ReadOnly = true;
      chkGroupByContracts.Enabled = false;

      contracts.Clear();

      lstContracts.Items.Clear();
      lstLog.Items.Clear();

      BuildRepository(txtPath.Text);

      groupByContracts = chkGroupByContracts.Checked;

      webBrowser1.Navigate(dijnet_hu);
    }

    private void Stop()
    {
      webBrowser1.Navigate("");
      webBrowser2.Navigate("");

      btnBrowse.Enabled = true;
      txtUserName.ReadOnly = false;
      txtPassword.ReadOnly = false;
      chkGroupByContracts.Enabled = true;
      btnStop.Enabled = false;
      btnStart.Enabled = true;
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
      Start();
    }

    private void btnStop_Click(object sender, EventArgs e)
    {
      webBrowser1.Navigate(string.Format(url_format, webBrowser1.Url.Scheme, webBrowser1.Url.DnsSafeHost, logout_path, string.Empty));
    }

    private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
    {
      if (e.Url.AbsolutePath.Equals(login_path))
      {
        HtmlElement loginform = webBrowser1.Document.Forms["loginform"];
        if (loginform != null)
        {
          HtmlElementCollection inputs = loginform.GetElementsByTagName("input");

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
        else
        {
          AddLog("Error: 'loginform' form not found!'");
        }
      }
      else if (e.Url.AbsolutePath.Equals(login_check_password_path))
      {
        webBrowser1.Navigate(string.Format(url_format, e.Url.Scheme, e.Url.DnsSafeHost, szamla_search_path, string.Empty));
      }
      else if (e.Url.AbsolutePath.Equals(szamla_search_path))
      {
        if (webBrowser1.Document.Forms.Count > 0)
        {
          contracts.Clear();
          lstContracts.Items.Clear();

          HtmlElement form = webBrowser1.Document.Forms[0];

          foreach (HtmlElement select in form.GetElementsByTagName("select"))
          {
            switch (select.Name)
            {
              case "regszolgid":
                foreach (HtmlElement option in select.Children)
                {
                  if (option.InnerText != null)
                  {
                    contracts.Add(option.GetAttribute("value"), option.InnerText);
                    lstContracts.Items.Add(option.InnerText);
                  }
                }
                contractsKeyEnumerator = contracts.GetEnumerator();
                break;
            }
          }

          if (contracts.Count == 0 || (groupByContracts && !NextService()))
          {
            Stop();
          }
          else
          {
            RunQuery();
          }
        }
        else
        {
          AddLog("Error: no forms found!");
        }
      }
      else if (e.Url.AbsolutePath.Equals(szamla_search_submit_path))
      {
        HtmlElement content_div = webBrowser1.Document.GetElementById("content");
        if (content_div != null)
        {
          HtmlElementCollection content_div_tables = content_div.GetElementsByTagName("table");
          if (content_div_tables != null)
          {
            HtmlElement listframetable_outer = null;
            string listframetable_id = null;

            foreach (HtmlElement content_div_table in content_div_tables)
            {
              if (content_div_table.Id != null && content_div_table.Id.StartsWith("listframetable_"))
              {
                listframetable_outer = content_div_table;
                listframetable_id = content_div_table.Id;
                break;
              }
            }

            if (listframetable_outer != null)
            {
              HtmlElement listframetable_outer_next_div = listframetable_outer.NextSibling;
              if (listframetable_outer_next_div != null && listframetable_outer_next_div.TagName.ToLower() == "div")
              {
                HtmlElement listframetable = listframetable_outer_next_div.FirstChild;
                if (listframetable != null && listframetable.Id != null && listframetable.Id.Equals(listframetable_id))
                {
                  HtmlElementCollection rows = listframetable.GetElementsByTagName("tr");

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
                        Dictionary<string, string> query_parts =
                          href.Query.TrimStart('?').Split('&').Select(x => x.Split('=')).ToDictionary(x => x[0], x => (x.Length > 1) ? x[1] : "");
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

                    missingInvoiceIndexes =
                      invoices.Root.Elements("Invoice")
                        .Select(
                          (element, index) =>
                            new { Element = element, Index = index }
                        )
                        .Where(
                          x =>
                            x.Element.Attribute("index") != null &&
                            (x.Element.Attribute("fajlok") == null || x.Element.Attribute("fajlok").Value == "0")
                        )
                        .Select(
                          x =>
                            new int[] { x.Index, Convert.ToInt32(x.Element.Attribute("index").Value) }
                        )
                        .ToArray();

                    rowCount = missingInvoiceIndexes.Length;

                    if (groupByContracts)
                    {
                      AddLog(string.Format("{0}: item(s) of {1} invoice(s) will need to be updated/downloaded!", contractsKeyEnumerator.Current.Value, rowCount));
                    }
                    else
                    {
                      AddLog(string.Format("Item(s) of {0} invoice(s) will need to be updated/downloaded!", rowCount));
                    }
                  }
                  catch (Exception ex)
                  {
                    AddLog("Error: " + ex.Message);
                  }

                  if (rowCount > 0)
                  {
                    webBrowser2.Navigate(string.Format(url_format, e.Url.Scheme, e.Url.DnsSafeHost, szamla_select_path, string.Format(szamla_select_query_format, missingInvoiceIndexes[rowIndex++][1])));
                  }
                  else if (!groupByContracts || !NextService())
                  {
                    webBrowser1.Navigate(string.Format(url_format, e.Url.Scheme, e.Url.DnsSafeHost, logout_path, string.Empty));
                  }
                }
                else
                {
                  AddLog("Error: first 'listframetable_*' table's next div sibling's child is not a table or its id is other than the first 'listframetable_*' table's id!");
                }
              }
              else
              {
                AddLog("Error: next sibling is not a div from 'listframetable_*' found in 'content' tables!");
              }
            }
            else
            {
              AddLog("Error: no 'listframetable_*' found in 'content' tables!");
            }
          }
          else
          {
            AddLog("Error: no tables in 'content' div!");
          }
        }
        else
        {
          AddLog("Error: 'content' div not found!");
        }
      }
      else if (e.Url.AbsolutePath.Equals(logout_path))
      {
        Stop();
      }
    }

    private bool NextService()
    {
      if (contractsKeyEnumerator.MoveNext() && webBrowser1.Document.Forms.Count > 0)
      {
        HtmlElement form = webBrowser1.Document.Forms[0];

        bool contractSelected = false;
        foreach (HtmlElement select in form.GetElementsByTagName("select"))
        {
          switch (select.Name)
          {
            case "regszolgid":
              select.SetAttribute("value", contractsKeyEnumerator.Current.Key);
              contractSelected = true;
              break;
          }
        }

        if (contractSelected)
        {
          foreach (HtmlElement input in form.GetElementsByTagName("input"))
          {
            if (input.GetAttribute("value").Equals("Keresés"))
            {
              input.InvokeMember("click");
              return true;
            }
          }
        }

        return false;
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
        HtmlElement tab_szamla_letolt = webBrowser2.Document.GetElementById("tab_szamla_letolt");
        if (tab_szamla_letolt != null)
        {
          HtmlElementCollection anchors = (tab_szamla_letolt.FirstChild != null) ? tab_szamla_letolt.FirstChild.GetElementsByTagName("a") : null;

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
                  if (groupByContracts)
                  {
                    path =
                      txtPath.Text.TrimEnd(@"\".ToCharArray())
                    + invoices.Root.Elements("Invoice")
                      .Select(
                        x =>
                          string.Format(
                            repository_path_format
                          , contractsKeyEnumerator.Current.Value
                          , x.Attribute("bemdat").Value.Replace(".", "")
                          , Uri.EscapeDataString(x.Attribute("szamlaszam").Value)
                          )
                      )
                      .ElementAt(missingInvoiceIndexes[rowIndex - 1][0]);
                  }
                  else
                  {
                    path =
                      txtPath.Text.TrimEnd(@"\".ToCharArray())
                    + invoices.Root.Elements("Invoice")
                      .Select(
                        x =>
                          string.Format(
                            repository_path_format
                          , contracts
                            .Where(kv => kv.Value.StartsWith(x.Attribute("ugyfelazon").Value + " ("))
                            .Select(kv => kv.Value)
                            .FirstOrDefault()
                          , x.Attribute("bemdat").Value.Replace(".", "")
                          , Uri.EscapeDataString(x.Attribute("szamlaszam").Value)
                          )
                      )
                      .ElementAt(missingInvoiceIndexes[rowIndex - 1][0]);
                  }

                  Directory.CreateDirectory(path);

                  string filename = path + @"\" + filename_attr[1];

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
                else
                {
                  AddLog("Error: second attribute is not a filename in 'Content-Disposition' header!");
                }
              }
              else
              {
                AddLog("Error: no 'Content-Disposition' header in http response or it's not an attachment with at least another attribute!");
              }
            }
          }

          webBrowser2.Navigate(string.Format(url_format, e.Url.Scheme, e.Url.DnsSafeHost, szamla_list_path, string.Empty));
        }
        else
        {
          AddLog("Error: 'tab_szamla_letolt' table not found!");
        }
      }
      else if (e.Url.AbsolutePath.Equals(szamla_list_path))
      {
        if (rowIndex < rowCount)
        {
          webBrowser2.Navigate(string.Format(url_format, e.Url.Scheme, e.Url.DnsSafeHost, szamla_select_path, string.Format(szamla_select_query_format, missingInvoiceIndexes[rowIndex++][1])));
        }
        else if (!groupByContracts || !NextService())
        {
          webBrowser1.Navigate(string.Format(url_format, e.Url.Scheme, e.Url.DnsSafeHost, logout_path, string.Empty));
        }
      }
    }
  }
}
