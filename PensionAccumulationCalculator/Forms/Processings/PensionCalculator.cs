using Microsoft.Data.SqlClient;

using PensionAccumulationCalculator.Entities;

using System.Configuration;
using System.Data;

namespace PensionAccumulationCalculator.Forms.Processings {
    public partial class PensionCalculator : Form
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        public PensionCalculator()
        {
            InitializeComponent();
        }

        private async void CalculateButton_Click(object sender, EventArgs e)
        {
            try {
                using (var connection = new SqlConnection(_connectionString)) {
                    CancellationTokenSource tokenSource = new CancellationTokenSource();

                    var openConnTask = connection.OpenAsync(tokenSource.Token);

                    if (Task.WaitAny(openConnTask, Task.Delay(Program.ConnectionWaitingTime, tokenSource.Token)) == 1 || openConnTask.IsFaulted) {
                        tokenSource.Cancel();
                        throw new TimeoutException();
                    }
                    using (var cmd = new SqlCommand("dbo.GetDataForPensionCalculator", connection)) {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (!int.TryParse(userTextBox.Text, out int id)) {
                            MessageBox.Show("Некоректное значение id.");
                        }

                        cmd.Parameters.Add(new SqlParameter("@user_id", id));

                        using (var reader = await cmd.ExecuteReaderAsync()) {
                            await reader.ReadAsync();

                            var tmpObj = new {
                                SecondName = reader.GetString(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Individual_pension_coefficient = reader.GetDecimal(3),
                                Cost = reader.GetDecimal(4),
                            };

                            FIOTextBox.Text = $"{tmpObj.SecondName} {tmpObj.FirstName} {tmpObj.LastName}";
                            IPCCostTextBox.Text = tmpObj.Cost.ToString();
                            IPCCountTextBox.Text = tmpObj.Individual_pension_coefficient.ToString();
                            pensionTextBox.Text = (tmpObj.Cost * tmpObj.Individual_pension_coefficient).ToString();
                        }
                    }
                }
            }
            catch (Exception) {
                MessageBox.Show("Что-то пошло не так.");
            }
        }

    }
}
