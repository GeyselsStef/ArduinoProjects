namespace StepperMotor
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.GroupBoxConnection = new System.Windows.Forms.GroupBox();
            this.ButtonDisconnect = new System.Windows.Forms.Button();
            this.ButtonReloadComPorts = new System.Windows.Forms.Button();
            this.ButtonConnect = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ButtonConnectWifi = new System.Windows.Forms.Button();
            this.textBoxIp = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ButtonStop = new System.Windows.Forms.Button();
            this.ButtonManualRightFast = new System.Windows.Forms.Button();
            this.ButtonManualRight = new System.Windows.Forms.Button();
            this.ButtonManualLeft = new System.Windows.Forms.Button();
            this.ButtonManualLeftFast = new System.Windows.Forms.Button();
            this.ButtonResetOrigin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelLogLevel = new System.Windows.Forms.Label();
            this.ButtonSendLogLevel = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.ButtonRefreshArduinoData = new System.Windows.Forms.Button();
            this.ButtonToOrigin = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.ButtonSendViaTCP = new System.Windows.Forms.Button();
            this.textBoxTxpCommand = new System.Windows.Forms.TextBox();
            this.GroupBoxConnection.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.Location = new System.Drawing.Point(9, 105);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(498, 252);
            this.textBox1.TabIndex = 0;
            // 
            // GroupBoxConnection
            // 
            this.GroupBoxConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GroupBoxConnection.Controls.Add(this.ButtonDisconnect);
            this.GroupBoxConnection.Controls.Add(this.ButtonReloadComPorts);
            this.GroupBoxConnection.Controls.Add(this.ButtonConnect);
            this.GroupBoxConnection.Controls.Add(this.comboBox1);
            this.GroupBoxConnection.Location = new System.Drawing.Point(9, 10);
            this.GroupBoxConnection.Margin = new System.Windows.Forms.Padding(2);
            this.GroupBoxConnection.Name = "GroupBoxConnection";
            this.GroupBoxConnection.Padding = new System.Windows.Forms.Padding(2);
            this.GroupBoxConnection.Size = new System.Drawing.Size(497, 90);
            this.GroupBoxConnection.TabIndex = 1;
            this.GroupBoxConnection.TabStop = false;
            this.GroupBoxConnection.Text = "Connection";
            // 
            // ButtonDisconnect
            // 
            this.ButtonDisconnect.Enabled = false;
            this.ButtonDisconnect.Location = new System.Drawing.Point(70, 41);
            this.ButtonDisconnect.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonDisconnect.Name = "ButtonDisconnect";
            this.ButtonDisconnect.Size = new System.Drawing.Size(74, 30);
            this.ButtonDisconnect.TabIndex = 3;
            this.ButtonDisconnect.Text = "Disconnect";
            this.ButtonDisconnect.UseVisualStyleBackColor = true;
            this.ButtonDisconnect.Click += new System.EventHandler(this.ButtonDisconnect_Click);
            // 
            // ButtonReloadComPorts
            // 
            this.ButtonReloadComPorts.Location = new System.Drawing.Point(240, 17);
            this.ButtonReloadComPorts.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonReloadComPorts.Name = "ButtonReloadComPorts";
            this.ButtonReloadComPorts.Size = new System.Drawing.Size(60, 21);
            this.ButtonReloadComPorts.TabIndex = 2;
            this.ButtonReloadComPorts.Text = "Refresh";
            this.ButtonReloadComPorts.UseVisualStyleBackColor = true;
            this.ButtonReloadComPorts.Click += new System.EventHandler(this.ButtonReload_Click);
            // 
            // ButtonConnect
            // 
            this.ButtonConnect.Location = new System.Drawing.Point(4, 41);
            this.ButtonConnect.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonConnect.Name = "ButtonConnect";
            this.ButtonConnect.Size = new System.Drawing.Size(62, 30);
            this.ButtonConnect.TabIndex = 1;
            this.ButtonConnect.Text = "Connect";
            this.ButtonConnect.UseVisualStyleBackColor = true;
            this.ButtonConnect.Click += new System.EventHandler(this.ButtonConnect_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(4, 17);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(232, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ButtonSendViaTCP);
            this.groupBox1.Controls.Add(this.textBoxTxpCommand);
            this.groupBox1.Controls.Add(this.ButtonConnectWifi);
            this.groupBox1.Controls.Add(this.textBoxIp);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.ButtonResetOrigin);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.labelLogLevel);
            this.groupBox1.Controls.Add(this.ButtonSendLogLevel);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.ButtonRefreshArduinoData);
            this.groupBox1.Controls.Add(this.ButtonToOrigin);
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Location = new System.Drawing.Point(511, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(403, 346);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // ButtonConnectWifi
            // 
            this.ButtonConnectWifi.Location = new System.Drawing.Point(208, 197);
            this.ButtonConnectWifi.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonConnectWifi.Name = "ButtonConnectWifi";
            this.ButtonConnectWifi.Size = new System.Drawing.Size(67, 21);
            this.ButtonConnectWifi.TabIndex = 10;
            this.ButtonConnectWifi.Text = "Connect";
            this.ButtonConnectWifi.UseVisualStyleBackColor = true;
            this.ButtonConnectWifi.Click += new System.EventHandler(this.ButtonConnectWifi_Click);
            // 
            // textBoxIp
            // 
            this.textBoxIp.Location = new System.Drawing.Point(103, 197);
            this.textBoxIp.Name = "textBoxIp";
            this.textBoxIp.Size = new System.Drawing.Size(100, 20);
            this.textBoxIp.TabIndex = 9;
            this.textBoxIp.Text = "192.168.0.186";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ButtonStop);
            this.groupBox2.Controls.Add(this.ButtonManualRightFast);
            this.groupBox2.Controls.Add(this.ButtonManualRight);
            this.groupBox2.Controls.Add(this.ButtonManualLeft);
            this.groupBox2.Controls.Add(this.ButtonManualLeftFast);
            this.groupBox2.Location = new System.Drawing.Point(6, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(392, 61);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // ButtonStop
            // 
            this.ButtonStop.Location = new System.Drawing.Point(178, 19);
            this.ButtonStop.Name = "ButtonStop";
            this.ButtonStop.Size = new System.Drawing.Size(39, 23);
            this.ButtonStop.TabIndex = 4;
            this.ButtonStop.Text = "Stop";
            this.ButtonStop.UseVisualStyleBackColor = true;
            this.ButtonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // ButtonManualRightFast
            // 
            this.ButtonManualRightFast.Location = new System.Drawing.Point(268, 19);
            this.ButtonManualRightFast.Name = "ButtonManualRightFast";
            this.ButtonManualRightFast.Size = new System.Drawing.Size(39, 23);
            this.ButtonManualRightFast.TabIndex = 3;
            this.ButtonManualRightFast.Text = ">>";
            this.ButtonManualRightFast.UseVisualStyleBackColor = true;
            this.ButtonManualRightFast.Click += new System.EventHandler(this.ButtonManualRightFast_Click);
            // 
            // ButtonManualRight
            // 
            this.ButtonManualRight.Location = new System.Drawing.Point(223, 19);
            this.ButtonManualRight.Name = "ButtonManualRight";
            this.ButtonManualRight.Size = new System.Drawing.Size(39, 23);
            this.ButtonManualRight.TabIndex = 2;
            this.ButtonManualRight.Text = ">";
            this.ButtonManualRight.UseVisualStyleBackColor = true;
            this.ButtonManualRight.Click += new System.EventHandler(this.ButtonManualRight_Click);
            // 
            // ButtonManualLeft
            // 
            this.ButtonManualLeft.Location = new System.Drawing.Point(133, 19);
            this.ButtonManualLeft.Name = "ButtonManualLeft";
            this.ButtonManualLeft.Size = new System.Drawing.Size(39, 23);
            this.ButtonManualLeft.TabIndex = 1;
            this.ButtonManualLeft.Text = "<";
            this.ButtonManualLeft.UseVisualStyleBackColor = true;
            this.ButtonManualLeft.Click += new System.EventHandler(this.ButtonManualLeft_Click);
            // 
            // ButtonManualLeftFast
            // 
            this.ButtonManualLeftFast.Location = new System.Drawing.Point(88, 19);
            this.ButtonManualLeftFast.Name = "ButtonManualLeftFast";
            this.ButtonManualLeftFast.Size = new System.Drawing.Size(39, 23);
            this.ButtonManualLeftFast.TabIndex = 0;
            this.ButtonManualLeftFast.Text = "<<";
            this.ButtonManualLeftFast.UseVisualStyleBackColor = true;
            this.ButtonManualLeftFast.Click += new System.EventHandler(this.ButtonManualLeftFast_Click);
            // 
            // ButtonResetOrigin
            // 
            this.ButtonResetOrigin.Location = new System.Drawing.Point(102, 275);
            this.ButtonResetOrigin.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonResetOrigin.Name = "ButtonResetOrigin";
            this.ButtonResetOrigin.Size = new System.Drawing.Size(85, 21);
            this.ButtonResetOrigin.TabIndex = 7;
            this.ButtonResetOrigin.Text = "Reset origin";
            this.ButtonResetOrigin.UseVisualStyleBackColor = true;
            this.ButtonResetOrigin.Click += new System.EventHandler(this.ButtonResetOrigin_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 275);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 21);
            this.label1.TabIndex = 6;
            this.label1.Text = "Log level";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelLogLevel
            // 
            this.labelLogLevel.Location = new System.Drawing.Point(5, 254);
            this.labelLogLevel.Name = "labelLogLevel";
            this.labelLogLevel.Size = new System.Drawing.Size(91, 21);
            this.labelLogLevel.TabIndex = 5;
            this.labelLogLevel.Text = "Log level";
            this.labelLogLevel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ButtonSendLogLevel
            // 
            this.ButtonSendLogLevel.Location = new System.Drawing.Point(228, 254);
            this.ButtonSendLogLevel.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonSendLogLevel.Name = "ButtonSendLogLevel";
            this.ButtonSendLogLevel.Size = new System.Drawing.Size(60, 21);
            this.ButtonSendLogLevel.TabIndex = 3;
            this.ButtonSendLogLevel.Text = "Send";
            this.ButtonSendLogLevel.UseVisualStyleBackColor = true;
            this.ButtonSendLogLevel.Click += new System.EventHandler(this.ButtonSendLogLevel_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(102, 254);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 4;
            // 
            // ButtonRefreshArduinoData
            // 
            this.ButtonRefreshArduinoData.Location = new System.Drawing.Point(337, 17);
            this.ButtonRefreshArduinoData.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonRefreshArduinoData.Name = "ButtonRefreshArduinoData";
            this.ButtonRefreshArduinoData.Size = new System.Drawing.Size(62, 20);
            this.ButtonRefreshArduinoData.TabIndex = 3;
            this.ButtonRefreshArduinoData.Text = "Refresh";
            this.ButtonRefreshArduinoData.UseVisualStyleBackColor = true;
            this.ButtonRefreshArduinoData.Click += new System.EventHandler(this.ButtonRefreshArduinoData_Click);
            // 
            // ButtonToOrigin
            // 
            this.ButtonToOrigin.Location = new System.Drawing.Point(194, 77);
            this.ButtonToOrigin.Name = "ButtonToOrigin";
            this.ButtonToOrigin.Size = new System.Drawing.Size(20, 23);
            this.ButtonToOrigin.TabIndex = 1;
            this.ButtonToOrigin.Text = "0";
            this.ButtonToOrigin.UseVisualStyleBackColor = true;
            this.ButtonToOrigin.Click += new System.EventHandler(this.ButtonToOrigin_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 12;
            this.trackBar1.Location = new System.Drawing.Point(6, 45);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(2);
            this.trackBar1.Maximum = 120;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(394, 45);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.TabStop = false;
            this.trackBar1.Value = 60;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            this.trackBar1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackBar1_MouseDown);
            this.trackBar1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar1_MouseUp);
            // 
            // ButtonSendViaTCP
            // 
            this.ButtonSendViaTCP.Location = new System.Drawing.Point(208, 223);
            this.ButtonSendViaTCP.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonSendViaTCP.Name = "ButtonSendViaTCP";
            this.ButtonSendViaTCP.Size = new System.Drawing.Size(67, 21);
            this.ButtonSendViaTCP.TabIndex = 12;
            this.ButtonSendViaTCP.Text = "Send TCP";
            this.ButtonSendViaTCP.UseVisualStyleBackColor = true;
            this.ButtonSendViaTCP.Click += new System.EventHandler(this.ButtonSendViaTCP_Click);
            // 
            // textBoxTxpCommand
            // 
            this.textBoxTxpCommand.Location = new System.Drawing.Point(103, 223);
            this.textBoxTxpCommand.Name = "textBoxTxpCommand";
            this.textBoxTxpCommand.Size = new System.Drawing.Size(100, 20);
            this.textBoxTxpCommand.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 366);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GroupBoxConnection);
            this.Controls.Add(this.textBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.GroupBoxConnection.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox GroupBoxConnection;
        private System.Windows.Forms.Button ButtonConnect;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button ButtonReloadComPorts;
        private System.Windows.Forms.Button ButtonToOrigin;
        private System.Windows.Forms.Button ButtonRefreshArduinoData;
        private System.Windows.Forms.Label labelLogLevel;
        private System.Windows.Forms.Button ButtonSendLogLevel;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button ButtonDisconnect;
        private System.Windows.Forms.Button ButtonResetOrigin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button ButtonManualRightFast;
        private System.Windows.Forms.Button ButtonManualRight;
        private System.Windows.Forms.Button ButtonManualLeft;
        private System.Windows.Forms.Button ButtonManualLeftFast;
        private System.Windows.Forms.Button ButtonStop;
        private System.Windows.Forms.TextBox textBoxIp;
        private System.Windows.Forms.Button ButtonConnectWifi;
        private System.Windows.Forms.Button ButtonSendViaTCP;
        private System.Windows.Forms.TextBox textBoxTxpCommand;
    }
}

