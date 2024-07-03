using PensionAccumulationCalculator.Entities;
using PensionAccumulationCalculator.Enums;
using PensionAccumulationCalculator.Services.Interfaces;

using System.Windows.Forms;

namespace PensionAccumulationCalculator.Forms {
    public partial class InsuranceRecordElement : Form {
        private readonly IInsuranceRecordService _recordService;
        private readonly CRUDAction _action;
        private readonly int _id;
        public InsuranceRecordElement(IInsuranceRecordService insuranceRecordService, CRUDAction action, int id = 0) {
            _recordService = insuranceRecordService;
            _action = action;
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
                    _idTextBox.Enabled = false;
                    _coefficientTextBox.Enabled = false;
                    _userIdTextBox.Enabled = false;
                    _yearTextBox.Enabled = false;
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
                    _idTextBox.Enabled = false;
                    _coefficientTextBox.Enabled = false;
                    _userIdTextBox.Enabled = false;
                    _yearTextBox.Enabled = false;
                    _actionButton.Click += Delete;
                    break;
            }
        }

        private async void InsuranceRecordElement_Load(object? sender, EventArgs e) {
            if (_action == CRUDAction.Create) { return; }

            Insurance_record entity = (await _recordService.GetByIdAsync(_id)).Data;

            _idTextBox.Text = entity.Insurance_exp_id.ToString();
            _userIdTextBox.Text = entity.User_id.ToString();
            _coefficientTextBox.Text = entity.Individual_pension_coefficient.ToString();
            _yearTextBox.Text = entity.Year.ToString();
        }

        private async void Create(object? sender, EventArgs e) {
            Insurance_record record = new Insurance_record() {
                User_id = int.Parse(_userIdTextBox.Text),
                Individual_pension_coefficient = Decimal.Parse(_coefficientTextBox.Text),
                Year = int.Parse(_yearTextBox.Text),
            };

            var response = await _recordService.TryCreateAsync(record);

            if (response.Data == false) {
                //MessageBox.Show();
            }

            Close();
        }

        private async void Update(object? sender, EventArgs e) {
            Insurance_record record = new Insurance_record() {
                Insurance_exp_id = _id,
                User_id = int.Parse(_userIdTextBox.Text),
                Individual_pension_coefficient = Decimal.Parse(_coefficientTextBox.Text),
                Year = int.Parse(_yearTextBox.Text),
            };

            var response = await _recordService.TryUpdateAsync(record);

            if (response.Data == false) {
                //MessageBox.Show();
            }

            Close();
        }

        private async void Delete(object? sender, EventArgs e) {
            var response = await _recordService.TryDeleteAsync(_id);

            if (response.Data == false) {
                //MessageBox.Show();
            }

            Close();
        }
    }
}
