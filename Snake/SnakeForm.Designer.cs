
namespace Snake {
    partial class SnakeForm {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.StartButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.CounterLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DijkstraControlRb = new System.Windows.Forms.RadioButton();
            this.ManualControlRb = new System.Windows.Forms.RadioButton();
            this.GameSpeedInput = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.RecreateButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.HeightSizeInput = new System.Windows.Forms.TextBox();
            this.WidthSizeInput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CellSizeInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.StartButton.Location = new System.Drawing.Point(8, 28);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(90, 40);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Старт";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            this.StartButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button1_KeyDown);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CounterLabel
            // 
            this.CounterLabel.AutoSize = true;
            this.CounterLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CounterLabel.Location = new System.Drawing.Point(72, 78);
            this.CounterLabel.Name = "CounterLabel";
            this.CounterLabel.Size = new System.Drawing.Size(19, 21);
            this.CounterLabel.TabIndex = 2;
            this.CounterLabel.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(9, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Счёт:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.GameSpeedInput);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.RecreateButton);
            this.groupBox1.Controls.Add(this.StopButton);
            this.groupBox1.Controls.Add(this.HeightSizeInput);
            this.groupBox1.Controls.Add(this.WidthSizeInput);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.CellSizeInput);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.CounterLabel);
            this.groupBox1.Controls.Add(this.StartButton);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(0, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 706);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.DijkstraControlRb);
            this.groupBox3.Controls.Add(this.ManualControlRb);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox3.Location = new System.Drawing.Point(6, 397);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(199, 309);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Алгоритм";
            // 
            // DijkstraControlRb
            // 
            this.DijkstraControlRb.AutoSize = true;
            this.DijkstraControlRb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DijkstraControlRb.Location = new System.Drawing.Point(6, 59);
            this.DijkstraControlRb.Name = "DijkstraControlRb";
            this.DijkstraControlRb.Size = new System.Drawing.Size(171, 25);
            this.DijkstraControlRb.TabIndex = 1;
            this.DijkstraControlRb.Text = "Алгоритм Дейкстры";
            this.DijkstraControlRb.UseVisualStyleBackColor = true;
            this.DijkstraControlRb.CheckedChanged += new System.EventHandler(this.DijkstraControlRb_CheckedChanged);
            // 
            // ManualControlRb
            // 
            this.ManualControlRb.AutoSize = true;
            this.ManualControlRb.Checked = true;
            this.ManualControlRb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ManualControlRb.Location = new System.Drawing.Point(6, 28);
            this.ManualControlRb.Name = "ManualControlRb";
            this.ManualControlRb.Size = new System.Drawing.Size(168, 25);
            this.ManualControlRb.TabIndex = 0;
            this.ManualControlRb.TabStop = true;
            this.ManualControlRb.Text = "Ручное управление";
            this.ManualControlRb.UseVisualStyleBackColor = true;
            this.ManualControlRb.CheckedChanged += new System.EventHandler(this.ManualControlRb_CheckedChanged);
            // 
            // GameSpeedInput
            // 
            this.GameSpeedInput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GameSpeedInput.Location = new System.Drawing.Point(7, 144);
            this.GameSpeedInput.Name = "GameSpeedInput";
            this.GameSpeedInput.Size = new System.Drawing.Size(182, 29);
            this.GameSpeedInput.TabIndex = 13;
            this.GameSpeedInput.TextChanged += new System.EventHandler(this.GameSpeedInput_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(6, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 21);
            this.label6.TabIndex = 12;
            this.label6.Text = "Скорость (1-100):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(6, 197);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 21);
            this.label5.TabIndex = 11;
            this.label5.Text = "Настройки поля:";
            // 
            // RecreateButton
            // 
            this.RecreateButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RecreateButton.Location = new System.Drawing.Point(9, 351);
            this.RecreateButton.Name = "RecreateButton";
            this.RecreateButton.Size = new System.Drawing.Size(184, 40);
            this.RecreateButton.TabIndex = 10;
            this.RecreateButton.Text = "Пересоздать";
            this.RecreateButton.UseVisualStyleBackColor = true;
            this.RecreateButton.Click += new System.EventHandler(this.RecreateButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.StopButton.Location = new System.Drawing.Point(108, 28);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(90, 40);
            this.StopButton.TabIndex = 9;
            this.StopButton.Text = "Стоп";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // HeightSizeInput
            // 
            this.HeightSizeInput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.HeightSizeInput.Location = new System.Drawing.Point(108, 313);
            this.HeightSizeInput.Name = "HeightSizeInput";
            this.HeightSizeInput.Size = new System.Drawing.Size(85, 29);
            this.HeightSizeInput.TabIndex = 8;
            this.HeightSizeInput.TextChanged += new System.EventHandler(this.HeightSizeInput_TextChanged);
            // 
            // WidthSizeInput
            // 
            this.WidthSizeInput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.WidthSizeInput.Location = new System.Drawing.Point(8, 313);
            this.WidthSizeInput.Name = "WidthSizeInput";
            this.WidthSizeInput.Size = new System.Drawing.Size(85, 29);
            this.WidthSizeInput.TabIndex = 7;
            this.WidthSizeInput.TextChanged += new System.EventHandler(this.WidthSizeInput_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(7, 285);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(197, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "Рамер поля (3-100) (Ш*В):";
            // 
            // CellSizeInput
            // 
            this.CellSizeInput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CellSizeInput.Location = new System.Drawing.Point(7, 250);
            this.CellSizeInput.Name = "CellSizeInput";
            this.CellSizeInput.Size = new System.Drawing.Size(182, 29);
            this.CellSizeInput.TabIndex = 5;
            this.CellSizeInput.TextChanged += new System.EventHandler(this.CellSizeInput_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(6, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(196, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "Размер ячейки (10-40 px):";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Honeydew;
            this.pictureBox1.Location = new System.Drawing.Point(213, 14);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 400);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // SnakeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 721);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Name = "SnakeForm";
            this.Text = "Змейка";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label CounterLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button RecreateButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.TextBox HeightSizeInput;
        private System.Windows.Forms.TextBox WidthSizeInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox CellSizeInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox GameSpeedInput;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton DijkstraControlRb;
        private System.Windows.Forms.RadioButton ManualControlRb;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

