namespace PensionAccumulationCalculator.Forms.ClientProfile
{
    partial class ClientList
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
            dataGridView1 = new DataGridView();
            Add = new Button();
            Edit = new Button();
            Delete = new Button();
            ExitToMenu = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(960, 468);
            dataGridView1.TabIndex = 0;
            // 
            // Add
            // 
            Add.Location = new Point(438, 497);
            Add.Name = "Add";
            Add.Size = new Size(163, 52);
            Add.TabIndex = 1;
            Add.Text = "Добавить";
            Add.UseVisualStyleBackColor = true;
            Add.Click += Add_Click;
            // 
            // Edit
            // 
            Edit.Location = new Point(619, 497);
            Edit.Name = "Edit";
            Edit.Size = new Size(163, 52);
            Edit.TabIndex = 2;
            Edit.Text = "Редактировать";
            Edit.UseVisualStyleBackColor = true;
            Edit.Click += Edit_Click;
            // 
            // Delete
            // 
            Delete.Location = new Point(800, 497);
            Delete.Name = "Delete";
            Delete.Size = new Size(163, 52);
            Delete.TabIndex = 3;
            Delete.Text = "Удалить";
            Delete.UseVisualStyleBackColor = true;
            Delete.Click += Delete_Click;
            // 
            // ExitToMenu
            // 
            ExitToMenu.Location = new Point(21, 497);
            ExitToMenu.Name = "ExitToMenu";
            ExitToMenu.Size = new Size(163, 52);
            ExitToMenu.TabIndex = 4;
            ExitToMenu.Text = "В меню";
            ExitToMenu.UseVisualStyleBackColor = true;
            ExitToMenu.Click += ExitToMenu_Click;
            // 
            // ClientList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(ExitToMenu);
            Controls.Add(Delete);
            Controls.Add(Edit);
            Controls.Add(Add);
            Controls.Add(dataGridView1);
            Name = "ClientList";
            Text = "Клиенты";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button Add;
        private Button Edit;
        private Button Delete;
        private Button ExitToMenu;
    }
}