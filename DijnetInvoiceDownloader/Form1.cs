using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;
using System.Xml.Linq;

using HtmlAgilityPack;

namespace DijnetInvoiceDownloader
{
  public partial class Form1 : Form
  {
    const string dijnet_base_url = "https://www.dijnet.hu/ekonto/";
    const string repository_path_format = @"\{0}\{1}_{2}";
    const string add_query_item = "&{0}={1}";

    //const string xpath_users_login_name_in_header = "//div[@id='header_menu']//span[contains('{0}', text())]";
    const string xpath_users_login_name_in_header = "//nav[@id='main-menu']//p/strong[contains('{0}', text())]";
    //const string xpath_group_items = "//select[@name='{0}']/option[@value!='']";
    const string xpath_script_contrains_var_ropts = "//script[not(@src) and contains(., 'var ropts')]";
    const string xpath_last_listframetable_rows = "//div[@id='content']//table[starts-with(@id, 'listframetable_')][last()]/tbody/tr";
    const string xpath_div_tab_szamla_letolt = "//div[@id='tab_szamla_letolt']";

    const string main_path = "control/main";
    const string login_check_ajax_path = "login/login_check_ajax";
    const string login_check_ajax_post_data_format = "username={0}&password={1}";
    const string login_check_password_path = "login/login_check_password";
    const string login_check_password_post_data_format = "vfw_form=login_check_password&username={0}&password={1}";
    const string szamla_search_submit_path = "control/szamla_search_submit";
    const string szamla_search_submit_post_data = "vfw_form=szamla_search_submit&vfw_coll=szamla_search_params";
    const string szamla_search_path = "control/szamla_search";
    const string szamla_select_path = "control/szamla_select";
    const string szamla_select_query_format = "?vfw_coll=szamla_list&vfw_coll_index=0&vfw_rowid={0}&vfw_colid=szamlaszam%7Cegyenleginfo_0";
    const string szamla_letolt_path = "control/szamla_letolt";
    const string szamla_list_path = "control/szamla_list";
    const string logout_path = "control/logout";

    const string field_ugyfelazon = "ugyfelazon";
    const string field_szamlaszam = "szamlaszam";
    const string field_bemdat = "bemdat";

    const string tag_Invoices = "Invoices";
    const string tag_Invoice = "Invoice";
    const string tag_File = "File";
    const string tag_index = "index";
    const string tag_files = "files";

    const string filePath = @".";

    CookieContainer cookieJar = new CookieContainer();

    Dictionary<string, string> contracts = new Dictionary<string, string>();
    Dictionary<string, string> serviceProviders = new Dictionary<string, string>();
    XDocument invoices = new XDocument();
    int[][] missingInvoiceIndexes = null;

    bool stopped = false;
    GroupBy groupBy = GroupBy.Contracts;

    public Form1()
    {
      InitializeComponent();

      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

      stopped = false;

      btnStop.Enabled = false;

      txtPath.Text = filePath;

      switch (groupBy)
      {
        case GroupBy.None:
          radioButton1.Checked = true;
          break;

        case GroupBy.Contracts:
          radioButton2.Checked = true;
          break;

        case GroupBy.ServiceProviders:
          radioButton3.Checked = true;
          break;
      }

      BuildRepositoryIndex(txtPath.Text);
    }

    private bool BuildRepositoryIndex(string path)
    {
      invoices.RemoveNodes();
      invoices.Add(new XElement(tag_Invoices));

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
              invoices.Root.Add(invoice = new XElement(tag_Invoice));

              invoice.Add(new XAttribute(field_szamlaszam, szamlaszam));
              invoice.Add(new XAttribute(field_bemdat, bemdat));
              //int i = di.Name.IndexOf(" (");
              //invoice.Add(new XAttribute(field_ugyfelazon, (i >= 0) ? di.Name.Substring(0, i) : di.Name));

              foreach (FileInfo fi in idi.EnumerateFiles())
              {
                invoice.Add(new XElement(tag_File, fi.Name));
              }

              int file_count = invoice.Elements().Count();
              invoice.Add(new XAttribute(tag_files, file_count));
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

      int visibleItemCount = lstLog.DisplayRectangle.Height / lstLog.ItemHeight;

      if (visibleItemCount < lstLog.Items.Count - lstLog.TopIndex)
      {
        lstLog.TopIndex = lstLog.Items.Count - visibleItemCount + 1;
      }
    }

