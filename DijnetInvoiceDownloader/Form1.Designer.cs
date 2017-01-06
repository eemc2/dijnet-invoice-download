namespace DijnetInvoiceDownloader
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnBrowse = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.txtPath = new System.Windows.Forms.TextBox();
      this.btnStart = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.txtPassword = new System.Windows.Forms.TextBox();
      this.txtUserName = new System.Windows.Forms.TextBox();
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.panel2 = new System.Windows.Forms.Panel();
      this.panel3 = new System.Windows.Forms.Panel();
      this.btnStop = new System.Windows.Forms.Button();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.lstLog = new System.Windows.Forms.ListBox();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.splitter2 = new System.Windows.Forms.Splitter();
      this.webBrowser2 = new System.Windows.Forms.WebBrowser();
      this.webBrowser1 = new System.Windows.Forms.WebBrowser();
      this.panel4 = new System.Windows.Forms.Panel();
      this.lstServices = new System.Windows.Forms.ListBox();
      this.label4 = new System.Windows.Forms.Label();
      this.splitter3 = new System.Windows.Forms.Splitter();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel3.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.panel4.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnStop);
      this.panel1.Controls.Add(this.btnBrowse);
      this.panel1.Controls.Add(this.label3);
      this.panel1.Controls.Add(this.txtPath);
      this.panel1.Controls.Add(this.btnStart);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.txtPassword);
      this.panel1.Controls.Add(this.txtUserName);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(954, 92);
      this.panel1.TabIndex = 9;
      // 
      // btnBrowse
      // 
      this.btnBrowse.Location = new System.Drawing.Point(405, 10);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new System.Drawing.Size(24, 21);
      this.btnBrowse.TabIndex = 7;
      this.btnBrowse.Text = "...";
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(9, 12);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(60, 13);
      this.label3.TabIndex = 6;
      this.label3.Text = "Repository:";
      // 
      // txtPath
      // 
      this.txtPath.Location = new System.Drawing.Point(75, 10);
      this.txtPath.Name = "txtPath";
      this.txtPath.ReadOnly = true;
      this.txtPath.Size = new System.Drawing.Size(324, 20);
      this.txtPath.TabIndex = 5;
      this.txtPath.Text = "C:\\TEMP\\dijnet";
      // 
      // btnStart
      // 
      this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnStart.Location = new System.Drawing.Point(844, 8);
      this.btnStart.Name = "btnStart";
      this.btnStart.Size = new System.Drawing.Size(102, 21);
      this.btnStart.TabIndex = 4;
      this.btnStart.Text = "Start";
      this.btnStart.UseVisualStyleBackColor = true;
      this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(11, 65);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(56, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Password:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(10, 39);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(58, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Username:";
      // 
      // txtPassword
      // 
      this.txtPassword.Location = new System.Drawing.Point(75, 62);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.PasswordChar = '*';
      this.txtPassword.Size = new System.Drawing.Size(161, 20);
      this.txtPassword.TabIndex = 1;
      // 
      // txtUserName
      // 
      this.txtUserName.Location = new System.Drawing.Point(75, 36);
      this.txtUserName.Name = "txtUserName";
      this.txtUserName.Size = new System.Drawing.Size(161, 20);
      this.txtUserName.TabIndex = 0;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.tabControl1);
      this.panel2.Controls.Add(this.splitter1);
      this.panel2.Controls.Add(this.panel3);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(0, 92);
      this.panel2.Name = "panel2";
      this.panel2.Padding = new System.Windows.Forms.Padding(8);
      this.panel2.Size = new System.Drawing.Size(954, 616);
      this.panel2.TabIndex = 12;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.splitter3);
      this.panel3.Controls.Add(this.lstServices);
      this.panel3.Controls.Add(this.panel4);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel3.Location = new System.Drawing.Point(8, 8);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(938, 263);
      this.panel3.TabIndex = 14;
      // 
      // btnStop
      // 
      this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnStop.Location = new System.Drawing.Point(844, 35);
      this.btnStop.Name = "btnStop";
      this.btnStop.Size = new System.Drawing.Size(102, 21);
      this.btnStop.TabIndex = 8;
      this.btnStop.Text = "Stop";
      this.btnStop.UseVisualStyleBackColor = true;
      this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
      // 
      // splitter1
      // 
      this.splitter1.Cursor = System.Windows.Forms.Cursors.HSplit;
      this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
      this.splitter1.Location = new System.Drawing.Point(8, 271);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(938, 3);
      this.splitter1.TabIndex = 15;
      this.splitter1.TabStop = false;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(8, 274);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(938, 334);
      this.tabControl1.TabIndex = 18;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.lstLog);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(930, 308);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Log";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // lstLog
      // 
      this.lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lstLog.FormattingEnabled = true;
      this.lstLog.Location = new System.Drawing.Point(3, 3);
      this.lstLog.Name = "lstLog";
      this.lstLog.Size = new System.Drawing.Size(924, 302);
      this.lstLog.TabIndex = 16;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.splitter2);
      this.tabPage2.Controls.Add(this.webBrowser2);
      this.tabPage2.Controls.Add(this.webBrowser1);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(930, 308);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Browser view(s) - debug";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // splitter2
      // 
      this.splitter2.Location = new System.Drawing.Point(482, 3);
      this.splitter2.Name = "splitter2";
      this.splitter2.Size = new System.Drawing.Size(3, 302);
      this.splitter2.TabIndex = 17;
      this.splitter2.TabStop = false;
      // 
      // webBrowser2
      // 
      this.webBrowser2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.webBrowser2.Location = new System.Drawing.Point(482, 3);
      this.webBrowser2.MinimumSize = new System.Drawing.Size(20, 20);
      this.webBrowser2.Name = "webBrowser2";
      this.webBrowser2.Size = new System.Drawing.Size(445, 302);
      this.webBrowser2.TabIndex = 16;
      this.webBrowser2.Url = new System.Uri("", System.UriKind.Relative);
      this.webBrowser2.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser2_DocumentCompleted);
      // 
      // webBrowser1
      // 
      this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Left;
      this.webBrowser1.Location = new System.Drawing.Point(3, 3);
      this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
      this.webBrowser1.Name = "webBrowser1";
      this.webBrowser1.Size = new System.Drawing.Size(479, 302);
      this.webBrowser1.TabIndex = 15;
      this.webBrowser1.Url = new System.Uri("", System.UriKind.Relative);
      this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
      // 
      // panel4
      // 
      this.panel4.Controls.Add(this.label4);
      this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel4.Location = new System.Drawing.Point(0, 0);
      this.panel4.Name = "panel4";
      this.panel4.Size = new System.Drawing.Size(938, 20);
      this.panel4.TabIndex = 17;
      // 
      // lstServices
      // 
      this.lstServices.Dock = System.Windows.Forms.DockStyle.Left;
      this.lstServices.FormattingEnabled = true;
      this.lstServices.Location = new System.Drawing.Point(0, 20);
      this.lstServices.Name = "lstServices";
      this.lstServices.Size = new System.Drawing.Size(298, 243);
      this.lstServices.TabIndex = 18;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(1, 4);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(58, 13);
      this.label4.TabIndex = 0;
      this.label4.Text = "Contract(s)";
      // 
      // splitter3
      // 
      this.splitter3.Location = new System.Drawing.Point(298, 20);
      this.splitter3.Name = "splitter3";
      this.splitter3.Size = new System.Drawing.Size(3, 243);
      this.splitter3.TabIndex = 19;
      this.splitter3.TabStop = false;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(954, 708);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel1);
      this.Name = "Form1";
      this.Text = "Díjnet invoice downloader";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.panel4.ResumeLayout(false);
      this.panel4.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnStart;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.TextBox txtUserName;
    private System.Windows.Forms.Button btnBrowse;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtPath;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.Button btnStop;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.ListBox lstLog;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.Splitter splitter2;
    private System.Windows.Forms.WebBrowser webBrowser2;
    private System.Windows.Forms.WebBrowser webBrowser1;
    private System.Windows.Forms.Splitter splitter1;
    private System.Windows.Forms.ListBox lstServices;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Splitter splitter3;
  }
}

