namespace PensionAccumulationCalculator.Forms.CoefficientsCost
{
    partial class CoefficientsCostElement
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
            label1 = new Label();
            year_textBox = new TextBox();
            label2 = new Label();
            textBox1 = new TextBox();
            Save = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(124, 67);
            label1.Name = "label1";
            label1.Size = new Size(39, 21);
            label1.TabIndex = 0;
            label1.Text = "Год:";
            // 
            // year_textBox
            // 
            year_textBox.Location = new Point(169, 95);
            year_textBox.Name = "year_textBox";
            year_textBox.Size = new Size(92, 23);
            year_textBox.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(5, 97);
            label2.Name = "label2";
            label2.Size = new Size(158, 21);
            label2.TabIndex = 2;
            label2.Text = "Стоимость, в рублях:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(169, 65);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(92, 23);
            textBox1.TabIndex = 3;
            // 
            // Save
            // 
            Save.Location = new Point(12, 12);
            Save.Name = "Save";
            Save.Size = new Size(124, 36);
            Save.TabIndex = 4;
            Save.Text = "Сохранить";
            Save.UseVisualStyleBackColor = true;
            // 
            // CoefficientsCostElement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(273, 148);
            Controls.Add(Save);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(year_textBox);
            Controls.Add(label1);
            Name = "CoefficientsCostElement";
            Text = "Стоимость ИПК за год";
            Load += CoefficientsCostElement_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox year_textBox;
        private Label label2;
        private TextBox textBox1;
        private Button Save;
    }
}