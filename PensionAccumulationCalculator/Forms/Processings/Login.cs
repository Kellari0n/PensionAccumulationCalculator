using PensionAccumulationCalculator.Entities;
using PensionAccumulationCalculator.Forms.MiliraryRecord;
using PensionAccumulationCalculator.Forms.WorkRecord;
using PensionAccumulationCalculator.Repos.Implementations;
using PensionAccumulationCalculator.Services.Implementations;
using PensionAccumulationCalculator.Services.Interfaces;

namespace PensionAccumulationCalculator.Forms {
    public partial class Login : Form {
        private readonly IUserService _userService;
        public Login(IUserService userService) {
            _userService = userService;

            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, EventArgs e) {
            User? user = new () { Login = _loginTextBox.Text, Password = _passwordTextBox.Text };

            var users = (await _userService.GetAllAsync()).Data;

            user = users.FirstOrDefault(u => u?.Login == user.Login && u.Password == user.Password, null);

            if (user != null) {
                Hide();
                MainMenu mainMenu = new MainMenu();
                mainMenu.FormClosed += (sender, e) => this.Close();
                mainMenu.Show(this);
            }
        }
    }
}