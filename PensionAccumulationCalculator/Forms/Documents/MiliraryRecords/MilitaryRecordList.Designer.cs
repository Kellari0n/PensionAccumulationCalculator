namespace PensionAccumulationCalculator.Forms.MiliraryRecord
{
    partial class MilitaryRecordList
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
            Add.Location = new Point(431, 497);
            Add.Name = "Add";
            Add.Size = new Size(163, 52);
            Add.TabIndex = 1;
            Add.Text = "Добавить";
            Add.UseVisualStyleBackColor = true;
            Add.Click += ExitToMenu_Click;
            // 
            // Edit
            // 
            Edit.Location = new Point(616, 497);
            Edit.Name = "Edit";
            Edit.Size = new Size(163, 52);
            Edit.TabIndex = 2;
            Edit.Text = "Редактировать";
            Edit.UseVisualStyleBackColor = true;
            // 
            // TryDelete
            // 
            Delete.Location = new Point(800, 497);
            Delete.Name = "TryDelete";
            Delete.Size = new Size(163, 52);
            Delete.TabIndex = 3;
            Delete.Text = "Удалить";
            Delete.UseVisualStyleBackColor = true;
            // 
            // ExitToMenu
            // 
            ExitToMenu.Location = new Point(23, 497);
            ExitToMenu.Name = "ExitToMenu";
            ExitToMenu.Size = new Size(163, 52);
            ExitToMenu.TabIndex = 4;
            ExitToMenu.Text = "В меню";
            ExitToMenu.UseVisualStyleBackColor = true;
            // 
            // MilitaryRecordList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(ExitToMenu);
            Controls.Add(Delete);
            Controls.Add(Edit);
            Controls.Add(Add);
            Controls.Add(dataGridView1);
            Name = "MilitaryRecordList";
            Text = "Записи военного  стажа";
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