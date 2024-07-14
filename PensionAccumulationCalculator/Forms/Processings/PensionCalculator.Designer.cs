namespace PensionAccumulationCalculator.Forms.Processings
{
    partial class PensionCalculator
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
        private void InitializeComponent() {
            groupBox1 = new GroupBox();
            calculateButton = new Button();
            userTextBox = new TextBox();
            label2 = new Label();
            groupBox2 = new GroupBox();
            label6 = new Label();
            pensionTextBox = new TextBox();
            IPCCostTextBox = new TextBox();
            IPCCountTextBox = new TextBox();
            FIOTextBox = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(calculateButton);
            groupBox1.Controls.Add(userTextBox);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(511, 105);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Укажите данные";
            // 
            // calculateButton
            // 
            calculateButton.Location = new Point(194, 67);
            calculateButton.Name = "calculateButton";
            calculateButton.Size = new Size(116, 32);
            calculateButton.TabIndex = 4;
            calculateButton.Text = "Расчитать";
            calculateButton.UseVisualStyleBackColor = true;
            calculateButton.Click += CalculateButton_Click;
            // 
            // userTextBox
            // 
            userTextBox.Location = new Point(61, 28);
            userTextBox.Name = "userTextBox";
            userTextBox.Size = new Size(146, 23);
            userTextBox.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(9, 36);
            label2.Name = "label2";
            label2.Size = new Size(46, 15);
            label2.TabIndex = 1;
            label2.Text = "Клиент";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(pensionTextBox);
            groupBox2.Controls.Add(IPCCostTextBox);
            groupBox2.Controls.Add(IPCCountTextBox);
            groupBox2.Controls.Add(FIOTextBox);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Location = new Point(12, 123);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(510, 186);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Калькулятор";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(17, 145);
            label6.Name = "label6";
            label6.Size = new Size(123, 15);
            label6.TabIndex = 7;
            label6.Text = "Ежемесячная пенсия";
            // 
            // pensionTextBox
            // 
            pensionTextBox.Location = new Point(181, 137);
            pensionTextBox.Name = "pensionTextBox";
            pensionTextBox.Size = new Size(305, 23);
            pensionTextBox.TabIndex = 6;
            // 
            // IPCCostTextBox
            // 
            IPCCostTextBox.Location = new Point(181, 103);
            IPCCostTextBox.Name = "IPCCostTextBox";
            IPCCostTextBox.Size = new Size(305, 23);
            IPCCostTextBox.TabIndex = 5;
            // 
            // IPCCountTextBox
            // 
            IPCCountTextBox.Location = new Point(181, 64);
            IPCCountTextBox.Name = "IPCCountTextBox";
            IPCCountTextBox.Size = new Size(305, 23);
            IPCCountTextBox.TabIndex = 4;
            // 
            // FIOTextBox
            // 
            FIOTextBox.Location = new Point(181, 25);
            FIOTextBox.Name = "FIOTextBox";
            FIOTextBox.Size = new Size(305, 23);
            FIOTextBox.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(17, 111);
            label5.Name = "label5";
            label5.Size = new Size(152, 15);
            label5.TabIndex = 2;
            label5.Text = "Стоимость коэффициента";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(17, 72);
            label4.Name = "label4";
            label4.Size = new Size(137, 15);
            label4.TabIndex = 1;
            label4.Text = "Сумма коэффициентов";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(17, 33);
            label3.Name = "label3";
            label3.Size = new Size(34, 15);
            label3.TabIndex = 0;
            label3.Text = "ФИО";
            // 
            // PensionCalculator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(534, 326);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "PensionCalculator";
            Text = "PensionCalculator";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button calculateButton;
        private TextBox userTextBox;
        private Label label2;
        private GroupBox groupBox2;
        private Label label6;
        private TextBox pensionTextBox;
        private TextBox IPCCostTextBox;
        private TextBox IPCCountTextBox;
        private TextBox FIOTextBox;
        private Label label5;
        private Label label4;
        private Label label3;
    }
}