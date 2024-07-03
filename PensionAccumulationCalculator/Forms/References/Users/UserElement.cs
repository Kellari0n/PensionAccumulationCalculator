using PensionAccumulationCalculator.Entities;
using PensionAccumulationCalculator.Enums;
using PensionAccumulationCalculator.Services.Interfaces;

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
                case CRUDAction.Delete:
                    _headerLabel.Text = "Удаление";
                    _actionButton.Text = "Удалить";
                    _loginTextBox.Enabled = false;
                    _passwordTextBox.Enabled = false;
                    _secondNameTextBox.Enabled = false;
                    _firstNameTextBox.Enabled = false;
                    _lastNameTextBox.Enabled = false;
                    _emailTextBox.Enabled = false;
                    _phoneTextBox.Enabled = false;
                    _actionButton.Click += Delete;
                    break;
            }
        }

        private async void Create(object? sender, EventArgs e) {
            User user = new () {
                Login = _loginTextBox.Text,
                Password = _passwordTextBox.Text,
            };

            Client client = new () {
                Second_name = _secondNameTextBox.Text,
                First_name = _firstNameTextBox.Text,
                Last_name = _lastNameTextBox.Text,
                Email = _emailTextBox.Text,
                Phone_number = _phoneTextBox.Text,
            };

            var response = await _userService.TryCreateAsync(user, client);
            
            if (response.Data == false) {
                //MessageBox.Show();
            }

            Close();
        }

        private async void Update(object? sender, EventArgs e) {
            User user = new () {
                User_id = _id,
                Login = _loginTextBox.Text,
                Password = _passwordTextBox.Text,
            };

            Client client = new () {
                User_id = _id,
                Second_name = _secondNameTextBox.Text,
                First_name = _firstNameTextBox.Text,
                Last_name = _lastNameTextBox.Text,
                Email = _emailTextBox.Text,
                Phone_number = _phoneTextBox.Text,
            };

            var userResponse = await _userService.TryUpdateAsync(user);

            if (userResponse.Data == false) {
                //MessageBox.Show();
            }

            var clientResponse = await _userService.TryUpdateClientAsync(client);

            if (clientResponse.Data == false) {
                //MessageBox.Show();
            }

            Close();
        }

        private async void Delete(object? sender, EventArgs e) {
            var response = await _userService.TryDeleteAsync(_id);

            if (response.Data == false) {
                //MessageBox.Show();
            }

            Close();
        }

        private async void UserElement_Load(object? sender, EventArgs e) {
            if (_CRUDAction != CRUDAction.Create) { 
                User user = (await _userService.GetByIdAsync(_id)).Data;
                Client client = (await _userService.GetClientByIdAsync(_id)).Data;

                _idTextBox.Text = user.User_id.ToString();
                _loginTextBox.Text = user.Login;
                _passwordTextBox.Text = user.Password;
                _secondNameTextBox.Text = client.Second_name;
                _firstNameTextBox.Text = client.First_name;
                _lastNameTextBox.Text = client.Last_name;
                _emailTextBox.Text = client.Email;
                _phoneTextBox.Text = client.Phone_number;
            }
        }
    }
}
