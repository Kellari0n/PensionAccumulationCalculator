using PensionAccumulationCalculator.Entities;
using PensionAccumulationCalculator.Enums;
using PensionAccumulationCalculator.Services.Interfaces;

using System.Xml;

namespace PensionAccumulationCalculator.Forms.References.Users {
    public partial class UserElement : Form {
        private readonly int _id;
        private readonly IUserService _userService;
        private readonly CRUDAction _CRUDAction;

        public UserElement(IUserService userService, CRUDAction action, int id = 0) {
            _userService = userService;
            _CRUDAction = action;
            _id = id;

            InitializeComponent();

            switch (action) {
                case CRUDAction.Create:
                    _headerLabel.Text = "Добавление";
                    _actionButton.Text = "Добавить";
                    _actionButton.Click += Create;
                    break;
                case CRUDAction.Read:
                    _headerLabel.Text = "Просмотр";
                    _loginTextBox.Enabled = false;
                    _passwordTextBox.Enabled = false;
                    _actionButton.Visible = false;
                    break;
                case CRUDAction.Update:
                    _headerLabel.Text = "Редактирование";
                    _actionButton.Text = "Сохранить";
                    _actionButton.Click += Update;
                    break;
                case CRUDAction.Delete:
                    _headerLabel.Text = "Удаление";
                    _actionButton.Text = "Удалить";
                    _loginTextBox.Enabled = false;
                    _passwordTextBox.Enabled = false;
                    _actionButton.Click += Delete;
                    break;
            }
        }

        private async void Create(object? sender, EventArgs e) {
            User user = new() {
                Login = _loginTextBox.Text,
                Password = _passwordTextBox.Text,
            };

            var response = await _userService.TryCreateAsync(user);

            if (response.Data == false) {
                MessageBox.Show(response.Description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Close();
        }

        private async void Update(object? sender, EventArgs e) {
            User user = new() {
                User_id = _id,
                Login = _loginTextBox.Text,
                Password = _passwordTextBox.Text,
            };

            var response = await _userService.TryUpdateAsync(user);

            if (response.Data == false) {
                MessageBox.Show(response.Description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Close();
        }

        private async void Delete(object? sender, EventArgs e) {
            var response = await _userService.TryDeleteAsync(_id);

            if (response.Data == false) {
                MessageBox.Show(response.Description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Close();
        }

        private async void UserElement_Load(object? sender, EventArgs e) {
            if (_CRUDAction != CRUDAction.Create) {
                var response = await _userService.GetByIdAsync(_id);

                if (response.Data == null) {
                    MessageBox.Show(response.Description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                User user = response.Data;

                _idTextBox.Text = user.User_id.ToString();
                _loginTextBox.Text = user.Login;
                _passwordTextBox.Text = user.Password;
            }
        }

        private async void ImportButton_Click(object sender, EventArgs e) {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.Filter = "Xml|*.xml";

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    XmlDocument xml = new XmlDocument();
                    xml.Load(openFileDialog.OpenFile());

                    var resp = await _userService.TryImportXmlAsync(xml);

                    if (resp.Data) {
                        MessageBox.Show("Пользователи успешно добавлены");
                        Close(); 
                        return;
                    }
                    else {
                        MessageBox.Show(resp.Description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close(); return;
                    }
                }
            }
        }
    }
}