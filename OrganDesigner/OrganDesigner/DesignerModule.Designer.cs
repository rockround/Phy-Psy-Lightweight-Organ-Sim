namespace OrganDesigner
{
    partial class DesignerModule
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
            this.Terminate = new System.Windows.Forms.Button();
            this.Simulate = new System.Windows.Forms.Button();
            this.StartHealth = new System.Windows.Forms.TextBox();
            this.Metabolism = new System.Windows.Forms.TextBox();
            this.PowerConsumption = new System.Windows.Forms.TextBox();
            this.MaxM = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.MaxCharge = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.DrainRate = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.BetaRate = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.MaxBoostCount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.FatGrowth = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.FatBreakdown = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.BaseBps = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.Homeostasis = new System.Windows.Forms.TextBox();
            this.OrganSelector = new System.Windows.Forms.ListBox();
            this.StructureCore = new System.Windows.Forms.ProgressBar();
            this.BetaCore = new System.Windows.Forms.ProgressBar();
            this.WriterCore = new System.Windows.Forms.ProgressBar();
            this.CapacitorCore = new System.Windows.Forms.ProgressBar();
            this.PumpCore = new System.Windows.Forms.ProgressBar();
            this.VisionCore = new System.Windows.Forms.ProgressBar();
            this.MotorCore = new System.Windows.Forms.ProgressBar();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.MotorPower = new System.Windows.Forms.ProgressBar();
            this.VisionPower = new System.Windows.Forms.ProgressBar();
            this.PumpPower = new System.Windows.Forms.ProgressBar();
            this.CapacitorPower = new System.Windows.Forms.ProgressBar();
            this.WriterPower = new System.Windows.Forms.ProgressBar();
            this.BetaPower = new System.Windows.Forms.ProgressBar();
            this.StructurePower = new System.Windows.Forms.ProgressBar();
            this.label23 = new System.Windows.Forms.Label();
            this.MotorTemp = new System.Windows.Forms.ProgressBar();
            this.VisionTemp = new System.Windows.Forms.ProgressBar();
            this.PumpTemp = new System.Windows.Forms.ProgressBar();
            this.CapacitorTemp = new System.Windows.Forms.ProgressBar();
            this.WriterTemp = new System.Windows.Forms.ProgressBar();
            this.BetaTemp = new System.Windows.Forms.ProgressBar();
            this.StructureTemp = new System.Windows.Forms.ProgressBar();
            this.CapacitorCharge = new System.Windows.Forms.ProgressBar();
            this.WriterCharge = new System.Windows.Forms.ProgressBar();
            this.MotorCharge = new System.Windows.Forms.ProgressBar();
            this.label24 = new System.Windows.Forms.Label();
            this.PauseButton = new System.Windows.Forms.Button();
            this.PlayButton = new System.Windows.Forms.Button();
            this.SimRate = new System.Windows.Forms.NumericUpDown();
            this.SimRateLabel = new System.Windows.Forms.Label();
            this.HeartRate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SimRate)).BeginInit();
            this.SuspendLayout();
            // 
            // Terminate
            // 
            this.Terminate.Location = new System.Drawing.Point(769, 610);
            this.Terminate.Name = "Terminate";
            this.Terminate.Size = new System.Drawing.Size(75, 23);
            this.Terminate.TabIndex = 0;
            this.Terminate.Text = "Terminate";
            this.Terminate.UseVisualStyleBackColor = true;
            this.Terminate.Click += new System.EventHandler(this.killMainLoop);
            // 
            // Simulate
            // 
            this.Simulate.Location = new System.Drawing.Point(395, 610);
            this.Simulate.Name = "Simulate";
            this.Simulate.Size = new System.Drawing.Size(75, 23);
            this.Simulate.TabIndex = 1;
            this.Simulate.Text = "Simulate";
            this.Simulate.UseVisualStyleBackColor = true;
            this.Simulate.Click += new System.EventHandler(this.startLoop);
            // 
            // StartHealth
            // 
            this.StartHealth.Location = new System.Drawing.Point(122, 101);
            this.StartHealth.Name = "StartHealth";
            this.StartHealth.Size = new System.Drawing.Size(100, 20);
            this.StartHealth.TabIndex = 2;
            // 
            // Metabolism
            // 
            this.Metabolism.Location = new System.Drawing.Point(122, 138);
            this.Metabolism.Name = "Metabolism";
            this.Metabolism.Size = new System.Drawing.Size(100, 20);
            this.Metabolism.TabIndex = 3;
            // 
            // PowerConsumption
            // 
            this.PowerConsumption.Location = new System.Drawing.Point(122, 173);
            this.PowerConsumption.Name = "PowerConsumption";
            this.PowerConsumption.Size = new System.Drawing.Size(100, 20);
            this.PowerConsumption.TabIndex = 4;
            // 
            // MaxM
            // 
            this.MaxM.Location = new System.Drawing.Point(122, 210);
            this.MaxM.Name = "MaxM";
            this.MaxM.Size = new System.Drawing.Size(100, 20);
            this.MaxM.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(122, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Comma Separated Parameters";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "StartHealth";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Metabolism";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "PowerConsumption";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(64, 210);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "MaxM";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(42, 249);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "MaxCharge";
            // 
            // MaxCharge
            // 
            this.MaxCharge.Location = new System.Drawing.Point(122, 249);
            this.MaxCharge.Name = "MaxCharge";
            this.MaxCharge.Size = new System.Drawing.Size(100, 20);
            this.MaxCharge.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(56, 378);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "DrainRate";
            // 
            // DrainRate
            // 
            this.DrainRate.Location = new System.Drawing.Point(122, 378);
            this.DrainRate.Name = "DrainRate";
            this.DrainRate.Size = new System.Drawing.Size(100, 20);
            this.DrainRate.TabIndex = 21;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(56, 338);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "BetaRate";
            // 
            // BetaRate
            // 
            this.BetaRate.Location = new System.Drawing.Point(122, 338);
            this.BetaRate.Name = "BetaRate";
            this.BetaRate.Size = new System.Drawing.Size(100, 20);
            this.BetaRate.TabIndex = 23;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(33, 295);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(82, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "MaxBoostCount";
            // 
            // MaxBoostCount
            // 
            this.MaxBoostCount.Location = new System.Drawing.Point(122, 292);
            this.MaxBoostCount.Name = "MaxBoostCount";
            this.MaxBoostCount.Size = new System.Drawing.Size(100, 20);
            this.MaxBoostCount.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(56, 418);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "FatGrowth";
            // 
            // FatGrowth
            // 
            this.FatGrowth.Location = new System.Drawing.Point(122, 418);
            this.FatGrowth.Name = "FatGrowth";
            this.FatGrowth.Size = new System.Drawing.Size(100, 20);
            this.FatGrowth.TabIndex = 31;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(36, 467);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "FatBreakdown";
            // 
            // FatBreakdown
            // 
            this.FatBreakdown.Location = new System.Drawing.Point(122, 464);
            this.FatBreakdown.Name = "FatBreakdown";
            this.FatBreakdown.Size = new System.Drawing.Size(100, 20);
            this.FatBreakdown.TabIndex = 29;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(56, 504);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 28;
            this.label9.Text = "BaseBps";
            // 
            // BaseBps
            // 
            this.BaseBps.Location = new System.Drawing.Point(122, 504);
            this.BaseBps.Name = "BaseBps";
            this.BaseBps.Size = new System.Drawing.Size(100, 20);
            this.BaseBps.TabIndex = 27;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(44, 550);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "Homeostasis";
            // 
            // Homeostasis
            // 
            this.Homeostasis.Location = new System.Drawing.Point(122, 547);
            this.Homeostasis.Name = "Homeostasis";
            this.Homeostasis.Size = new System.Drawing.Size(100, 20);
            this.Homeostasis.TabIndex = 33;
            // 
            // OrganSelector
            // 
            this.OrganSelector.FormattingEnabled = true;
            this.OrganSelector.Items.AddRange(new object[] {
            "Writer",
            "Capacitor",
            "Motor",
            "Structure",
            "Beta",
            "Pump",
            "Vision"});
            this.OrganSelector.Location = new System.Drawing.Point(274, 94);
            this.OrganSelector.Name = "OrganSelector";
            this.OrganSelector.Size = new System.Drawing.Size(120, 95);
            this.OrganSelector.TabIndex = 35;
            this.OrganSelector.SelectedIndexChanged += new System.EventHandler(this.OrganSelection);
            // 
            // StructureCore
            // 
            this.StructureCore.Location = new System.Drawing.Point(541, 200);
            this.StructureCore.Name = "StructureCore";
            this.StructureCore.Size = new System.Drawing.Size(100, 23);
            this.StructureCore.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.StructureCore.TabIndex = 36;
            // 
            // BetaCore
            // 
            this.BetaCore.Location = new System.Drawing.Point(541, 388);
            this.BetaCore.Name = "BetaCore";
            this.BetaCore.Size = new System.Drawing.Size(100, 23);
            this.BetaCore.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.BetaCore.TabIndex = 38;
            // 
            // WriterCore
            // 
            this.WriterCore.Location = new System.Drawing.Point(541, 285);
            this.WriterCore.Name = "WriterCore";
            this.WriterCore.Size = new System.Drawing.Size(100, 23);
            this.WriterCore.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.WriterCore.TabIndex = 40;
            // 
            // CapacitorCore
            // 
            this.CapacitorCore.Location = new System.Drawing.Point(541, 239);
            this.CapacitorCore.Name = "CapacitorCore";
            this.CapacitorCore.Size = new System.Drawing.Size(100, 23);
            this.CapacitorCore.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.CapacitorCore.TabIndex = 41;
            // 
            // PumpCore
            // 
            this.PumpCore.Location = new System.Drawing.Point(541, 441);
            this.PumpCore.Name = "PumpCore";
            this.PumpCore.Size = new System.Drawing.Size(100, 23);
            this.PumpCore.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.PumpCore.TabIndex = 44;
            // 
            // VisionCore
            // 
            this.VisionCore.Location = new System.Drawing.Point(541, 494);
            this.VisionCore.Name = "VisionCore";
            this.VisionCore.Size = new System.Drawing.Size(100, 23);
            this.VisionCore.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.VisionCore.TabIndex = 46;
            // 
            // MotorCore
            // 
            this.MotorCore.Location = new System.Drawing.Point(541, 338);
            this.MotorCore.Name = "MotorCore";
            this.MotorCore.Size = new System.Drawing.Size(100, 23);
            this.MotorCore.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.MotorCore.TabIndex = 48;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(428, 200);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(50, 13);
            this.label14.TabIndex = 50;
            this.label14.Text = "Structure";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(429, 239);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 13);
            this.label15.TabIndex = 51;
            this.label15.Text = "Capacitor";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(429, 292);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 13);
            this.label16.TabIndex = 52;
            this.label16.Text = "Writer";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(429, 338);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(34, 13);
            this.label17.TabIndex = 53;
            this.label17.Text = "Motor";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(429, 388);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(29, 13);
            this.label18.TabIndex = 54;
            this.label18.Text = "Beta";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(429, 441);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(34, 13);
            this.label19.TabIndex = 55;
            this.label19.Text = "Pump";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(429, 494);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(35, 13);
            this.label20.TabIndex = 56;
            this.label20.Text = "Vision";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(562, 138);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(29, 13);
            this.label21.TabIndex = 57;
            this.label21.Text = "Core";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(709, 138);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(37, 13);
            this.label22.TabIndex = 65;
            this.label22.Text = "Power";
            // 
            // MotorPower
            // 
            this.MotorPower.Location = new System.Drawing.Point(688, 338);
            this.MotorPower.Name = "MotorPower";
            this.MotorPower.Size = new System.Drawing.Size(100, 23);
            this.MotorPower.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.MotorPower.TabIndex = 64;
            // 
            // VisionPower
            // 
            this.VisionPower.Location = new System.Drawing.Point(688, 494);
            this.VisionPower.Name = "VisionPower";
            this.VisionPower.Size = new System.Drawing.Size(100, 23);
            this.VisionPower.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.VisionPower.TabIndex = 63;
            // 
            // PumpPower
            // 
            this.PumpPower.Location = new System.Drawing.Point(688, 441);
            this.PumpPower.Name = "PumpPower";
            this.PumpPower.Size = new System.Drawing.Size(100, 23);
            this.PumpPower.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.PumpPower.TabIndex = 62;
            // 
            // CapacitorPower
            // 
            this.CapacitorPower.Location = new System.Drawing.Point(688, 239);
            this.CapacitorPower.Name = "CapacitorPower";
            this.CapacitorPower.Size = new System.Drawing.Size(100, 23);
            this.CapacitorPower.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.CapacitorPower.TabIndex = 61;
            // 
            // WriterPower
            // 
            this.WriterPower.Location = new System.Drawing.Point(688, 285);
            this.WriterPower.Name = "WriterPower";
            this.WriterPower.Size = new System.Drawing.Size(100, 23);
            this.WriterPower.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.WriterPower.TabIndex = 60;
            // 
            // BetaPower
            // 
            this.BetaPower.Location = new System.Drawing.Point(688, 388);
            this.BetaPower.Name = "BetaPower";
            this.BetaPower.Size = new System.Drawing.Size(100, 23);
            this.BetaPower.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.BetaPower.TabIndex = 59;
            // 
            // StructurePower
            // 
            this.StructurePower.Location = new System.Drawing.Point(688, 200);
            this.StructurePower.Name = "StructurePower";
            this.StructurePower.Size = new System.Drawing.Size(100, 23);
            this.StructurePower.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.StructurePower.TabIndex = 58;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(865, 138);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(67, 13);
            this.label23.TabIndex = 73;
            this.label23.Text = "Temperature";
            // 
            // MotorTemp
            // 
            this.MotorTemp.Location = new System.Drawing.Point(844, 338);
            this.MotorTemp.Name = "MotorTemp";
            this.MotorTemp.Size = new System.Drawing.Size(100, 23);
            this.MotorTemp.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.MotorTemp.TabIndex = 72;
            // 
            // VisionTemp
            // 
            this.VisionTemp.Location = new System.Drawing.Point(844, 494);
            this.VisionTemp.Name = "VisionTemp";
            this.VisionTemp.Size = new System.Drawing.Size(100, 23);
            this.VisionTemp.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.VisionTemp.TabIndex = 71;
            // 
            // PumpTemp
            // 
            this.PumpTemp.Location = new System.Drawing.Point(844, 441);
            this.PumpTemp.Name = "PumpTemp";
            this.PumpTemp.Size = new System.Drawing.Size(100, 23);
            this.PumpTemp.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.PumpTemp.TabIndex = 70;
            // 
            // CapacitorTemp
            // 
            this.CapacitorTemp.Location = new System.Drawing.Point(844, 239);
            this.CapacitorTemp.Name = "CapacitorTemp";
            this.CapacitorTemp.Size = new System.Drawing.Size(100, 23);
            this.CapacitorTemp.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.CapacitorTemp.TabIndex = 69;
            // 
            // WriterTemp
            // 
            this.WriterTemp.Location = new System.Drawing.Point(844, 285);
            this.WriterTemp.Name = "WriterTemp";
            this.WriterTemp.Size = new System.Drawing.Size(100, 23);
            this.WriterTemp.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.WriterTemp.TabIndex = 68;
            // 
            // BetaTemp
            // 
            this.BetaTemp.Location = new System.Drawing.Point(844, 388);
            this.BetaTemp.Name = "BetaTemp";
            this.BetaTemp.Size = new System.Drawing.Size(100, 23);
            this.BetaTemp.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.BetaTemp.TabIndex = 67;
            // 
            // StructureTemp
            // 
            this.StructureTemp.Location = new System.Drawing.Point(844, 200);
            this.StructureTemp.Name = "StructureTemp";
            this.StructureTemp.Size = new System.Drawing.Size(100, 23);
            this.StructureTemp.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.StructureTemp.TabIndex = 66;
            // 
            // CapacitorCharge
            // 
            this.CapacitorCharge.Location = new System.Drawing.Point(981, 239);
            this.CapacitorCharge.Name = "CapacitorCharge";
            this.CapacitorCharge.Size = new System.Drawing.Size(100, 23);
            this.CapacitorCharge.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.CapacitorCharge.TabIndex = 74;
            // 
            // WriterCharge
            // 
            this.WriterCharge.Location = new System.Drawing.Point(981, 285);
            this.WriterCharge.Name = "WriterCharge";
            this.WriterCharge.Size = new System.Drawing.Size(100, 23);
            this.WriterCharge.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.WriterCharge.TabIndex = 75;
            // 
            // MotorCharge
            // 
            this.MotorCharge.Location = new System.Drawing.Point(981, 338);
            this.MotorCharge.Name = "MotorCharge";
            this.MotorCharge.Size = new System.Drawing.Size(100, 23);
            this.MotorCharge.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.MotorCharge.TabIndex = 76;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(1005, 138);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(41, 13);
            this.label24.TabIndex = 77;
            this.label24.Text = "Charge";
            // 
            // PauseButton
            // 
            this.PauseButton.Location = new System.Drawing.Point(606, 591);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(75, 23);
            this.PauseButton.TabIndex = 78;
            this.PauseButton.Text = "Pause";
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(606, 635);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(75, 23);
            this.PlayButton.TabIndex = 79;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // SimRate
            // 
            this.SimRate.DecimalPlaces = 2;
            this.SimRate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.SimRate.Location = new System.Drawing.Point(498, 610);
            this.SimRate.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.SimRate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.SimRate.Name = "SimRate";
            this.SimRate.Size = new System.Drawing.Size(74, 20);
            this.SimRate.TabIndex = 80;
            this.SimRate.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // SimRateLabel
            // 
            this.SimRateLabel.AutoSize = true;
            this.SimRateLabel.Location = new System.Drawing.Point(498, 579);
            this.SimRateLabel.Name = "SimRateLabel";
            this.SimRateLabel.Size = new System.Drawing.Size(81, 13);
            this.SimRateLabel.TabIndex = 81;
            this.SimRateLabel.Text = "Simulation Rate";
            // 
            // HeartRate
            // 
            this.HeartRate.AutoSize = true;
            this.HeartRate.Location = new System.Drawing.Point(541, 41);
            this.HeartRate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.HeartRate.Name = "HeartRate";
            this.HeartRate.Size = new System.Drawing.Size(13, 13);
            this.HeartRate.TabIndex = 82;
            this.HeartRate.Text = "0";
            // 
            // DesignerModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 710);
            this.Controls.Add(this.HeartRate);
            this.Controls.Add(this.SimRateLabel);
            this.Controls.Add(this.SimRate);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.MotorCharge);
            this.Controls.Add(this.WriterCharge);
            this.Controls.Add(this.CapacitorCharge);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.MotorTemp);
            this.Controls.Add(this.VisionTemp);
            this.Controls.Add(this.PumpTemp);
            this.Controls.Add(this.CapacitorTemp);
            this.Controls.Add(this.WriterTemp);
            this.Controls.Add(this.BetaTemp);
            this.Controls.Add(this.StructureTemp);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.MotorPower);
            this.Controls.Add(this.VisionPower);
            this.Controls.Add(this.PumpPower);
            this.Controls.Add(this.CapacitorPower);
            this.Controls.Add(this.WriterPower);
            this.Controls.Add(this.BetaPower);
            this.Controls.Add(this.StructurePower);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.MotorCore);
            this.Controls.Add(this.VisionCore);
            this.Controls.Add(this.PumpCore);
            this.Controls.Add(this.CapacitorCore);
            this.Controls.Add(this.WriterCore);
            this.Controls.Add(this.BetaCore);
            this.Controls.Add(this.StructureCore);
            this.Controls.Add(this.OrganSelector);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.Homeostasis);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.FatGrowth);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.FatBreakdown);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.BaseBps);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.MaxBoostCount);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.BetaRate);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.DrainRate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.MaxCharge);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MaxM);
            this.Controls.Add(this.PowerConsumption);
            this.Controls.Add(this.Metabolism);
            this.Controls.Add(this.StartHealth);
            this.Controls.Add(this.Simulate);
            this.Controls.Add(this.Terminate);
            this.Name = "DesignerModule";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.onLoad);
            ((System.ComponentModel.ISupportInitialize)(this.SimRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Terminate;
        private System.Windows.Forms.Button Simulate;
        private System.Windows.Forms.TextBox StartHealth;
        private System.Windows.Forms.TextBox Metabolism;
        private System.Windows.Forms.TextBox PowerConsumption;
        private System.Windows.Forms.TextBox MaxM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox MaxCharge;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox DrainRate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox BetaRate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox MaxBoostCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox FatGrowth;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox FatBreakdown;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox BaseBps;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox Homeostasis;
        private System.Windows.Forms.ListBox OrganSelector;
        private System.Windows.Forms.ProgressBar StructureCore;
        private System.Windows.Forms.ProgressBar BetaCore;
        private System.Windows.Forms.ProgressBar WriterCore;
        private System.Windows.Forms.ProgressBar CapacitorCore;
        private System.Windows.Forms.ProgressBar PumpCore;
        private System.Windows.Forms.ProgressBar VisionCore;
        private System.Windows.Forms.ProgressBar MotorCore;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ProgressBar MotorPower;
        private System.Windows.Forms.ProgressBar VisionPower;
        private System.Windows.Forms.ProgressBar PumpPower;
        private System.Windows.Forms.ProgressBar CapacitorPower;
        private System.Windows.Forms.ProgressBar WriterPower;
        private System.Windows.Forms.ProgressBar BetaPower;
        private System.Windows.Forms.ProgressBar StructurePower;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ProgressBar MotorTemp;
        private System.Windows.Forms.ProgressBar VisionTemp;
        private System.Windows.Forms.ProgressBar PumpTemp;
        private System.Windows.Forms.ProgressBar CapacitorTemp;
        private System.Windows.Forms.ProgressBar WriterTemp;
        private System.Windows.Forms.ProgressBar BetaTemp;
        private System.Windows.Forms.ProgressBar StructureTemp;
        private System.Windows.Forms.ProgressBar CapacitorCharge;
        private System.Windows.Forms.ProgressBar WriterCharge;
        private System.Windows.Forms.ProgressBar MotorCharge;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.NumericUpDown SimRate;
        private System.Windows.Forms.Label SimRateLabel;
        private System.Windows.Forms.Label HeartRate;
    }
}

