using PensionAccumulationCalculator.Services.Interfaces;

namespace PensionAccumulationCalculator.Forms.WorkRecord {
    public partial class WorkRecordList : Form
    {
        private readonly IWorkRecordService _recordService;
        public WorkRecordList(IWorkRecordService recordService) {
            _recordService = recordService;

            InitializeComponent();
        }

        private async void InsuranceRecordList_Load(object sender, EventArgs e) {
            await UpdateListAsync();
        }

        private async Task UpdateListAsync() {
            _dataGridView.DataSource = (await _recordService.GetAllAsync()).Data;
        }

        private void ExitToMenuButton_Click(object sender, EventArgs e) {
            Close();
        }

        private void CreateButton_Click(object sender, EventArgs e) {
            WorkRecordElement elementForm = new(_recordService, Enums.CRUDAction.Create);
            elementForm.FormClosed += async (sender, e) => {
                await UpdateListAsync();
                this.Visible = true;
            };
            this.Visible = false;
            elementForm.Show(this);
        }

        private void UpdateButton_Click(object sender, EventArgs e) {
            int selectedId = GetSelectedIds().FirstOrDefault(0);

            if (selectedId == 0) { return; }

            WorkRecordElement elementForm = new(_recordService, Enums.CRUDAction.Update, selectedId);
            elementForm.FormClosed += async (sender, e) => {
                await UpdateListAsync();
                this.Visible = true;
            };
            this.Visible = false;
            elementForm.Show(this);
        }

        private async void DeleteButton_Click(object sender, EventArgs e) {
            List<int> selectedId = GetSelectedIds();

            if (selectedId.Count == 0) { return; }
            else if (selectedId.Count == 1) {
                WorkRecordElement elementForm = new(_recordService, Enums.CRUDAction.Delete, GetSelectedIds().First());
                elementForm.FormClosed += async (sender, e) => {
                    await UpdateListAsync();
                    this.Visible = true;
                };
                this.Visible = false;
                elementForm.Show(this);
            }
            else {
                foreach (int id in selectedId) {
                    if ((await _recordService.TryDeleteAsync(id)).Data) {
                        //MessageBox.Show();
                    }
                    await UpdateListAsync();
                }
            }
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
