using PensionAccumulationCalculator.Enums;
using PensionAccumulationCalculator.Forms.References.Users;
using PensionAccumulationCalculator.Services.Interfaces;

using System.Xml;
using System.Xml.Linq;

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
            var response = await _userService.GetAllAsync();

            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                //MessageBox.Show();
                return;
            }

            _dataGridView.DataSource = response.Data;
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

        private async void ExportButton_Click(object sender, EventArgs e) {
            XmlDocument? xmlDocument = new XmlDocument();
            var ids = GetSelectedIds();
            
            if (ids.Count != 1) {
                var resp = await _userService.ExportXmlAsync();
                if (resp.StatusCode == System.Net.HttpStatusCode.OK) {
                    xmlDocument = resp.Data;
                }
                else {
                    MessageBox.Show(resp.Description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else {
                var resp = await _userService.ExportXmlByIdAsync(ids.First());
                if (resp.StatusCode == System.Net.HttpStatusCode.OK) {
                    xmlDocument = resp.Data;
                }
                else {
                    MessageBox.Show(resp.Description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog() {
                DefaultExt = "*.xml",
                Filter = "Xml|*.xml",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads",
                FileName = "export"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                xmlDocument?.Save(saveFileDialog.FileName);
            }
        }
    }
}
