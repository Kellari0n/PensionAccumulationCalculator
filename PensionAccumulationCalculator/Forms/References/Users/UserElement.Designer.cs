namespace PensionAccumulationCalculator.Forms.References.Users {
    partial class UserElement {
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
            _actionButton = new Button();
            _headerLabel = new Label();
            _passwordTextBox = new TextBox();
            _loginTextBox = new TextBox();
            _idTextBox = new TextBox();
            _loginLabel = new Label();
            _passwordLabel = new Label();
            _idLabel = new Label();
            SuspendLayout();
            // 
            // _actionButton
            // 
            _actionButton.Location = new Point(110, 174);
            _actionButton.Name = "_actionButton";
            _actionButton.Size = new Size(124, 27);
            _actionButton.TabIndex = 4;
            _actionButton.Text = "Сохранить";
            _actionButton.UseVisualStyleBackColor = true;
            // 
            // _headerLabel
            // 
            _headerLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            _headerLabel.Location = new Point(12, 9);
            _headerLabel.Name = "_headerLabel";
            _headerLabel.Size = new Size(326, 28);
            _headerLabel.TabIndex = 12;
            _headerLabel.Text = "Header";
            _headerLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // _passwordTextBox
            // 
            _passwordTextBox.Location = new Point(101, 115);
            _passwordTextBox.Name = "_passwordTextBox";
            _passwordTextBox.Size = new Size(198, 23);
            _passwordTextBox.TabIndex = 18;
            // 
            // _loginTextBox
            // 
            _loginTextBox.Location = new Point(101, 78);
            _loginTextBox.Name = "_loginTextBox";
            _loginTextBox.Size = new Size(198, 23);
            _loginTextBox.TabIndex = 17;
            // 
            // _idTextBox
            // 
            _idTextBox.Enabled = false;
            _idTextBox.Location = new Point(101, 40);
            _idTextBox.Name = "_idTextBox";
            _idTextBox.Size = new Size(198, 23);
            _idTextBox.TabIndex = 16;
            // 
            // _loginLabel
            // 
            _loginLabel.Font = new Font("Segoe UI", 12F);
            _loginLabel.Location = new Point(18, 80);
            _loginLabel.Name = "_loginLabel";
            _loginLabel.Size = new Size(77, 21);
            _loginLabel.TabIndex = 15;
            _loginLabel.Text = "Логин";
            _loginLabel.TextAlign = ContentAlignment.TopRight;
            // 
            // _passwordLabel
            // 
            _passwordLabel.Font = new Font("Segoe UI", 12F);
            _passwordLabel.Location = new Point(18, 115);
            _passwordLabel.Name = "_passwordLabel";
            _passwordLabel.Size = new Size(77, 21);
            _passwordLabel.TabIndex = 14;
            _passwordLabel.Text = "Пароль";
            _passwordLabel.TextAlign = ContentAlignment.TopRight;
            // 
            // _idLabel
            // 
            _idLabel.Font = new Font("Segoe UI", 12F);
            _idLabel.Location = new Point(20, 42);
            _idLabel.Name = "_idLabel";
            _idLabel.Size = new Size(75, 21);
            _idLabel.TabIndex = 13;
            _idLabel.Text = "ID";
            _idLabel.TextAlign = ContentAlignment.TopRight;
            // 
            // UserElement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(350, 213);
            Controls.Add(_passwordTextBox);
            Controls.Add(_loginTextBox);
            Controls.Add(_idTextBox);
            Controls.Add(_loginLabel);
            Controls.Add(_passwordLabel);
            Controls.Add(_idLabel);
            Controls.Add(_headerLabel);
            Controls.Add(_actionButton);
            Name = "UserElement";
            Text = "Пользователь";
            Load += UserElement_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button _actionButton;
        private Label _headerLabel;
        private TextBox _passwordTextBox;
        private TextBox _loginTextBox;
        private TextBox _idTextBox;
        private Label _loginLabel;
        private Label _passwordLabel;
        private Label _idLabel;
    }
}