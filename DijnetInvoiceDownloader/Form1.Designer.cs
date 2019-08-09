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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.radioButton3 = new System.Windows.Forms.RadioButton();
      this.radioButton2 = new System.Windows.Forms.RadioButton();
      this.radioButton1 = new System.Windows.Forms.RadioButton();
      this.btnStop = new System.Windows.Forms.Button();
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
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.lstLog = new System.Windows.Forms.ListBox();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.panel3 = new System.Windows.Forms.Panel();
      this.splitter3 = new System.Windows.Forms.Splitter();
      this.lstGroupItems = new System.Windows.Forms.ListBox();
      this.panel4 = new System.Windows.Forms.Panel();
      this.label4 = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.panel3.SuspendLayout();
      this.panel4.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.groupBox1);
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
      this.panel1.Size = new System.Drawing.Size(954, 104);
      this.panel1.TabIndex = 9;
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.radioButton3);
      this.groupBox1.Controls.Add(this.radioButton2);
      this.groupBox1.Controls.Add(this.radioButton1);
      this.groupBox1.Location = new System.Drawing.Point(707, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(131, 94);
      this.groupBox1.TabIndex = 10;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Csoportosítás";
      // 
      // radioButton3
      // 
      this.radioButton3.AutoSize = true;
      this.radioButton3.Location = new System.Drawing.Point(15, 65);
      this.radioButton3.Name = "radioButton3";
      this.radioButton3.Size = new System.Drawing.Size(110, 17);
      this.radioButton3.TabIndex = 12;
      this.radioButton3.Text = "Szolgáltató szerint";
      this.radioButton3.UseVisualStyleBackColor = true;
      // 
      // radioButton2
      // 
      this.radioButton2.AutoSize = true;
      this.radioButton2.Location = new System.Drawing.Point(15, 42);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.Size = new System.Drawing.Size(107, 17);
      this.radioButton2.TabIndex = 11;
      this.radioButton2.Text = "Szerződés szerint";
      this.radioButton2.UseVisualStyleBackColor = true;
      // 
      // radioButton1
      // 
      this.radioButton1.AutoSize = true;
      this.radioButton1.Checked = true;
      this.radioButton1.Location = new System.Drawing.Point(15, 19);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new System.Drawing.Size(52, 17);
      this.radioButton1.TabIndex = 10;
      this.radioButton1.TabStop = true;
      this.radioButton1.Text = "Nincs";
      this.radioButton1.UseVisualStyleBackColor = true;
      // 
      // btnStop
      // 
      this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnStop.Location = new System.Drawing.Point(844, 35);
      this.btnStop.Name = "btnStop";
      this.btnStop.Size = new System.Drawing.Size(102, 21);
      this.btnStop.TabIndex = 5;
      this.btnStop.Text = "&Megállít";
      this.btnStop.UseVisualStyleBackColor = true;
      this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
      // 
      // btnBrowse
      // 
      this.btnBrowse.Location = new System.Drawing.Point(405, 10);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new System.Drawing.Size(24, 21);
      this.btnBrowse.TabIndex = 3;
      this.btnBrowse.Text = "...";
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(9, 12);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(44, 13);
      this.label3.TabIndex = 6;
      this.label3.Text = "Adattár:";
      // 
      // txtPath
      // 
      this.txtPath.Location = new System.Drawing.Point(75, 10);
      this.txtPath.Name = "txtPath";
      this.txtPath.ReadOnly = true;
      this.txtPath.Size = new System.Drawing.Size(324, 20);
      this.txtPath.TabIndex = 6;
      this.txtPath.TabStop = false;
      this.txtPath.Text = ".";
      // 
      // btnStart
      // 
      this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnStart.Location = new System.Drawing.Point(844, 8);
      this.btnStart.Name = "btnStart";
      this.btnStart.Size = new System.Drawing.Size(102, 21);
      this.btnStart.TabIndex = 4;
      this.btnStart.Text = "&Start";
      this.btnStart.UseVisualStyleBackColor = true;
      this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(11, 65);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(39, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Jelszó:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(10, 39);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(54, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Felh. név:";
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
      this.panel2.Location = new System.Drawing.Point(0, 104);
      this.panel2.Name = "panel2";
      this.panel2.Padding = new System.Windows.Forms.Padding(8);
      this.panel2.Size = new System.Drawing.Size(954, 604);
      this.panel2.TabIndex = 12;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(8, 274);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(938, 322);
      this.tabControl1.TabIndex = 18;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.lstLog);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(930, 296);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Napló";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // lstLog
      // 
      this.lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lstLog.FormattingEnabled = true;
      this.lstLog.Location = new System.Drawing.Point(3, 3);
      this.lstLog.Name = "lstLog";
      this.lstLog.Size = new System.Drawing.Size(924, 290);
      this.lstLog.TabIndex = 16;
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
      // panel3
      // 
      this.panel3.Controls.Add(this.splitter3);
      this.panel3.Controls.Add(this.lstGroupItems);
      this.panel3.Controls.Add(this.panel4);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel3.Location = new System.Drawing.Point(8, 8);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(938, 263);
      this.panel3.TabIndex = 14;
      // 
      // splitter3
      // 
      this.splitter3.Location = new System.Drawing.Point(298, 20);
      this.splitter3.Name = "splitter3";
      this.splitter3.Size = new System.Drawing.Size(3, 243);
      this.splitter3.TabIndex = 19;
      this.splitter3.TabStop = false;
      // 
      // lstGroupItems
      // 
      this.lstGroupItems.Dock = System.Windows.Forms.DockStyle.Left;
      this.lstGroupItems.FormattingEnabled = true;
      this.lstGroupItems.Location = new System.Drawing.Point(0, 20);
      this.lstGroupItems.Name = "lstGroupItems";
      this.lstGroupItems.Size = new System.Drawing.Size(298, 243);
      this.lstGroupItems.TabIndex = 18;
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
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(1, 4);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(86, 13);
      this.label4.TabIndex = 0;
      this.label4.Text = "Csoport elem(ek)";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(954, 708);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel1);
      this.Name = "Form1";
      this.Text = "Díjnet számla letöltő";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
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
    private System.Windows.Forms.Splitter splitter1;
    private System.Windows.Forms.ListBox lstGroupItems;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Splitter splitter3;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton radioButton3;
    private System.Windows.Forms.RadioButton radioButton2;
    private System.Windows.Forms.RadioButton radioButton1;
  }
}

