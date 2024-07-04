namespace PensionAccumulationCalculator.Forms.References.Users {
    partial class ClientElement {
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
            _secondNameLabel = new Label();
            _lastNameLabel = new Label();
            _firstNameLabel = new Label();
            _actionButton = new Button();
            _secondNameTextBox = new TextBox();
            _firstNameTextBox = new TextBox();
            _lastNameTextBox = new TextBox();
            _phoneLabel = new Label();
            _phoneTextBox = new TextBox();
            _emailLabel = new Label();
            _emailTextBox = new TextBox();
            _headerLabel = new Label();
            _idTextBox = new TextBox();
            _idLabel = new Label();
            SuspendLayout();
            // 
            // _secondNameLabel
            // 
            _secondNameLabel.AutoSize = true;
            _secondNameLabel.Font = new Font("Segoe UI", 12F);
            _secondNameLabel.Location = new Point(20, 91);
            _secondNameLabel.Name = "_secondNameLabel";
            _secondNameLabel.Size = new Size(75, 21);
            _secondNameLabel.TabIndex = 0;
            _secondNameLabel.Text = "Фамилия";
            // 
            // _lastNameLabel
            // 
            _lastNameLabel.AutoSize = true;
            _lastNameLabel.Font = new Font("Segoe UI", 12F);
            _lastNameLabel.Location = new Point(18, 164);
            _lastNameLabel.Name = "_lastNameLabel";
            _lastNameLabel.Size = new Size(77, 21);
            _lastNameLabel.TabIndex = 2;
            _lastNameLabel.Text = "Отчество";
            // 
            // _firstNameLabel
            // 
            _firstNameLabel.AutoSize = true;
            _firstNameLabel.Font = new Font("Segoe UI", 12F);
            _firstNameLabel.Location = new Point(54, 129);
            _firstNameLabel.Name = "_firstNameLabel";
            _firstNameLabel.Size = new Size(41, 21);
            _firstNameLabel.TabIndex = 3;
            _firstNameLabel.Text = "Имя";
            // 
            // _actionButton
            // 
            _actionButton.Location = new Point(115, 305);
            _actionButton.Name = "_actionButton";
            _actionButton.Size = new Size(124, 27);
            _actionButton.TabIndex = 4;
            _actionButton.Text = "Сохранить";
            _actionButton.UseVisualStyleBackColor = true;
            // 
            // _secondNameTextBox
            // 
            _secondNameTextBox.Location = new Point(101, 89);
            _secondNameTextBox.Name = "_secondNameTextBox";
            _secondNameTextBox.Size = new Size(198, 23);
            _secondNameTextBox.TabIndex = 5;
            // 
            // _firstNameTextBox
            // 
            _firstNameTextBox.Location = new Point(101, 127);
            _firstNameTextBox.Name = "_firstNameTextBox";
            _firstNameTextBox.Size = new Size(198, 23);
            _firstNameTextBox.TabIndex = 6;
            // 
            // _lastNameTextBox
            // 
            _lastNameTextBox.Location = new Point(101, 164);
            _lastNameTextBox.Name = "_lastNameTextBox";
            _lastNameTextBox.Size = new Size(198, 23);
            _lastNameTextBox.TabIndex = 7;
            // 
            // _phoneLabel
            // 
            _phoneLabel.AutoSize = true;
            _phoneLabel.Font = new Font("Segoe UI", 12F);
            _phoneLabel.Location = new Point(24, 215);
            _phoneLabel.Name = "_phoneLabel";
            _phoneLabel.Size = new Size(71, 21);
            _phoneLabel.TabIndex = 8;
            _phoneLabel.Text = "Телефон";
            // 
            // _phoneTextBox
            // 
            _phoneTextBox.Location = new Point(101, 213);
            _phoneTextBox.Name = "_phoneTextBox";
            _phoneTextBox.Size = new Size(198, 23);
            _phoneTextBox.TabIndex = 9;
            // 
            // _emailLabel
            // 
            _emailLabel.AutoSize = true;
            _emailLabel.Font = new Font("Segoe UI", 12F);
            _emailLabel.Location = new Point(47, 251);
            _emailLabel.Name = "_emailLabel";
            _emailLabel.Size = new Size(48, 21);
            _emailLabel.TabIndex = 10;
            _emailLabel.Text = "Email";
            // 
            // _emailTextBox
            // 
            _emailTextBox.Location = new Point(101, 249);
            _emailTextBox.Name = "_emailTextBox";
            _emailTextBox.Size = new Size(198, 23);
            _emailTextBox.TabIndex = 11;
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
            // _idTextBox
            // 
            _idTextBox.Enabled = false;
            _idTextBox.Location = new Point(101, 40);
            _idTextBox.Name = "_idTextBox";
            _idTextBox.Size = new Size(198, 23);
            _idTextBox.TabIndex = 16;
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
            // ClientElement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(350, 344);
            Controls.Add(_idTextBox);
            Controls.Add(_idLabel);
            Controls.Add(_headerLabel);
            Controls.Add(_emailTextBox);
            Controls.Add(_emailLabel);
            Controls.Add(_phoneTextBox);
            Controls.Add(_phoneLabel);
            Controls.Add(_lastNameTextBox);
            Controls.Add(_firstNameTextBox);
            Controls.Add(_secondNameTextBox);
            Controls.Add(_actionButton);
            Controls.Add(_firstNameLabel);
            Controls.Add(_lastNameLabel);
            Controls.Add(_secondNameLabel);
            Name = "ClientElement";
            Text = "Пользователь";
            Load += UserElement_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label _secondNameLabel;
        private Label _lastNameLabel;
        private Label _firstNameLabel;
        private Button _actionButton;
        private TextBox _secondNameTextBox;
        private TextBox _firstNameTextBox;
        private TextBox _lastNameTextBox;
        private Label _phoneLabel;
        private TextBox _phoneTextBox;
        private Label _emailLabel;
        private TextBox _emailTextBox;
        private Label _headerLabel;
        private TextBox _idTextBox;
        private Label _idLabel;
    }
}