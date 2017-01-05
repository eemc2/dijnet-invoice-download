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
      this.webBrowser1 = new System.Windows.Forms.WebBrowser();
      this.webBrowser2 = new System.Windows.Forms.WebBrowser();
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnBrowse = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.txtPath = new System.Windows.Forms.TextBox();
      this.btnStart = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.txtPassword = new System.Windows.Forms.TextBox();
      this.txtUserName = new System.Windows.Forms.TextBox();
      this.lstServices = new System.Windows.Forms.ListBox();
      this.lstLog = new System.Windows.Forms.ListBox();
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // webBrowser1
      // 
      this.webBrowser1.Location = new System.Drawing.Point(405, 110);
      this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
      this.webBrowser1.Name = "webBrowser1";
      this.webBrowser1.Size = new System.Drawing.Size(48, 42);
      this.webBrowser1.TabIndex = 7;
      this.webBrowser1.Url = new System.Uri("", System.UriKind.Relative);
      this.webBrowser1.Visible = false;
      this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
      // 
      // webBrowser2
      // 
      this.webBrowser2.Location = new System.Drawing.Point(405, 158);
      this.webBrowser2.MinimumSize = new System.Drawing.Size(20, 20);
      this.webBrowser2.Name = "webBrowser2";
      this.webBrowser2.Size = new System.Drawing.Size(48, 43);
      this.webBrowser2.TabIndex = 8;
      this.webBrowser2.Url = new System.Uri("", System.UriKind.Relative);
      this.webBrowser2.Visible = false;
      this.webBrowser2.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser2_DocumentCompleted);
      // 
      // panel1
      // 
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
      this.panel1.Size = new System.Drawing.Size(1046, 104);
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
      this.btnStart.Location = new System.Drawing.Point(242, 36);
      this.btnStart.Name = "btnStart";
      this.btnStart.Size = new System.Drawing.Size(18, 21);
      this.btnStart.TabIndex = 4;
      this.btnStart.Text = "?";
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
      // lstServices
      // 
      this.lstServices.FormattingEnabled = true;
      this.lstServices.Location = new System.Drawing.Point(12, 110);
      this.lstServices.Name = "lstServices";
      this.lstServices.Size = new System.Drawing.Size(387, 147);
      this.lstServices.TabIndex = 10;
      // 
      // lstLog
      // 
      this.lstLog.FormattingEnabled = true;
      this.lstLog.Location = new System.Drawing.Point(12, 267);
      this.lstLog.Name = "lstLog";
      this.lstLog.Size = new System.Drawing.Size(1022, 537);
      this.lstLog.TabIndex = 11;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1046, 962);
      this.Controls.Add(this.lstLog);
      this.Controls.Add(this.lstServices);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.webBrowser2);
      this.Controls.Add(this.webBrowser1);
      this.Name = "Form1";
      this.Text = "Form1";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.WebBrowser webBrowser1;
    private System.Windows.Forms.WebBrowser webBrowser2;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnStart;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.TextBox txtUserName;
    private System.Windows.Forms.ListBox lstServices;
    private System.Windows.Forms.ListBox lstLog;
    private System.Windows.Forms.Button btnBrowse;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtPath;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
  }
}

