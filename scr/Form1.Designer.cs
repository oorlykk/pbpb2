namespace pbpb
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose( bool disposing )
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnStartStopBot = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.chb_SaveReward = new System.Windows.Forms.CheckBox();
            this.tray = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiStartBot = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStopBot = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiBotStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.pUBGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.launchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.terminateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.openPicturesFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAutolauchifidle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrIdleCheck = new System.Windows.Forms.Timer(this.components);
            this.chb_AutoStartOnIdle = new System.Windows.Forms.CheckBox();
            this.ne_MaxIdle = new System.Windows.Forms.NumericUpDown();
            this.txLog = new System.Windows.Forms.TextBox();
            this.panel_test = new System.Windows.Forms.FlowLayoutPanel();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnttt = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.ne_PosX = new System.Windows.Forms.NumericUpDown();
            this.ne_PosY = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chb_PassiveMode = new System.Windows.Forms.CheckBox();
            this.chb_HiddenMode = new System.Windows.Forms.CheckBox();
            this.chb_CanKillSteam = new System.Windows.Forms.CheckBox();
            this.cbox_PubgInput = new System.Windows.Forms.ComboBox();
            this.chb_view = new System.Windows.Forms.CheckBox();
            this.button8 = new System.Windows.Forms.Button();
            this.ne_MaxRoundTIme = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.PanelView = new pbpb.PanelDoubleBuffered();
            this.lab_CurrentRounTime = new System.Windows.Forms.Label();
            this.trayms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ne_MaxIdle)).BeginInit();
            this.panel_test.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ne_PosX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ne_PosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ne_MaxRoundTIme)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartStopBot
            // 
            this.btnStartStopBot.Location = new System.Drawing.Point(12, 5);
            this.btnStartStopBot.Name = "btnStartStopBot";
            this.btnStartStopBot.Size = new System.Drawing.Size(54, 25);
            this.btnStartStopBot.TabIndex = 9;
            this.btnStartStopBot.Text = "on";
            this.btnStartStopBot.UseVisualStyleBackColor = true;
            this.btnStartStopBot.Click += new System.EventHandler(this.btnStartStopBot_click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(394, 171);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(86, 25);
            this.button5.TabIndex = 10;
            this.button5.Tag = "clearlog";
            this.button5.Text = "Clear Log";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.btnTag_Click);
            // 
            // chb_SaveReward
            // 
            this.chb_SaveReward.AutoSize = true;
            this.chb_SaveReward.Location = new System.Drawing.Point(12, 197);
            this.chb_SaveReward.Name = "chb_SaveReward";
            this.chb_SaveReward.Size = new System.Drawing.Size(121, 21);
            this.chb_SaveReward.TabIndex = 14;
            this.chb_SaveReward.Text = "Save Rewards";
            this.chb_SaveReward.UseVisualStyleBackColor = true;
            this.chb_SaveReward.Click += new System.EventHandler(this.ReadGui);
            // 
            // tray
            // 
            this.tray.BalloonTipTitle = "PBPB";
            this.tray.ContextMenuStrip = this.trayms;
            this.tray.Text = "pbpb";
            this.tray.Visible = true;
            this.tray.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tray_Click);
            // 
            // trayms
            // 
            this.trayms.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.trayms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiStartBot,
            this.tsmiStopBot,
            this.tsmiShow,
            this.toolStripMenuItem4,
            this.tsmiBotStatus,
            this.toolStripMenuItem3,
            this.pUBGToolStripMenuItem,
            this.toolStripMenuItem5,
            this.openPicturesFolderToolStripMenuItem,
            this.tsmiAutolauchifidle,
            this.toolStripMenuItem1,
            this.tsmiExit});
            this.trayms.Name = "trayms";
            this.trayms.Size = new System.Drawing.Size(188, 236);
            this.trayms.Opening += new System.ComponentModel.CancelEventHandler(this.trayms_Opening);
            // 
            // tsmiStartBot
            // 
            this.tsmiStartBot.Name = "tsmiStartBot";
            this.tsmiStartBot.Size = new System.Drawing.Size(187, 26);
            this.tsmiStartBot.Text = "Start";
            this.tsmiStartBot.Click += new System.EventHandler(this.StartBotClick);
            // 
            // tsmiStopBot
            // 
            this.tsmiStopBot.Name = "tsmiStopBot";
            this.tsmiStopBot.Size = new System.Drawing.Size(187, 26);
            this.tsmiStopBot.Text = "Stop";
            this.tsmiStopBot.Click += new System.EventHandler(this.StopBotClick);
            // 
            // tsmiShow
            // 
            this.tsmiShow.Name = "tsmiShow";
            this.tsmiShow.Size = new System.Drawing.Size(187, 26);
            this.tsmiShow.Tag = "show";
            this.tsmiShow.Text = "Restore";
            this.tsmiShow.Click += new System.EventHandler(this.btnTag_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(184, 6);
            // 
            // tsmiBotStatus
            // 
            this.tsmiBotStatus.Enabled = false;
            this.tsmiBotStatus.Name = "tsmiBotStatus";
            this.tsmiBotStatus.Size = new System.Drawing.Size(187, 26);
            this.tsmiBotStatus.Text = "?statusbot";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(184, 6);
            // 
            // pUBGToolStripMenuItem
            // 
            this.pUBGToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.launchToolStripMenuItem,
            this.terminateToolStripMenuItem,
            this.openCoToolStripMenuItem,
            this.hideWindowToolStripMenuItem,
            this.showWindowToolStripMenuItem});
            this.pUBGToolStripMenuItem.Name = "pUBGToolStripMenuItem";
            this.pUBGToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.pUBGToolStripMenuItem.Text = "PUBG";
            // 
            // launchToolStripMenuItem
            // 
            this.launchToolStripMenuItem.Name = "launchToolStripMenuItem";
            this.launchToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.launchToolStripMenuItem.Tag = "run";
            this.launchToolStripMenuItem.Text = "Launch game";
            this.launchToolStripMenuItem.Click += new System.EventHandler(this.btnTag_Click);
            // 
            // terminateToolStripMenuItem
            // 
            this.terminateToolStripMenuItem.Name = "terminateToolStripMenuItem";
            this.terminateToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.terminateToolStripMenuItem.Tag = "kill";
            this.terminateToolStripMenuItem.Text = "End game";
            this.terminateToolStripMenuItem.Click += new System.EventHandler(this.btnTag_Click);
            // 
            // openCoToolStripMenuItem
            // 
            this.openCoToolStripMenuItem.Name = "openCoToolStripMenuItem";
            this.openCoToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.openCoToolStripMenuItem.Tag = "cfg";
            this.openCoToolStripMenuItem.Text = "Open \"GameUserSettings.ini\"";
            this.openCoToolStripMenuItem.Click += new System.EventHandler(this.btnTag_Click);
            // 
            // hideWindowToolStripMenuItem
            // 
            this.hideWindowToolStripMenuItem.Name = "hideWindowToolStripMenuItem";
            this.hideWindowToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.hideWindowToolStripMenuItem.Tag = "h";
            this.hideWindowToolStripMenuItem.Text = "Hide game window";
            this.hideWindowToolStripMenuItem.Click += new System.EventHandler(this.btnTag_Click);
            // 
            // showWindowToolStripMenuItem
            // 
            this.showWindowToolStripMenuItem.Name = "showWindowToolStripMenuItem";
            this.showWindowToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.showWindowToolStripMenuItem.Tag = "s";
            this.showWindowToolStripMenuItem.Text = "Restore game window";
            this.showWindowToolStripMenuItem.Click += new System.EventHandler(this.btnTag_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(184, 6);
            // 
            // openPicturesFolderToolStripMenuItem
            // 
            this.openPicturesFolderToolStripMenuItem.Name = "openPicturesFolderToolStripMenuItem";
            this.openPicturesFolderToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.openPicturesFolderToolStripMenuItem.Tag = "openrewards";
            this.openPicturesFolderToolStripMenuItem.Text = "Show Rewards...";
            this.openPicturesFolderToolStripMenuItem.Click += new System.EventHandler(this.OpenRewardsFolderClick);
            // 
            // tsmiAutolauchifidle
            // 
            this.tsmiAutolauchifidle.Checked = true;
            this.tsmiAutolauchifidle.CheckOnClick = true;
            this.tsmiAutolauchifidle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiAutolauchifidle.Name = "tsmiAutolauchifidle";
            this.tsmiAutolauchifidle.Size = new System.Drawing.Size(187, 26);
            this.tsmiAutolauchifidle.Tag = "autolauchifidle";
            this.tsmiAutolauchifidle.Text = "Autolauch if Idle";
            this.tsmiAutolauchifidle.Click += new System.EventHandler(this.btnTag_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(184, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(187, 26);
            this.tsmiExit.Tag = "exit";
            this.tsmiExit.Text = "Exit";
            this.tsmiExit.Click += new System.EventHandler(this.btnTag_Click);
            // 
            // tmrIdleCheck
            // 
            this.tmrIdleCheck.Enabled = true;
            this.tmrIdleCheck.Tick += new System.EventHandler(this.tmrIdleCheck_Tick);
            // 
            // chb_AutoStartOnIdle
            // 
            this.chb_AutoStartOnIdle.AutoSize = true;
            this.chb_AutoStartOnIdle.Location = new System.Drawing.Point(12, 250);
            this.chb_AutoStartOnIdle.Name = "chb_AutoStartOnIdle";
            this.chb_AutoStartOnIdle.Size = new System.Drawing.Size(189, 21);
            this.chb_AutoStartOnIdle.TabIndex = 17;
            this.chb_AutoStartOnIdle.Text = "Autolauch if Idle (minuts):";
            this.chb_AutoStartOnIdle.UseVisualStyleBackColor = true;
            this.chb_AutoStartOnIdle.Click += new System.EventHandler(this.ReadGui);
            // 
            // ne_MaxIdle
            // 
            this.ne_MaxIdle.Location = new System.Drawing.Point(203, 249);
            this.ne_MaxIdle.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.ne_MaxIdle.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ne_MaxIdle.Name = "ne_MaxIdle";
            this.ne_MaxIdle.Size = new System.Drawing.Size(74, 22);
            this.ne_MaxIdle.TabIndex = 18;
            this.ne_MaxIdle.Value = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.ne_MaxIdle.Click += new System.EventHandler(this.ReadGui);
            // 
            // txLog
            // 
            this.txLog.BackColor = System.Drawing.SystemColors.Control;
            this.txLog.Location = new System.Drawing.Point(12, 36);
            this.txLog.Multiline = true;
            this.txLog.Name = "txLog";
            this.txLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txLog.Size = new System.Drawing.Size(472, 128);
            this.txLog.TabIndex = 8;
            this.txLog.Tag = "txlog";
            this.txLog.DoubleClick += new System.EventHandler(this.btnTag_Click);
            // 
            // panel_test
            // 
            this.panel_test.Controls.Add(this.button7);
            this.panel_test.Controls.Add(this.button6);
            this.panel_test.Controls.Add(this.button3);
            this.panel_test.Controls.Add(this.button2);
            this.panel_test.Controls.Add(this.button1);
            this.panel_test.Controls.Add(this.btnttt);
            this.panel_test.Location = new System.Drawing.Point(12, 305);
            this.panel_test.Name = "panel_test";
            this.panel_test.Size = new System.Drawing.Size(468, 93);
            this.panel_test.TabIndex = 21;
            this.panel_test.Visible = false;
            // 
            // button7
            // 
            this.button7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button7.Location = new System.Drawing.Point(3, 3);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(86, 25);
            this.button7.TabIndex = 13;
            this.button7.Tag = "run";
            this.button7.Text = "Run";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.btnTag_Click);
            // 
            // button6
            // 
            this.button6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button6.Location = new System.Drawing.Point(95, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(86, 25);
            this.button6.TabIndex = 12;
            this.button6.Tag = "kill";
            this.button6.Text = "Kill";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.btnTag_Click);
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button3.Location = new System.Drawing.Point(187, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(35, 25);
            this.button3.TabIndex = 8;
            this.button3.Tag = "scr";
            this.button3.Text = "scr";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnTag_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.Location = new System.Drawing.Point(228, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(70, 25);
            this.button2.TabIndex = 7;
            this.button2.Tag = "fromf";
            this.button2.Text = "from f";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnTag_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(304, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 25);
            this.button1.TabIndex = 0;
            this.button1.Tag = "froms";
            this.button1.Text = "from s";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnTag_Click);
            // 
            // btnttt
            // 
            this.btnttt.Location = new System.Drawing.Point(396, 3);
            this.btnttt.Name = "btnttt";
            this.btnttt.Size = new System.Drawing.Size(63, 25);
            this.btnttt.TabIndex = 19;
            this.btnttt.Text = "ttt";
            this.btnttt.UseVisualStyleBackColor = true;
            this.btnttt.Click += new System.EventHandler(this.btnttt_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(394, 209);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(86, 25);
            this.button4.TabIndex = 23;
            this.button4.Tag = "about";
            this.button4.Text = "?";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btnTag_Click);
            // 
            // ne_PosX
            // 
            this.ne_PosX.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.ne_PosX.Location = new System.Drawing.Point(299, 8);
            this.ne_PosX.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.ne_PosX.Name = "ne_PosX";
            this.ne_PosX.Size = new System.Drawing.Size(74, 22);
            this.ne_PosX.TabIndex = 24;
            this.ne_PosX.Value = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.ne_PosX.Click += new System.EventHandler(this.ReadGui);
            // 
            // ne_PosY
            // 
            this.ne_PosY.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.ne_PosY.Location = new System.Drawing.Point(406, 8);
            this.ne_PosY.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.ne_PosY.Name = "ne_PosY";
            this.ne_PosY.Size = new System.Drawing.Size(74, 22);
            this.ne_PosY.TabIndex = 25;
            this.ne_PosY.Value = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.ne_PosY.Click += new System.EventHandler(this.ReadGui);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(272, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 17);
            this.label1.TabIndex = 26;
            this.label1.Tag = "X";
            this.label1.Text = "X:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(379, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 17);
            this.label2.TabIndex = 27;
            this.label2.Tag = "Y";
            this.label2.Text = "Y:";
            this.label2.Click += new System.EventHandler(this.label1_Click);
            // 
            // chb_PassiveMode
            // 
            this.chb_PassiveMode.AutoSize = true;
            this.chb_PassiveMode.Location = new System.Drawing.Point(12, 170);
            this.chb_PassiveMode.Name = "chb_PassiveMode";
            this.chb_PassiveMode.Size = new System.Drawing.Size(118, 21);
            this.chb_PassiveMode.TabIndex = 28;
            this.chb_PassiveMode.Text = "Passive mode";
            this.chb_PassiveMode.UseVisualStyleBackColor = true;
            this.chb_PassiveMode.Click += new System.EventHandler(this.ReadGui);
            // 
            // chb_HiddenMode
            // 
            this.chb_HiddenMode.AutoSize = true;
            this.chb_HiddenMode.Location = new System.Drawing.Point(207, 9);
            this.chb_HiddenMode.Name = "chb_HiddenMode";
            this.chb_HiddenMode.Size = new System.Drawing.Size(59, 21);
            this.chb_HiddenMode.TabIndex = 29;
            this.chb_HiddenMode.Text = "Hide";
            this.chb_HiddenMode.UseVisualStyleBackColor = true;
            this.chb_HiddenMode.Click += new System.EventHandler(this.ReadGui);
            // 
            // chb_CanKillSteam
            // 
            this.chb_CanKillSteam.AutoSize = true;
            this.chb_CanKillSteam.Location = new System.Drawing.Point(12, 223);
            this.chb_CanKillSteam.Name = "chb_CanKillSteam";
            this.chb_CanKillSteam.Size = new System.Drawing.Size(149, 21);
            this.chb_CanKillSteam.TabIndex = 30;
            this.chb_CanKillSteam.Text = "Allow restart steam";
            this.chb_CanKillSteam.UseVisualStyleBackColor = true;
            this.chb_CanKillSteam.Click += new System.EventHandler(this.ReadGui);
            // 
            // cbox_PubgInput
            // 
            this.cbox_PubgInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbox_PubgInput.FormattingEnabled = true;
            this.cbox_PubgInput.Items.AddRange(new object[] {
            "event",
            "message"});
            this.cbox_PubgInput.Location = new System.Drawing.Point(80, 6);
            this.cbox_PubgInput.Name = "cbox_PubgInput";
            this.cbox_PubgInput.Size = new System.Drawing.Size(121, 24);
            this.cbox_PubgInput.TabIndex = 31;
            this.cbox_PubgInput.SelectedIndexChanged += new System.EventHandler(this.cbox_PubgInput_SelectedIndexChanged);
            // 
            // chb_view
            // 
            this.chb_view.AutoSize = true;
            this.chb_view.Location = new System.Drawing.Point(207, 170);
            this.chb_view.Name = "chb_view";
            this.chb_view.Size = new System.Drawing.Size(59, 21);
            this.chb_view.TabIndex = 32;
            this.chb_view.Tag = "view";
            this.chb_view.Text = "View";
            this.chb_view.UseVisualStyleBackColor = true;
            this.chb_view.Click += new System.EventHandler(this.ReadGui);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(394, 247);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(86, 25);
            this.button8.TabIndex = 34;
            this.button8.Tag = "exit";
            this.button8.Text = "Exit";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.btnTag_Click);
            // 
            // ne_MaxRoundTIme
            // 
            this.ne_MaxRoundTIme.Location = new System.Drawing.Point(203, 277);
            this.ne_MaxRoundTIme.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.ne_MaxRoundTIme.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.ne_MaxRoundTIme.Name = "ne_MaxRoundTIme";
            this.ne_MaxRoundTIme.Size = new System.Drawing.Size(74, 22);
            this.ne_MaxRoundTIme.TabIndex = 35;
            this.ne_MaxRoundTIme.Value = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.ne_MaxRoundTIme.Click += new System.EventHandler(this.ReadGui);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 279);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 17);
            this.label3.TabIndex = 36;
            this.label3.Tag = "X";
            this.label3.Text = "Max Round Time (minutes):";
            // 
            // PanelView
            // 
            this.PanelView.BackgroundImage = global::pbpb.Properties.Resources.banner;
            this.PanelView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PanelView.Location = new System.Drawing.Point(12, 36);
            this.PanelView.Name = "PanelView";
            this.PanelView.Size = new System.Drawing.Size(472, 127);
            this.PanelView.TabIndex = 33;
            this.PanelView.Tag = "pview";
            this.PanelView.DoubleClick += new System.EventHandler(this.btnTag_Click);
            // 
            // lab_CurrentRounTime
            // 
            this.lab_CurrentRounTime.AutoSize = true;
            this.lab_CurrentRounTime.Location = new System.Drawing.Point(296, 171);
            this.lab_CurrentRounTime.Name = "lab_CurrentRounTime";
            this.lab_CurrentRounTime.Size = new System.Drawing.Size(42, 17);
            this.lab_CurrentRounTime.TabIndex = 37;
            this.lab_CurrentRounTime.Tag = "";
            this.lab_CurrentRounTime.Text = "None";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 343);
            this.Controls.Add(this.lab_CurrentRounTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ne_MaxRoundTIme);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.PanelView);
            this.Controls.Add(this.chb_view);
            this.Controls.Add(this.cbox_PubgInput);
            this.Controls.Add(this.chb_CanKillSteam);
            this.Controls.Add(this.chb_HiddenMode);
            this.Controls.Add(this.chb_PassiveMode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ne_PosY);
            this.Controls.Add(this.ne_PosX);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.panel_test);
            this.Controls.Add(this.txLog);
            this.Controls.Add(this.btnStartStopBot);
            this.Controls.Add(this.ne_MaxIdle);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.chb_AutoStartOnIdle);
            this.Controls.Add(this.chb_SaveReward);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PBPB //by SN";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.trayms.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ne_MaxIdle)).EndInit();
            this.panel_test.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ne_PosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ne_PosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ne_MaxRoundTIme)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnStartStopBot;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.CheckBox chb_SaveReward;
        private System.Windows.Forms.NotifyIcon tray;
        private System.Windows.Forms.ContextMenuStrip trayms;
        private System.Windows.Forms.ToolStripMenuItem tsmiStartBot;
        private System.Windows.Forms.ToolStripMenuItem tsmiStopBot;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem tsmiBotStatus;
        private System.Windows.Forms.ToolStripMenuItem tsmiShow;
        private System.Windows.Forms.ToolStripMenuItem openPicturesFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.Timer tmrIdleCheck;
        private System.Windows.Forms.CheckBox chb_AutoStartOnIdle;
        private System.Windows.Forms.NumericUpDown ne_MaxIdle;
        private System.Windows.Forms.ToolStripMenuItem tsmiAutolauchifidle;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.Button btnttt;
        private System.Windows.Forms.TextBox txLog;
        private System.Windows.Forms.FlowLayoutPanel panel_test;
        private System.Windows.Forms.ToolStripMenuItem pUBGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem launchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem terminateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.NumericUpDown ne_PosX;
        private System.Windows.Forms.NumericUpDown ne_PosY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chb_PassiveMode;
        private System.Windows.Forms.CheckBox chb_HiddenMode;
        private System.Windows.Forms.CheckBox chb_CanKillSteam;
        private System.Windows.Forms.ComboBox cbox_PubgInput;
        private System.Windows.Forms.CheckBox chb_view;
        public PanelDoubleBuffered PanelView;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.NumericUpDown ne_MaxRoundTIme;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lab_CurrentRounTime;
    }
}

