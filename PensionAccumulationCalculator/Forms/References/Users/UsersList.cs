using PensionAccumulationCalculator.Enums;
using PensionAccumulationCalculator.Forms.References.Users;
using PensionAccumulationCalculator.Services.Interfaces;

namespace PensionAccumulationCalculator.Forms.Users {
    public partial class UsersList : Form {
        private readonly IUserService _userService;
        public UsersList(IUserService userService) {
            _userService = userService;

            InitializeComponent();
        }

        private async void UsersList_Load(object? sender, EventArgs e) {
            await UpdateListAsync();
        }

        private void Add_Click(object sender, EventArgs e) {
            var userForm = new UserElement(_userService, CRUDAction.Create);
            userForm.FormClosed += async (sender, e) => {
                await UpdateListAsync();
                this.Visible = true;
            };
            this.Visible = false;
            userForm.Show(this);
        }

        private void Edit_Click(object sender, EventArgs e) {
            var selectedId = GetSelectedIds().FirstOrDefault(0);

            if (selectedId == 0) { return; }

            var userForm = new UserElement(_userService, CRUDAction.Update, selectedId);
            userForm.FormClosed += async (sender, e) => {
                await UpdateListAsync();
                this.Visible = true;
            };
            this.Visible = false;
            userForm.Show(this);
        }

        private async void Delete_Click(object sender, EventArgs e) {
            var selectedIds = GetSelectedIds();

            if (selectedIds.Count == 0) { return; }
            else if (selectedIds.Count == 1) {
                var userForm = new UserElement(_userService, CRUDAction.Delete, selectedIds.First());
                userForm.FormClosed += async (sender, e) => {
                    await UpdateListAsync();
                    this.Visible = true;
                };
                this.Visible = false;
                userForm.Show(this);
            }
            else {
                foreach (var id in selectedIds) { 
                    if (!(await _userService.TryDeleteAsync(id)).Data) {
                        //MessageBox.Show();
                    }
                }
                await UpdateListAsync();
            }
        }

        private void ExitToMenu_Click(object sender, EventArgs e) {
            Close();
        }

        private async Task UpdateListAsync() {
            var usersResponse = await _userService.GetAllAsync();
            var clientsResponse = await _userService.GetClientsAsync();

            var users = usersResponse.Data;
            var clients = clientsResponse.Data;

            var data = users.Join(clients, u => u.User_id, c => c.User_id, (u, c) => new {
                u.User_id,
                u.Login,
                u.Password,
                c.Second_name,
                c.First_name,
                c.Last_name,
                c.Phone_number,
                c.Email
            })
            .ToList();

            _dataGridView.DataSource = data;
        }

        private List<int> GetSelectedIds() {
            List<int> selectedRows = new();
            for (int i = 0; i < _dataGridView.SelectedCells.Count; i++) {
                if (!selectedRows.Contains(_dataGridView.SelectedCells[i].RowIndex)) {
                    selectedRows.Add(_dataGridView.SelectedCells[i].RowIndex);
                }
            }

            List<int> selectedIds = new();
            foreach (int i in selectedRows) {
                selectedIds.Add((int)_dataGridView.Rows[i].Cells[0].Value);
            }

            return selectedIds;
        }
    }
}
