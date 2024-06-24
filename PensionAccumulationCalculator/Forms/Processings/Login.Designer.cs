namespace PensionAccumulationCalculator.Forms {
    partial class Login {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            LogoLabel = new Label();
            button1 = new Button();
            LoginLabel = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            ForgotYourPasswordLinkLabel = new LinkLabel();
            SuspendLayout();
            // 
            // LogoLabel
            // 
            LogoLabel.AutoSize = true;
            LogoLabel.Font = new Font("Segoe UI", 12F);
            LogoLabel.Location = new Point(12, 9);
            LogoLabel.Name = "LogoLabel";
            LogoLabel.Size = new Size(281, 21);
            LogoLabel.TabIndex = 0;
            LogoLabel.Text = "Калькулятор пенсионных накоплений";
            // 
            // button1
            // 
            button1.Location = new Point(111, 169);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Войти";
            button1.UseVisualStyleBackColor = true;
            // 
            // LoginLabel
            // 
            LoginLabel.AutoSize = true;
            LoginLabel.Location = new Point(67, 45);
            LoginLabel.Name = "LoginLabel";
            LoginLabel.Size = new Size(41, 15);
            LoginLabel.TabIndex = 2;
            LoginLabel.Text = "Логин";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(67, 100);
            label3.Name = "label3";
            label3.Size = new Size(49, 15);
            label3.TabIndex = 3;
            label3.Text = "Пароль";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(67, 63);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(166, 23);
            textBox1.TabIndex = 4;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(67, 118);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(166, 23);
            textBox2.TabIndex = 5;
            // 
            // ForgotYourPasswordLinkLabel
            // 
            ForgotYourPasswordLinkLabel.AutoSize = true;
            ForgotYourPasswordLinkLabel.Location = new Point(100, 207);
            ForgotYourPasswordLinkLabel.Name = "ForgotYourPasswordLinkLabel";
            ForgotYourPasswordLinkLabel.Size = new Size(98, 15);
            ForgotYourPasswordLinkLabel.TabIndex = 6;
            ForgotYourPasswordLinkLabel.TabStop = true;
            ForgotYourPasswordLinkLabel.Text = "Забыли пароль?";
            ForgotYourPasswordLinkLabel.Visible = false;
            // 
            // Index
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(303, 241);
            Controls.Add(ForgotYourPasswordLinkLabel);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(LoginLabel);
            Controls.Add(button1);
            Controls.Add(LogoLabel);
            Name = "Index";
            Text = "Index";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LogoLabel;
        private Button button1;
        private Label LoginLabel;
        private Label label3;
        private TextBox textBox1;
        private TextBox textBox2;
        private LinkLabel ForgotYourPasswordLinkLabel;
    }
}