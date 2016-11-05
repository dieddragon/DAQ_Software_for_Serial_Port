namespace Serial_DAQ_Software
{
    partial class MainWin
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
            this.components = new System.ComponentModel.Container();
            this.serialPortLC = new System.IO.Ports.SerialPort(this.components);
            this.zedGraphControl = new ZedGraph.ZedGraphControl();
            this.cbCOM = new System.Windows.Forms.ComboBox();
            this.btnConnectSP = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSetFreq = new System.Windows.Forms.Button();
            this.cbSetFreqLC = new System.Windows.Forms.ComboBox();
            this.gbSerialPort = new System.Windows.Forms.GroupBox();
            this.gbDaqLC = new System.Windows.Forms.GroupBox();
            this.btnWritetoFile = new System.Windows.Forms.Button();
            this.tbComment = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tbNoData = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbIndTS = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbSerialPort.SuspendLayout();
            this.gbDaqLC.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPortLC
            // 
            this.serialPortLC.BaudRate = 115200;
            this.serialPortLC.DtrEnable = true;
            this.serialPortLC.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortLC_DataReceived);
            // 
            // zedGraphControl
            // 
            this.zedGraphControl.IsShowPointValues = false;
            this.zedGraphControl.Location = new System.Drawing.Point(0, 0);
            this.zedGraphControl.Name = "zedGraphControl";
            this.zedGraphControl.PointValueFormat = "G";
            this.zedGraphControl.Size = new System.Drawing.Size(640, 480);
            this.zedGraphControl.TabIndex = 0;
            // 
            // cbCOM
            // 
            this.cbCOM.FormattingEnabled = true;
            this.cbCOM.Location = new System.Drawing.Point(6, 19);
            this.cbCOM.Name = "cbCOM";
            this.cbCOM.Size = new System.Drawing.Size(121, 21);
            this.cbCOM.TabIndex = 1;
            this.cbCOM.Text = "COM Port";
            // 
            // btnConnectSP
            // 
            this.btnConnectSP.Location = new System.Drawing.Point(6, 46);
            this.btnConnectSP.Name = "btnConnectSP";
            this.btnConnectSP.Size = new System.Drawing.Size(75, 23);
            this.btnConnectSP.TabIndex = 2;
            this.btnConnectSP.Text = "Connect";
            this.btnConnectSP.UseVisualStyleBackColor = true;
            this.btnConnectSP.Click += new System.EventHandler(this.btnConnectSP_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(6, 91);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(6, 120);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSetFreq
            // 
            this.btnSetFreq.Location = new System.Drawing.Point(6, 46);
            this.btnSetFreq.Name = "btnSetFreq";
            this.btnSetFreq.Size = new System.Drawing.Size(91, 23);
            this.btnSetFreq.TabIndex = 6;
            this.btnSetFreq.Text = "Set Frequency";
            this.btnSetFreq.UseVisualStyleBackColor = true;
            this.btnSetFreq.Click += new System.EventHandler(this.btnSetFreq_Click);
            // 
            // cbSetFreqLC
            // 
            this.cbSetFreqLC.FormattingEnabled = true;
            this.cbSetFreqLC.Items.AddRange(new object[] {
            "1",
            "2",
            "5",
            "10",
            "20",
            "40",
            "50",
            "100",
            "200",
            "500",
            "1000"});
            this.cbSetFreqLC.Location = new System.Drawing.Point(6, 19);
            this.cbSetFreqLC.Name = "cbSetFreqLC";
            this.cbSetFreqLC.Size = new System.Drawing.Size(121, 21);
            this.cbSetFreqLC.TabIndex = 7;
            this.cbSetFreqLC.Text = "Sampling Frequency (Hz)";
            // 
            // gbSerialPort
            // 
            this.gbSerialPort.Controls.Add(this.btnConnectSP);
            this.gbSerialPort.Controls.Add(this.cbCOM);
            this.gbSerialPort.Location = new System.Drawing.Point(646, 10);
            this.gbSerialPort.Name = "gbSerialPort";
            this.gbSerialPort.Size = new System.Drawing.Size(134, 79);
            this.gbSerialPort.TabIndex = 10;
            this.gbSerialPort.TabStop = false;
            this.gbSerialPort.Text = "Serial Port";
            // 
            // gbDaqLC
            // 
            this.gbDaqLC.Controls.Add(this.cbSetFreqLC);
            this.gbDaqLC.Controls.Add(this.btnSetFreq);
            this.gbDaqLC.Controls.Add(this.btnStart);
            this.gbDaqLC.Controls.Add(this.btnReset);
            this.gbDaqLC.Location = new System.Drawing.Point(646, 95);
            this.gbDaqLC.Name = "gbDaqLC";
            this.gbDaqLC.Size = new System.Drawing.Size(134, 151);
            this.gbDaqLC.TabIndex = 12;
            this.gbDaqLC.TabStop = false;
            this.gbDaqLC.Text = "Load Cell Daq";
            // 
            // btnWritetoFile
            // 
            this.btnWritetoFile.Location = new System.Drawing.Point(651, 439);
            this.btnWritetoFile.Name = "btnWritetoFile";
            this.btnWritetoFile.Size = new System.Drawing.Size(75, 23);
            this.btnWritetoFile.TabIndex = 15;
            this.btnWritetoFile.Text = "Write to File";
            this.btnWritetoFile.UseVisualStyleBackColor = true;
            this.btnWritetoFile.Click += new System.EventHandler(this.btnWritetoFile_Click);
            // 
            // tbComment
            // 
            this.tbComment.Location = new System.Drawing.Point(652, 413);
            this.tbComment.Name = "tbComment";
            this.tbComment.Size = new System.Drawing.Size(122, 20);
            this.tbComment.TabIndex = 15;
            this.tbComment.Text = "Comment";
            // 
            // tbNoData
            // 
            this.tbNoData.Location = new System.Drawing.Point(6, 41);
            this.tbNoData.Name = "tbNoData";
            this.tbNoData.Size = new System.Drawing.Size(35, 20);
            this.tbNoData.TabIndex = 16;
            this.tbNoData.Text = "4";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbIndTS);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbNoData);
            this.groupBox1.Location = new System.Drawing.Point(646, 257);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(133, 150);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 52);
            this.label2.TabIndex = 19;
            this.label2.Text = "Index of Time Stamp \r\nin Data\r\n(0 for no Time Stamp)\r\n\r\n";
            // 
            // tbIndTS
            // 
            this.tbIndTS.Location = new System.Drawing.Point(6, 124);
            this.tbIndTS.Name = "tbIndTS";
            this.tbIndTS.Size = new System.Drawing.Size(35, 20);
            this.tbIndTS.TabIndex = 18;
            this.tbIndTS.Text = "4";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "No of Data in Message";
            // 
            // MainWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbComment);
            this.Controls.Add(this.btnWritetoFile);
            this.Controls.Add(this.gbDaqLC);
            this.Controls.Add(this.gbSerialPort);
            this.Controls.Add(this.zedGraphControl);
            this.Name = "MainWin";
            this.Text = "DAQ From Serial Port";
            this.gbSerialPort.ResumeLayout(false);
            this.gbDaqLC.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPortLC;
        private ZedGraph.ZedGraphControl zedGraphControl;
        private System.Windows.Forms.ComboBox cbCOM;
        private System.Windows.Forms.Button btnConnectSP;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSetFreq;
        private System.Windows.Forms.ComboBox cbSetFreqLC;
        private System.Windows.Forms.GroupBox gbSerialPort;
        private System.Windows.Forms.GroupBox gbDaqLC;
        private System.Windows.Forms.Button btnWritetoFile;
        private System.Windows.Forms.TextBox tbComment;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox tbNoData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbIndTS;
        private System.Windows.Forms.Label label1;
    }
}

