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
            _logoLabel = new Label();
            _loginButton = new Button();
            _loginLabel = new Label();
            _passwordLabel = new Label();
            _loginTextBox = new TextBox();
            _passwordTextBox = new TextBox();
            _forgotYourPasswordLinkLabel = new LinkLabel();
            SuspendLayout();
            // 
            // _logoLabel
            // 
            _logoLabel.AutoSize = true;
            _logoLabel.Font = new Font("Segoe UI", 12F);
            _logoLabel.Location = new Point(12, 9);
            _logoLabel.Name = "_logoLabel";
            _logoLabel.Size = new Size(281, 21);
            _logoLabel.TabIndex = 0;
            _logoLabel.Text = "Калькулятор пенсионных накоплений";
            // 
            // _loginButton
            // 
            _loginButton.Location = new Point(111, 169);
            _loginButton.Name = "_loginButton";
            _loginButton.Size = new Size(75, 23);
            _loginButton.TabIndex = 1;
            _loginButton.Text = "Войти";
            _loginButton.UseVisualStyleBackColor = true;
            _loginButton.Click += LoginButton_Click;
            // 
            // _loginLabel
            // 
            _loginLabel.AutoSize = true;
            _loginLabel.Location = new Point(67, 45);
            _loginLabel.Name = "_loginLabel";
            _loginLabel.Size = new Size(41, 15);
            _loginLabel.TabIndex = 2;
            _loginLabel.Text = "Логин";
            // 
            // _passwordLabel
            // 
            _passwordLabel.AutoSize = true;
            _passwordLabel.Location = new Point(67, 100);
            _passwordLabel.Name = "_passwordLabel";
            _passwordLabel.Size = new Size(49, 15);
            _passwordLabel.TabIndex = 3;
            _passwordLabel.Text = "Пароль";
            // 
            // _loginTextBox
            // 
            _loginTextBox.Location = new Point(67, 63);
            _loginTextBox.Name = "_loginTextBox";
            _loginTextBox.Size = new Size(166, 23);
            _loginTextBox.TabIndex = 4;
            // 
            // _passwordTextBox
            // 
            _passwordTextBox.Location = new Point(67, 118);
            _passwordTextBox.Name = "_passwordTextBox";
            _passwordTextBox.Size = new Size(166, 23);
            _passwordTextBox.TabIndex = 5;
            // 
            // _forgotYourPasswordLinkLabel
            // 
            _forgotYourPasswordLinkLabel.AutoSize = true;
            _forgotYourPasswordLinkLabel.Location = new Point(100, 207);
            _forgotYourPasswordLinkLabel.Name = "_forgotYourPasswordLinkLabel";
            _forgotYourPasswordLinkLabel.Size = new Size(98, 15);
            _forgotYourPasswordLinkLabel.TabIndex = 6;
            _forgotYourPasswordLinkLabel.TabStop = true;
            _forgotYourPasswordLinkLabel.Text = "Забыли пароль?";
            _forgotYourPasswordLinkLabel.Visible = false;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(303, 241);
            Controls.Add(_forgotYourPasswordLinkLabel);
            Controls.Add(_passwordTextBox);
            Controls.Add(_loginTextBox);
            Controls.Add(_passwordLabel);
            Controls.Add(_loginLabel);
            Controls.Add(_loginButton);
            Controls.Add(_logoLabel);
            Name = "Login";
            Text = "Index";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label _logoLabel;
        private Button _loginButton;
        private Label _loginLabel;
        private Label _passwordLabel;
        private TextBox _loginTextBox;
        private TextBox _passwordTextBox;
        private LinkLabel _forgotYourPasswordLinkLabel;
    }
}