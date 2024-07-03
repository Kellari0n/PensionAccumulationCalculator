namespace PensionAccumulationCalculator.Forms
{
    partial class InsuranceRecordElement
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
            _headerLabel = new Label();
            _actionButton = new Button();
            _idLabel = new Label();
            _idTextBox = new TextBox();
            _userIdTextBox = new TextBox();
            _userIdLabel = new Label();
            _coefficientTextBox = new TextBox();
            _coefficientLabel = new Label();
            _yearTextBox = new TextBox();
            _yearLabel = new Label();
            SuspendLayout();
            // 
            // _headerLabel
            // 
            _headerLabel.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            _headerLabel.Location = new Point(12, 9);
            _headerLabel.Name = "_headerLabel";
            _headerLabel.Size = new Size(409, 23);
            _headerLabel.TabIndex = 0;
            _headerLabel.Text = "Header";
            _headerLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // _actionButton
            // 
            _actionButton.Location = new Point(137, 239);
            _actionButton.Name = "_actionButton";
            _actionButton.Size = new Size(146, 28);
            _actionButton.TabIndex = 1;
            _actionButton.Text = "action";
            _actionButton.UseVisualStyleBackColor = true;
            // 
            // _idLabel
            // 
            _idLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            _idLabel.Location = new Point(12, 54);
            _idLabel.Name = "_idLabel";
            _idLabel.Size = new Size(214, 23);
            _idLabel.TabIndex = 5;
            _idLabel.Text = "Insurance_exp_id";
            _idLabel.TextAlign = ContentAlignment.TopRight;
            // 
            // _idTextBox
            // 
            _idTextBox.Enabled = false;
            _idTextBox.Location = new Point(232, 54);
            _idTextBox.Name = "_idTextBox";
            _idTextBox.Size = new Size(175, 23);
            _idTextBox.TabIndex = 9;
            // 
            // _userIdTextBox
            // 
            _userIdTextBox.Location = new Point(232, 97);
            _userIdTextBox.Name = "_userIdTextBox";
            _userIdTextBox.Size = new Size(175, 23);
            _userIdTextBox.TabIndex = 11;
            // 
            // _userIdLabel
            // 
            _userIdLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            _userIdLabel.Location = new Point(12, 97);
            _userIdLabel.Name = "_userIdLabel";
            _userIdLabel.Size = new Size(214, 23);
            _userIdLabel.TabIndex = 10;
            _userIdLabel.Text = "User_id";
            _userIdLabel.TextAlign = ContentAlignment.TopRight;
            // 
            // _coefficientTextBox
            // 
            _coefficientTextBox.Location = new Point(232, 140);
            _coefficientTextBox.Name = "_coefficientTextBox";
            _coefficientTextBox.Size = new Size(175, 23);
            _coefficientTextBox.TabIndex = 13;
            // 
            // _coefficientLabel
            // 
            _coefficientLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            _coefficientLabel.Location = new Point(12, 140);
            _coefficientLabel.Name = "_coefficientLabel";
            _coefficientLabel.Size = new Size(214, 23);
            _coefficientLabel.TabIndex = 12;
            _coefficientLabel.Text = "Individual_pension_coefficient";
            _coefficientLabel.TextAlign = ContentAlignment.TopRight;
            // 
            // _yearTextBox
            // 
            _yearTextBox.Location = new Point(232, 182);
            _yearTextBox.Name = "_yearTextBox";
            _yearTextBox.Size = new Size(175, 23);
            _yearTextBox.TabIndex = 15;
            // 
            // _yearLabel
            // 
            _yearLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            _yearLabel.Location = new Point(12, 182);
            _yearLabel.Name = "_yearLabel";
            _yearLabel.Size = new Size(214, 23);
            _yearLabel.TabIndex = 14;
            _yearLabel.Text = "Year";
            _yearLabel.TextAlign = ContentAlignment.TopRight;
            // 
            // InsuranceRecordElement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(433, 279);
            Controls.Add(_yearTextBox);
            Controls.Add(_yearLabel);
            Controls.Add(_coefficientTextBox);
            Controls.Add(_coefficientLabel);
            Controls.Add(_userIdTextBox);
            Controls.Add(_userIdLabel);
            Controls.Add(_idTextBox);
            Controls.Add(_idLabel);
            Controls.Add(_actionButton);
            Controls.Add(_headerLabel);
            Name = "InsuranceRecordElement";
            Text = "InsuranceRecord";
            Load += InsuranceRecordElement_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label _headerLabel;
        private Button _actionButton;
        private Label _idLabel;
        private TextBox _idTextBox;
        private TextBox _userIdTextBox;
        private Label _userIdLabel;
        private TextBox _coefficientTextBox;
        private Label _coefficientLabel;
        private TextBox _yearTextBox;
        private Label _yearLabel;
    }
}