using Microsoft.Data.SqlClient;

using PensionAccumulationCalculator.Entities;
using PensionAccumulationCalculator.Repos.Interfaces;

using System.Configuration;
using System.Data;
using System.Xml;

namespace PensionAccumulationCalculator.Repos.Implementations {
    internal class WorkRecordRepo : IWorkRecordRepo {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        public async Task<bool> TryCreateAsync(Work_record entity) {
            using (var connection = new SqlConnection(_connectionString)) {
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                var openConnTask = connection.OpenAsync(tokenSource.Token);

                if (Task.WaitAny(openConnTask, Task.Delay(Program.ConnectionWaitingTime, tokenSource.Token)) == 1 || openConnTask.IsFaulted) {
                    tokenSource.Cancel();
                    throw new TimeoutException();
                }
                using (var cmd = new SqlCommand("dbo.CreateWorkRecord", connection)) {
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
                using (var cmd = new SqlCommand("dbo.DeleteWorkRecord", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        await reader.ReadAsync();

                        return reader.GetBoolean(0);
                    }
                }
            }
        }

        public async Task<ICollection<Work_record>> GetAllAsync() {
            using (var connection = new SqlConnection(_connectionString)) {
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                var openConnTask = connection.OpenAsync(tokenSource.Token);

                if (Task.WaitAny(openConnTask, Task.Delay(Program.ConnectionWaitingTime, tokenSource.Token)) == 1 || openConnTask.IsFaulted) {
                    tokenSource.Cancel();
                    throw new TimeoutException();
                }
                using (var cmd = new SqlCommand("dbo.GetAllWorkRecords", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        var entities = new List<Work_record>();

                        while (await reader.ReadAsync()) {
                            entities.Add(new Work_record {
                                Work_exp_id = reader.GetInt32(0),
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

        public async Task<Work_record> GetByIdAsync(int id) {
            using (var connection = new SqlConnection(_connectionString)) {
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                var openConnTask = connection.OpenAsync(tokenSource.Token);

                if (Task.WaitAny(openConnTask, Task.Delay(Program.ConnectionWaitingTime, tokenSource.Token)) == 1 || openConnTask.IsFaulted) {
                    tokenSource.Cancel();
                    throw new TimeoutException();
                }
                using (var cmd = new SqlCommand("dbo.GetWorkRecordById", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        await reader.ReadAsync();

                        return new Work_record {
                            Work_exp_id = reader.GetInt32(0),
                            User_id = reader.GetInt32(1),
                            Individual_pension_coefficient = reader.GetDecimal(2),
                            Year = reader.GetInt32(3),
                        };
                    }
                }
            }
        }

        public async Task<bool> TryUpdateAsync(Work_record entity) {
            using (var connection = new SqlConnection(_connectionString)) {
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                var openConnTask = connection.OpenAsync(tokenSource.Token);

                if (Task.WaitAny(openConnTask, Task.Delay(Program.ConnectionWaitingTime, tokenSource.Token)) == 1 || openConnTask.IsFaulted) {
                    tokenSource.Cancel();
                    throw new TimeoutException();
                }
                using (var cmd = new SqlCommand("dbo.UpdateWorkRecord", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", entity.Work_exp_id));
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

        public async Task<XmlDocument> ExportXmlByIdAsync(int id) {
            using (var connection = new SqlConnection(_connectionString)) {
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                var openConnTask = connection.OpenAsync(tokenSource.Token);

                if (Task.WaitAny(openConnTask, Task.Delay(Program.ConnectionWaitingTime, tokenSource.Token)) == 1 || openConnTask.IsFaulted) {
                    tokenSource.Cancel();
                    throw new TimeoutException();
                }

                using (var cmd = new SqlCommand("dbo.GetWorkRecordXml", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        await reader.ReadAsync();

                        XmlDocument document = new XmlDocument();

                        document.LoadXml(reader.GetString(0));

                        return document;
                    }
                }
            }
        }

        public async Task<XmlDocument> ExportXmlAsync() {
            using (var connection = new SqlConnection(_connectionString)) {
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                var openConnTask = connection.OpenAsync(tokenSource.Token);

                if (Task.WaitAny(openConnTask, Task.Delay(Program.ConnectionWaitingTime, tokenSource.Token)) == 1 || openConnTask.IsFaulted) {
                    tokenSource.Cancel();
                    throw new TimeoutException();
                }

                using (var cmd = new SqlCommand("dbo.GetWorkRecordsXml", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        await reader.ReadAsync();

                        XmlDocument document = new XmlDocument();

                        document.LoadXml(reader.GetString(0));

                        return document;
                    }
                }
            }
        }

        public Task<bool> TryImportXmlAsync(XmlDocument xml) {
            throw new NotImplementedException();
        }
    }
}
