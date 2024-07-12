namespace PensionAccumulationCalculator.Forms.Users
{
    partial class UsersList
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
            _dataGridView = new DataGridView();
            _createButton = new Button();
            _updateButton = new Button();
            _deleteButton = new Button();
            _exitToMenuButton = new Button();
            _exportButton = new Button();
            ((System.ComponentModel.ISupportInitialize)_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // _dataGridView
            // 
            _dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dataGridView.Location = new Point(12, 12);
            _dataGridView.Name = "_dataGridView";
            _dataGridView.Size = new Size(960, 468);
            _dataGridView.TabIndex = 0;
            // 
            // _createButton
            // 
            _createButton.Location = new Point(437, 497);
            _createButton.Name = "_createButton";
            _createButton.Size = new Size(163, 52);
            _createButton.TabIndex = 1;
            _createButton.Text = "Добавить";
            _createButton.UseVisualStyleBackColor = true;
            _createButton.Click += Add_Click;
            // 
            // _updateButton
            // 
            _updateButton.Location = new Point(619, 497);
            _updateButton.Name = "_updateButton";
            _updateButton.Size = new Size(163, 52);
            _updateButton.TabIndex = 2;
            _updateButton.Text = "Редактировать";
            _updateButton.UseVisualStyleBackColor = true;
            _updateButton.Click += Edit_Click;
            // 
            // _deleteButton
            // 
            _deleteButton.Location = new Point(800, 497);
            _deleteButton.Name = "_deleteButton";
            _deleteButton.Size = new Size(163, 52);
            _deleteButton.TabIndex = 3;
            _deleteButton.Text = "Удалить";
            _deleteButton.UseVisualStyleBackColor = true;
            _deleteButton.Click += Delete_Click;
            // 
            // _exitToMenuButton
            // 
            _exitToMenuButton.Location = new Point(21, 497);
            _exitToMenuButton.Name = "_exitToMenuButton";
            _exitToMenuButton.Size = new Size(163, 52);
            _exitToMenuButton.TabIndex = 4;
            _exitToMenuButton.Text = "В меню";
            _exitToMenuButton.UseVisualStyleBackColor = true;
            _exitToMenuButton.Click += ExitToMenu_Click;
            // 
            // _exportButton
            // 
            _exportButton.Location = new Point(254, 497);
            _exportButton.Name = "_exportButton";
            _exportButton.Size = new Size(163, 52);
            _exportButton.TabIndex = 5;
            _exportButton.Text = "Экспорт";
            _exportButton.UseVisualStyleBackColor = true;
            _exportButton.Click += ExportButton_Click;
            // 
            // UsersList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(_exportButton);
            Controls.Add(_exitToMenuButton);
            Controls.Add(_deleteButton);
            Controls.Add(_updateButton);
            Controls.Add(_createButton);
            Controls.Add(_dataGridView);
            Name = "UsersList";
            Text = "Пользователи";
            Load += UsersList_Load;
            ((System.ComponentModel.ISupportInitialize)_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView _dataGridView;
        private Button _createButton;
        private Button _updateButton;
        private Button _deleteButton;
        private Button _exitToMenuButton;
        private Button _exportButton;
    }
}