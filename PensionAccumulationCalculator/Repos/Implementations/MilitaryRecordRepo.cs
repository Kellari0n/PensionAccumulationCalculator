using Microsoft.Data.SqlClient;

using PensionAccumulationCalculator.Entities;
using PensionAccumulationCalculator.Repos.Interfaces;

using System.Configuration;
using System.Data;

namespace PensionAccumulationCalculator.Repos.Implementations {
    internal class MilitaryRecordRepo : IMilitaryRecordRepo {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        public async Task<bool> TryCreateAsync(Military_record entity) {
            using (var connection = new SqlConnection(_connectionString)) {
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                var openConnTask = connection.OpenAsync(tokenSource.Token);

                if (Task.WaitAny(openConnTask, Task.Delay(Program.ConnectionWaitingTime, tokenSource.Token)) == 1 || openConnTask.IsFaulted) {
                    tokenSource.Cancel();
                    throw new TimeoutException();
                }
                using (var cmd = new SqlCommand("dbo.CreateMilitaryRecord", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@user_id", entity.User_id));
                    cmd.Parameters.Add(new SqlParameter("@individual_pension_coefficient", entity.Individual_pension_coefficient));
                    cmd.Parameters.Add(new SqlParameter("@year", entity.Year));

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        await reader.ReadAsync();

                        return reader.GetBoolean(0);
                    }
                }
            }
        }

        public async Task<bool> TryDeleteAsync(int id) {
            using (var connection = new SqlConnection(_connectionString)) {
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                var openConnTask = connection.OpenAsync(tokenSource.Token);

                if (Task.WaitAny(openConnTask, Task.Delay(Program.ConnectionWaitingTime, tokenSource.Token)) == 1 || openConnTask.IsFaulted) {
                    tokenSource.Cancel();
                    throw new TimeoutException();
                }
                using (var cmd = new SqlCommand("dbo.DeleteMilitaryRecord", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        await reader.ReadAsync();

                        return reader.GetBoolean(0);
                    }
                }
            }
        }

        public async Task<ICollection<Military_record>> GetAllAsync() {
            using (var connection = new SqlConnection(_connectionString)) {
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                var openConnTask = connection.OpenAsync(tokenSource.Token);

                if (Task.WaitAny(openConnTask, Task.Delay(Program.ConnectionWaitingTime, tokenSource.Token)) == 1 || openConnTask.IsFaulted) {
                    tokenSource.Cancel();
                    throw new TimeoutException();
                }
                using (var cmd = new SqlCommand("dbo.GetAllMilitaryRecords", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        var entities = new List<Military_record>();

                        while (await reader.ReadAsync()) {
                            entities.Add(new Military_record {
                                Military_exp_id = reader.GetInt32(0),
                                User_id = reader.GetInt32(1),
                                Individual_pension_coefficient = reader.GetDecimal(2),
                                Year = reader.GetInt32(3),
                            });
                        }
                        return entities;
                    }
                }
            }
        }

        public async Task<Military_record> GetByIdAsync(int id) {
            using (var connection = new SqlConnection(_connectionString)) {
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                var openConnTask = connection.OpenAsync(tokenSource.Token);

                if (Task.WaitAny(openConnTask, Task.Delay(Program.ConnectionWaitingTime, tokenSource.Token)) == 1 || openConnTask.IsFaulted) {
                    tokenSource.Cancel();
                    throw new TimeoutException();
                }
                using (var cmd = new SqlCommand("dbo.GetMilitaryRecordById", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        await reader.ReadAsync();

                        return new Military_record {
                            Military_exp_id = reader.GetInt32(0),
                            User_id = reader.GetInt32(1),
                            Individual_pension_coefficient = reader.GetDecimal(2),
                            Year = reader.GetInt32(3),
                        };
                    }
                }
            }
        }

        public async Task<bool> TryUpdateAsync(Military_record entity) {
            using (var connection = new SqlConnection(_connectionString)) {
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                var openConnTask = connection.OpenAsync(tokenSource.Token);

                if (Task.WaitAny(openConnTask, Task.Delay(Program.ConnectionWaitingTime, tokenSource.Token)) == 1 || openConnTask.IsFaulted) {
                    tokenSource.Cancel();
                    throw new TimeoutException();
                }
                using (var cmd = new SqlCommand("dbo.UpdateMilitaryRecord", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", entity.Military_exp_id));
                    cmd.Parameters.Add(new SqlParameter("@user_id", entity.User_id));
                    cmd.Parameters.Add(new SqlParameter("@individual_pension_coefficient", entity.Individual_pension_coefficient));
                    cmd.Parameters.Add(new SqlParameter("@year", entity.Year));

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        await reader.ReadAsync();

                        return reader.GetBoolean(0);
                    }
                }
            }
        }
    }
}