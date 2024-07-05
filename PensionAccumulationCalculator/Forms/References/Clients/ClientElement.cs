using Azure;

using PensionAccumulationCalculator.Entities;
using PensionAccumulationCalculator.Enums;
using PensionAccumulationCalculator.Services.Interfaces;

namespace PensionAccumulationCalculator.Forms.References.Users {
    public partial class ClientElement : Form {
        private readonly int _id;
        private readonly IUserService _userService;
        private readonly CRUDAction _CRUDAction;

        public ClientElement(IUserService userService, CRUDAction action, int id = 0) {
            _userService = userService;
            _CRUDAction = action;
            _id = id;

            InitializeComponent();

            switch (action) {
                //case CRUDAction.Create:
                //    _headerLabel.Text = "Добавление";
                //    _actionButton.Text = "Добавить";
                //    _actionButton.Click += Create;
                //    break;
                case CRUDAction.Read:
                    _headerLabel.Text = "Просмотр";
                    _secondNameTextBox.Enabled = false;
                    _firstNameTextBox.Enabled = false;
                    _lastNameTextBox.Enabled = false;
                    _emailTextBox.Enabled = false;
                    _phoneTextBox.Enabled = false;
                    _actionButton.Visible = false;
                    break;
                case CRUDAction.Update:
                    _headerLabel.Text = "Редактирование";
                    _actionButton.Text = "Сохранить";
                    _actionButton.Click += Update;
                    break;
                //case CRUDAction.Delete:
                //    _headerLabel.Text = "Удаление";
                //    _actionButton.Text = "Удалить";
                //    _secondNameTextBox.Enabled = false;
                //    _firstNameTextBox.Enabled = false;
                //    _lastNameTextBox.Enabled = false;
                //    _emailTextBox.Enabled = false;
                //    _phoneTextBox.Enabled = false;
                //    _actionButton.Click += Delete;
                //    break;
            }
        }

        //private async void Create(object? sender, EventArgs e) {
        //    Client client = new() {
        //        Second_name = _secondNameTextBox.Text,
        //        First_name = _firstNameTextBox.Text,
        //        Last_name = _lastNameTextBox.Text,
        //        Email = _emailTextBox.Text,
        //        Phone_number = _phoneTextBox.Text,
        //    };

        //    var response = await _userService.TryCreateAsync(user, client);

        //    if (response.Data == false) {
        //        //MessageBox.Show();
        //    }

        //    Close();
        //}

        private async void Update(object? sender, EventArgs e) {
            Client client = new () {
                User_id = _id,
                Second_name = _secondNameTextBox.Text,
                First_name = _firstNameTextBox.Text,
                Last_name = _lastNameTextBox.Text,
                Email = _emailTextBox.Text,
                Phone_number = _phoneTextBox.Text,
            };

            var response = await _userService.TryUpdateClientAsync(client);

            if (response.Data == false) {
                MessageBox.Show(response.Description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Close();
        }

        //private async void Delete(object? sender, EventArgs e) {
        //    var response = await _userService.TryDeleteAsync(_id);

        //    if (response.Data == false) {
        //        //MessageBox.Show();
        //    }

        //    Close();
        //}

        private async void UserElement_Load(object? sender, EventArgs e) {
            if (_CRUDAction != CRUDAction.Create) { 
                var response = await _userService.GetClientByIdAsync(_id);

                if (response.Data == null) {
                    MessageBox.Show(response.Description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                Client client = response.Data;

                _idTextBox.Text = client.User_id.ToString();
                _secondNameTextBox.Text = client.Second_name;
                _firstNameTextBox.Text = client.First_name;
                _lastNameTextBox.Text = client.Last_name;
                _emailTextBox.Text = client.Email;
                _phoneTextBox.Text = client.Phone_number;
            }
        }
    }
}
