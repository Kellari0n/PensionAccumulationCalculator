using PensionAccumulationCalculator.Entities;
using PensionAccumulationCalculator.Services.Interfaces;

namespace PensionAccumulationCalculator.Forms {
    public partial class Login : Form {
        private readonly IUserService _userService;
        public Login(IUserService userService) {
            _userService = userService;

            InitializeComponent();
        }

        private async void LoginButton_Click(object? sender, EventArgs e) {
            User? user = new() { Login = _loginTextBox.Text, Password = _passwordTextBox.Text };

            var response = await _userService.GetAllAsync();

            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                MessageBox.Show(response.Description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var users = response.Data;

            user = users?.FirstOrDefault(u => u?.Login == user.Login && u.Password == user.Password, null);

            if (user != null) {
                Hide();
                MainMenu mainMenu = new MainMenu();
                mainMenu.FormClosed += (sender, e) => this.Close();
                mainMenu.Show(this);
            }
        }

        private void Login_Load(object? sender, EventArgs e) {
            _loginTextBox.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) { LoginButton_Click(sender, e); }  };
            _passwordTextBox.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) { LoginButton_Click(sender, e); } };
        }
    }
}