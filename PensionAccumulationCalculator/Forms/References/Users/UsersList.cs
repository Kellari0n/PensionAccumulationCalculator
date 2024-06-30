using PensionAccumulationCalculator.Services.Interfaces;

namespace PensionAccumulationCalculator.Forms.Users {
    public partial class UsersList : Form {
        private readonly IUserService _userService;
        public UsersList(IUserService userService) {
            _userService = userService;

            InitializeComponent();
        }

        private async void UsersList_Load(object sender, EventArgs e) {
            var usersResponse = await _userService.GetAllAsync();
            var clientsResponse = await _userService.GetClientsAsync();

            var users = usersResponse.Data;
            var clients = clientsResponse.Data;

            var data = users.Join(clients, u => u.User_id, c => c.User_id, (u, c) => 
                new { u.User_id, u.Login, u.Password, c.Second_name, c.First_name, c.Last_name, c.Phone_number, c.Email } )
                .ToList();

            dataGridView.DataSource = data;
        }

        private void Add_Click(object sender, EventArgs e) {

        }

        private void Edit_Click(object sender, EventArgs e) {

        }

        private void Delete_Click(object sender, EventArgs e) {

        }

        private void ExitToMenu_Click(object sender, EventArgs e) {

        }
    }
}