    private void Start()
    {
      stopped = false;

      btnStart.Enabled = false;
      btnStop.Enabled = true;
      btnBrowse.Enabled = false;
      txtUserName.ReadOnly = true;
      txtPassword.ReadOnly = true;
      groupBox1.Enabled = false;

      contracts.Clear();
      serviceProviders.Clear();

      lstGroupItems.Items.Clear();
      lstLog.Items.Clear();

      BuildRepositoryIndex(txtPath.Text);

      if (radioButton1.Checked)
      {
        groupBy = GroupBy.None;
      }
      else if (radioButton2.Checked)
      {
        groupBy = GroupBy.Contracts;
      }
      else if (radioButton3.Checked)
      {
        groupBy = GroupBy.ServiceProviders;
      }

      cookieJar = new CookieContainer();

      try
      {
        try
        {
          DownloadInvoices();
        }
        catch (Exception ex)
        {
          AddLog("Error: " + ex.Message);
        }
      }
      finally
      {
        Stop();
      }
    }

    private void Stop()
    {
      btnBrowse.Enabled = true;
      txtUserName.ReadOnly = false;
      txtPassword.ReadOnly = false;
      groupBox1.Enabled = true;
      btnStop.Enabled = false;
      btnStart.Enabled = true;

      cookieJar = null;
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
      while (!(repoBuilt = BuildRepositoryIndex(folderBrowserDialog1.SelectedPath)));

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
      btnStop.Enabled = false;
      stopped = true;
      //webBrowser1.Navigate(string.Format(url_format, webBrowser1.Url.Scheme, webBrowser1.Url.DnsSafeHost, logout_path, string.Empty));
    }

    private HtmlAgilityPack.HtmlNode Navigate(Uri uri)
    {
      return Navigate(uri, string.Empty);
    }

    private HtmlAgilityPack.HtmlNode Navigate(string uriString)
    {
      return Navigate(new Uri(uriString), string.Empty);
    }

    private HtmlAgilityPack.HtmlNode NavigatePath(string uriPathString)
    {
      return Navigate(new Uri(dijnet_base_url + uriPathString), string.Empty);
    }  

    private HtmlAgilityPack.HtmlNode Navigate(Uri uri, string postData)
    {
      HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

      using(WebClient wc = new WebClient())
      {
        wc.Headers[HttpRequestHeader.Accept] = "text/html";
        //wc.Headers[HttpRequestHeader.Accept] = "text/html, application/xhtml+xml, application/xml; q=0.9, */*; q=0.8";
        //wc.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate, br";
        //wc.Headers[HttpRequestHeader.AcceptLanguage] = "hu-HU";
        //wc.Headers[HttpRequestHeader.CacheControl] = "max-age=0";
        //wc.Headers[HttpRequestHeader.Connection] = "Keep-Alive";
        wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
        wc.Headers[HttpRequestHeader.Cookie] = cookieJar.GetCookieHeader(uri);
        //wc.Headers[HttpRequestHeader.Referer] = uri.AbsoluteUri;

        string htmlResult = wc.UploadString(uri, postData);

        cookieJar.SetCookies(uri, wc.ResponseHeaders[HttpResponseHeader.SetCookie]);

        doc.LoadHtml(htmlResult);
      }

      return doc.DocumentNode;
    }

