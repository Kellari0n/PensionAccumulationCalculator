namespace PensionAccumulationCalculator.Forms
{
    partial class MainMenu
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
            client_button = new Button();
            insurance_record_button = new Button();
            military_record_button = new Button();
            work_record_button = new Button();
            ipc_accum_button = new Button();
            user_button = new Button();
            _calculatorButton = new Button();
            SuspendLayout();
            // 
            // client_button
            // 
            client_button.Location = new Point(12, 75);
            client_button.Name = "client_button";
            client_button.Size = new Size(140, 40);
            client_button.TabIndex = 0;
            client_button.Text = "Клиенты";
            client_button.UseVisualStyleBackColor = true;
            client_button.Click += Client_button_Click;
            // 
            // insurance_record_button
            // 
            insurance_record_button.Location = new Point(12, 130);
            insurance_record_button.Name = "insurance_record_button";
            insurance_record_button.Size = new Size(140, 40);
            insurance_record_button.TabIndex = 1;
            insurance_record_button.Text = "Страховой стаж";
            insurance_record_button.UseVisualStyleBackColor = true;
            insurance_record_button.Click += Insurance_record_button_Click;
            // 
            // military_record_button
            // 
            military_record_button.Location = new Point(12, 185);
            military_record_button.Name = "military_record_button";
            military_record_button.Size = new Size(140, 40);
            military_record_button.TabIndex = 2;
            military_record_button.Text = "Военный стаж";
            military_record_button.UseVisualStyleBackColor = true;
            military_record_button.Click += Military_record_button_Click;
            // 
            // work_record_button
            // 
            work_record_button.Location = new Point(12, 240);
            work_record_button.Name = "work_record_button";
            work_record_button.Size = new Size(140, 40);
            work_record_button.TabIndex = 3;
            work_record_button.Text = "Трудовой стаж";
            work_record_button.UseVisualStyleBackColor = true;
            work_record_button.Click += Work_record_button_Click;
            // 
            // ipc_accum_button
            // 
            ipc_accum_button.Location = new Point(12, 295);
            ipc_accum_button.Name = "ipc_accum_button";
            ipc_accum_button.Size = new Size(140, 40);
            ipc_accum_button.TabIndex = 4;
            ipc_accum_button.Text = "Стоимость ИПК по годам";
            ipc_accum_button.UseVisualStyleBackColor = true;
            ipc_accum_button.Click += Ipc_accum_button_Click;
            // 
            // user_button
            // 
            user_button.Location = new Point(12, 20);
            user_button.Name = "user_button";
            user_button.Size = new Size(140, 40);
            user_button.TabIndex = 5;
            user_button.Text = "Пользователи";
            user_button.UseVisualStyleBackColor = true;
            user_button.Click += User_button_Click;
            // 
            // _calculatorButton
            // 
            _calculatorButton.Location = new Point(12, 352);
            _calculatorButton.Name = "_calculatorButton";
            _calculatorButton.Size = new Size(140, 40);
            _calculatorButton.TabIndex = 6;
            _calculatorButton.Text = "Калькулятор";
            _calculatorButton.UseVisualStyleBackColor = true;
            _calculatorButton.Click += СalculatorButton_Click;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 404);
            Controls.Add(_calculatorButton);
            Controls.Add(user_button);
            Controls.Add(ipc_accum_button);
            Controls.Add(work_record_button);
            Controls.Add(military_record_button);
            Controls.Add(insurance_record_button);
            Controls.Add(client_button);
            Name = "MainMenu";
            Text = "Меню";
            ResumeLayout(false);
        }

        #endregion

        private Button client_button;
        private Button insurance_record_button;
        private Button military_record_button;
        private Button work_record_button;
        private Button ipc_accum_button;
        private Button user_button;
        private Button _calculatorButton;
    }
}