    private bool Download(Uri uri, out string fileName, out byte[] fileData)
    {
      fileName = null;

      using (WebClient wc = new WebClient())
      {
        wc.Headers[HttpRequestHeader.Cookie] = cookieJar.GetCookieHeader(uri);

        fileData = wc.DownloadData(uri);

        cookieJar.SetCookies(uri, wc.ResponseHeaders[HttpResponseHeader.SetCookie]);

        string[] content_disposition = wc.ResponseHeaders["Content-Disposition"].Split(';');
        if (content_disposition != null && content_disposition.Length > 1 && content_disposition[0] == "attachment")
        {
          string[] filename_attr = content_disposition[1].Split('=');
          if (filename_attr != null && filename_attr.Length > 1 && filename_attr[0] == "filename")
          {
            fileName = filename_attr[1];
            return true;
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
        return false;
      }
    }

    private HtmlAgilityPack.HtmlNode Navigate(string uriString, string postData)
    {
      return Navigate(new Uri(uriString), postData);
    }

    private HtmlAgilityPack.HtmlNode NavigatePath(string uriPathString, string postData)
    {
      return Navigate(new Uri(dijnet_base_url + uriPathString), postData);
    }

    static Uri GetAbsoluteUrlString(string url)
    {
      var uri = new Uri(url, UriKind.RelativeOrAbsolute);
      if (!uri.IsAbsoluteUri)
        uri = new Uri(new Uri(dijnet_base_url), uri);
      return uri;
    }

    private void DownloadInvoices()
    {
      HtmlAgilityPack.HtmlNode node;

      Uri baseUrl = new Uri(dijnet_base_url);

      Uri url = new Uri(baseUrl, login_check_password_path);
      node = Navigate(url, string.Format(login_check_password_post_data_format, txtUserName.Text, txtPassword.Text));
      //if ((node = node.SelectSingleNode(string.Format(xpath_users_login_name_in_header, txtUserName.Text))) != null && (node = node.SelectSingleNode("em")) != null && WebUtility.HtmlDecode(node.InnerText).StartsWith("Bejelentkezési név:"))
      if ((node = node.SelectSingleNode(string.Format(xpath_users_login_name_in_header, txtUserName.Text))) != null && WebUtility.HtmlDecode(node.ParentNode.InnerText).StartsWith("Bejelentkezési név:"))
      {
        node = Navigate(url = new Uri(baseUrl, szamla_search_path));
/*
        HtmlAgilityPack.HtmlNodeCollection contractNodes = node.SelectNodes(string.Format(xpath_group_items, GroupBy.Contracts.ToFilterString()));
        if (contractNodes != null)
        {
          foreach (HtmlAgilityPack.HtmlNode contract in contractNodes)
          {
            contracts.Add(contract.GetAttributeValue("value", ""), WebUtility.HtmlDecode((contract.NextSibling != null) ? contract.NextSibling.InnerText : ""));
          }
        }

        HtmlAgilityPack.HtmlNodeCollection serviceProviderNodes = node.SelectNodes(string.Format(xpath_group_items, GroupBy.ServiceProviders.ToFilterString()));
        if (serviceProviderNodes != null)
        {
          foreach (HtmlAgilityPack.HtmlNode serviceProvider in serviceProviderNodes)
          {
            serviceProviders.Add(serviceProvider.GetAttributeValue("value", ""), WebUtility.HtmlDecode((serviceProvider.NextSibling != null) ? serviceProvider.NextSibling.InnerText : ""));
          }
        }
*/
        HtmlAgilityPack.HtmlNodeCollection scriptNodes = node.SelectNodes(xpath_script_contrains_var_ropts);
        if (scriptNodes != null)
        {
          foreach (HtmlAgilityPack.HtmlNode script in scriptNodes)
          {
            StringReader sr = new StringReader(script.InnerHtml);
            string line;
            string[] line_parts;
            while (((line = sr.ReadLine()) != null) && ((line_parts = line.Trim().Split("=".ToCharArray())) != null))
            {
              string[] var_parts;
              if ((line_parts.Length > 1) && ((var_parts = line_parts[0].Split(" \t".ToCharArray())) != null))
              {
                var_parts = var_parts.Where(x => x != String.Empty).ToArray();
                if ((var_parts.Length > 1) && (var_parts[0] == "var") && (var_parts[1] == "ropts"))
                {                 
                  string json = String.Join("=", line_parts, 1, line_parts.Length - 1).Trim().TrimEnd(';');
                  JavaScriptSerializer jss = new JavaScriptSerializer();
                  List<ServiceProviderContracts> spcs = jss.Deserialize<List<ServiceProviderContracts>>(json);

                  foreach (ServiceProviderContracts spc in spcs.OrderBy(x => x.aliasnev))
                  {
                    if (!contracts.ContainsKey(spc.regszolgid.ToString()))
                    {
                      contracts.Add(spc.regszolgid.ToString(), spc.aliasnev);
                    }
                  }

                  foreach (ServiceProviderContracts spc in spcs.OrderBy(x => x.szlaszolgnev))
                  {
                    if (!serviceProviders.ContainsKey(spc.szlaszolgnev))
                    {
                      serviceProviders.Add(spc.szlaszolgnev, spc.szlaszolgnev);
                    }
                  }
                }                
              }
            }
          }
        }

        if (groupBy == GroupBy.None)
        {
          node = Navigate(url = new Uri(baseUrl, szamla_search_submit_path));
          DownloadInvoices(url, null, node.SelectNodes(xpath_last_listframetable_rows), false);
        }
        else
        {
          Dictionary<string, string> groupItems = (groupBy == GroupBy.Contracts) ? contracts : serviceProviders;

          if (groupItems.Count > 0)
          {
            foreach (KeyValuePair<string, string> groupItem in groupItems)
            {
              node = Navigate(url = new Uri(baseUrl, szamla_search_submit_path), szamla_search_submit_post_data + string.Format(add_query_item, groupBy.ToFilterString(), groupItem.Key));
              DownloadInvoices(url, groupItem.Value, node.SelectNodes(xpath_last_listframetable_rows), true);
            }
            foreach (KeyValuePair<string, string> groupItem in groupItems)
            {
              if (stopped)
              {
                break;
              }

              node = Navigate(url = new Uri(baseUrl, szamla_search_submit_path), szamla_search_submit_post_data + string.Format(add_query_item, groupBy.ToFilterString(), groupItem.Key));
              DownloadInvoices(url, groupItem.Value, node.SelectNodes(xpath_last_listframetable_rows), false);
            }
          }
          else
          {
            AddLog("Error: no items found in selected group!");
          }
        }

        Navigate(new Uri(baseUrl, logout_path));
      }
      else
      {
        AddLog("Error: login failed!");
      }
    }

    private void DownloadInvoices(Uri url, string groupName, HtmlAgilityPack.HtmlNodeCollection listFrameTableRows, bool discoverOnly)
    {
      if (invoices.Root != null && invoices.Root.Name != tag_Invoices)
      {
        invoices.RemoveNodes();
      }
      if (invoices.Root == null)
      {
        invoices.Add(new XElement(tag_Invoices));
      }

      invoices.Root.Elements(tag_Invoice).Attributes(tag_index).Remove();

      try
      {
        int i = 0;

        if (listFrameTableRows != null)
        {
          foreach (HtmlAgilityPack.HtmlNode listFrameTableRow in listFrameTableRows)
          {
            if (stopped)
            {
              return;
            }

            Dictionary<string, string> row = new Dictionary<string, string>();
            HtmlAgilityPack.HtmlNodeCollection anchors = listFrameTableRow.SelectNodes(".//a");
            if (anchors != null)
            {
              foreach (HtmlAgilityPack.HtmlNode anchor in anchors)
              {
                Uri href = new Uri(url, anchor.GetAttributeValue("href", string.Empty));
                Dictionary<string, string> query_parts =
                  href.Query.TrimStart('?').Split('&').Select(x => x.Split('=')).ToDictionary(x => x[0], x => (x.Length > 1) ? x[1] : "");
                row.Add(Uri.UnescapeDataString(query_parts["vfw_colid"]).Split('|')[0].Trim(), WebUtility.HtmlDecode(anchor.InnerText.Trim()));
              }
            }
            else
            {
              HtmlAgilityPack.HtmlNodeCollection tds = listFrameTableRow.SelectNodes("td");
              foreach (HtmlAgilityPack.HtmlNode td in tds)
              {
                Uri href = new Uri(url, WebUtility.HtmlDecode(td.GetAttributeValue("onclick", string.Empty)));
                Dictionary<string, string> query_parts =
                  href.Query.TrimStart('?').Split('&').Select(x => x.Split('=')).ToDictionary(x => x[0], x => (x.Length > 1) ? x[1] : "");
                row.Add(Uri.UnescapeDataString(query_parts["vfw_colid"]).Split('|')[0].Trim(), WebUtility.HtmlDecode(td.InnerText.Trim()));
              }
            }

            XElement xrow =
              invoices.Root.Elements(tag_Invoice)
                .Where(
                  x =>
                    x.Attribute(field_bemdat).Value == row[field_bemdat] &&
                    x.Attribute(field_szamlaszam).Value == row[field_szamlaszam]
                )
                .FirstOrDefault();

            if (xrow == null)
            {
              invoices.Root.Add(xrow = new XElement(tag_Invoice));
            }

            foreach (KeyValuePair<string, string> column in row)
            {
              xrow.SetAttributeValue(column.Key, column.Value);
            }

            xrow.Add(new XAttribute(tag_index, i++));
          }
        }

        missingInvoiceIndexes =
          invoices.Root.Elements(tag_Invoice)
            .Select(
              (element, index) =>
                new { Element = element, Index = index }
            )
            .Where(
              x =>
                x.Element.Attribute(tag_index) != null &&
                (x.Element.Attribute(tag_files) == null || x.Element.Attribute(tag_files).Value == "0")
            )
            .Select(
              x =>
                new int[] { x.Index, Convert.ToInt32(x.Element.Attribute(tag_index).Value) }
            )
            .ToArray();

        if (groupName != null)
        {
          if (discoverOnly)
          {
            lstGroupItems.Items.Add(string.Format("{0} - {1}/{2}", groupName, missingInvoiceIndexes.Length, (listFrameTableRows != null) ? listFrameTableRows.Count : 0));
          }
        }
        else
        {
          lstGroupItems.Items.Add(string.Format("{0}/{1}", missingInvoiceIndexes.Length, (listFrameTableRows != null) ? listFrameTableRows.Count : 0));
        }
      }
      catch (Exception ex)
      {
        AddLog("Error: " + ex.Message);
      }

      if (!discoverOnly && missingInvoiceIndexes.Length > 0)
      {
        Uri urlBase = new Uri(dijnet_base_url);

        for (int i = 0; i < missingInvoiceIndexes.Length; i++)
        {
          if (stopped)
          {
            return;
          }

          HtmlAgilityPack.HtmlNode node;
          node = Navigate(url = new Uri(urlBase, szamla_select_path + string.Format(szamla_select_query_format, missingInvoiceIndexes[i][1])));
          node = Navigate(url = new Uri(urlBase, szamla_letolt_path));

          HtmlAgilityPack.HtmlNode tab_szamla_letolt = node.SelectSingleNode(xpath_div_tab_szamla_letolt);
          if (tab_szamla_letolt != null)
          {
            HtmlAgilityPack.HtmlNodeCollection anchors = (tab_szamla_letolt.FirstChild != null) ? tab_szamla_letolt.FirstChild.SelectNodes(".//a") : null;
            if (anchors != null)
            {
              foreach (HtmlAgilityPack.HtmlNode a in anchors)
              {
                Uri href = new Uri(a.GetAttributeValue("href", null), UriKind.RelativeOrAbsolute);
                if (!href.IsAbsoluteUri)
                {
                  href = new Uri(url, href);
                }

                if (href.DnsSafeHost.Equals(url.DnsSafeHost))
                {
                  Application.DoEvents();

                  string fileName;
                  byte[] fileData;

                  if (Download(href, out fileName, out fileData))
                  {
                    string path =
                      txtPath.Text.TrimEnd(@"\".ToCharArray())
                    + invoices.Root.Elements(tag_Invoice)
                      .Select(
                        x =>
                          new {
                            Szamlaszam = x.Attribute(field_szamlaszam)
                          , BemDat = x.Attribute(field_bemdat)
                          , UgyfelAzon = x.Attribute(field_ugyfelazon)
                          }
                      )
                      .Select(
                        x =>
                          string.Format(
                            repository_path_format
                          , contracts
                            .Where(kv => x.UgyfelAzon != null && kv.Value.StartsWith(x.UgyfelAzon.Value + " ("))
                            .Select(kv => kv.Value)
                            .FirstOrDefault()
                          , x.BemDat.Value.Replace(".", "")
                          , Uri.EscapeDataString(x.Szamlaszam.Value)
                          )
                      )
                      .ElementAt(missingInvoiceIndexes[i][0]);

                    string filename = path + @"\" + fileName;

                    try
                    {
                      Directory.CreateDirectory(path);

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

            Navigate(url = new Uri(urlBase, szamla_list_path));
          }
          else
          {
            AddLog("Error: div 'tab_szamla_letolt' not found!");
            break;
          }
        }
      }
      else
      {
        Application.DoEvents();
      }
    }
  }

  public enum GroupBy
  {
    None,
    Contracts,
    ServiceProviders
  }

  static class GroupByMethods
  {
    public static String ToFilterString(this GroupBy groupBy)
    {
      switch (groupBy)
      {
        case GroupBy.Contracts:
          return "regszolgid";
        case GroupBy.ServiceProviders:
          return "szlaszolgnev";
        default:
          return null;
      }
    }
  }
}
