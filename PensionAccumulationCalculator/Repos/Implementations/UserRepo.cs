using Microsoft.Data.SqlClient;

using PensionAccumulationCalculator.Entities;
using PensionAccumulationCalculator.Repos.Interfaces;

using System.Configuration;
using System.Data;
using System.Xml;

namespace PensionAccumulationCalculator.Repos.Implementations {
    internal class UserRepo : IUserRepo {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        public async Task<bool> TryCreateAsync(User user) {
            using (var connection = new SqlConnection(_connectionString)) {
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                var openConnTask = connection.OpenAsync(tokenSource.Token);

                if (Task.WaitAny(openConnTask, Task.Delay(Program.ConnectionWaitingTime, tokenSource.Token)) == 1 || openConnTask.IsFaulted) {
                    tokenSource.Cancel();
                    throw new TimeoutException();
                }

                using (var cmd = new SqlCommand("dbo.CreateUser", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@login", user.Login));
                    cmd.Parameters.Add(new SqlParameter("@password", user.Password));

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        await reader.ReadAsync();

                        return reader.GetBoolean(0);
                    }
                }
            }
        }

        public async Task<bool> TryCreateAsync(User user, Client client) {
            using (var connection = new SqlConnection(_connectionString)) {
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                var openConnTask = connection.OpenAsync(tokenSource.Token);

                if (Task.WaitAny(openConnTask, Task.Delay(Program.ConnectionWaitingTime, tokenSource.Token)) == 1 || openConnTask.IsFaulted) {
                    tokenSource.Cancel();
                    throw new TimeoutException();
                }

                using (var cmd = new SqlCommand("dbo.CreateUser", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@login", user.Login));
                    cmd.Parameters.Add(new SqlParameter("@password", user.Password));
                    cmd.Parameters.Add(new SqlParameter("@second_name", client.Second_name));
                    cmd.Parameters.Add(new SqlParameter("@first_name", client.First_name));
                    cmd.Parameters.Add(new SqlParameter("@last_name", client.Last_name));
                    cmd.Parameters.Add(new SqlParameter("@phone_number", client.Phone_number));
                    cmd.Parameters.Add(new SqlParameter("@email", client.Email));

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

                using (var cmd = new SqlCommand("dbo.DeleteUser", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        await reader.ReadAsync();

                        return reader.GetBoolean(0);
                    }
                }
            }
        }

        public async Task<ICollection<User>> GetAllAsync() {
            using (var connection = new SqlConnection(_connectionString)) {
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                var openConnTask = connection.OpenAsync(tokenSource.Token);

                if (Task.WaitAny(openConnTask, Task.Delay(Program.ConnectionWaitingTime, tokenSource.Token)) == 1 || openConnTask.IsFaulted) {
                    tokenSource.Cancel();
                    throw new TimeoutException();
                }

                using (var cmd = new SqlCommand("dbo.GetAllUsers", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        var entities = new List<User>();

                        while (await reader.ReadAsync()) {
                            entities.Add(new User { 
                                User_id = reader.GetInt32(0),
                                Login = ((IDataRecord)reader)[1].ToString() ?? string.Empty, 
                                Password = ((IDataRecord)reader)[2].ToString() ?? string.Empty 
                            });
                        }
                        return entities;
                    }
                }
            }
        }

        public async Task<ICollection<Client>> GetClientsAsync() {
            using (var connection = new SqlConnection(_connectionString)) {
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                var openConnTask = connection.OpenAsync(tokenSource.Token);

                if (Task.WaitAny(openConnTask, Task.Delay(Program.ConnectionWaitingTime, tokenSource.Token)) == 1 || openConnTask.IsFaulted) {
                    tokenSource.Cancel();
                    throw new TimeoutException();
                }

                using (var cmd = new SqlCommand("dbo.GetAllClients", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        var entities = new List<Client>();

                        while (await reader.ReadAsync()) {
                            entities.Add(new Client {
                                User_id = reader.GetInt32(0),
                                Second_name = ((IDataRecord)reader)[1].ToString() ?? string.Empty,
                                First_name = ((IDataRecord)reader)[2].ToString() ?? string.Empty,
                                Last_name = ((IDataRecord)reader)[3].ToString() ?? string.Empty,
                                Phone_number = ((IDataRecord)reader)[4].ToString() ?? string.Empty,
                                Email = ((IDataRecord)reader)[5].ToString() ?? string.Empty,
                            });
                        }
                        return entities;
                    }
                }
            }
        }

        public async Task<User> GetByIdAsync(int id) {
            using (var connection = new SqlConnection(_connectionString)) {
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                var openConnTask = connection.OpenAsync(tokenSource.Token);

                if (Task.WaitAny(openConnTask, Task.Delay(Program.ConnectionWaitingTime, tokenSource.Token)) == 1 || openConnTask.IsFaulted) {
                    tokenSource.Cancel();
                    throw new TimeoutException();
                }

                using (var cmd = new SqlCommand("dbo.GetUserById", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        await reader.ReadAsync();
                        
                        return new User {
                            Login = ((IDataRecord)reader)[1].ToString() ?? string.Empty,
                            Password = ((IDataRecord)reader)[2].ToString() ?? string.Empty
                        };
                    }
                }
            }
        }

        public async Task<Client> GetClientByIdAsync(int id) {
            using (var connection = new SqlConnection(_connectionString)) {
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                var openConnTask = connection.OpenAsync(tokenSource.Token);

                if (Task.WaitAny(openConnTask, Task.Delay(Program.ConnectionWaitingTime, tokenSource.Token)) == 1 || openConnTask.IsFaulted) {
                    tokenSource.Cancel();
                    throw new TimeoutException();
                }

                using (var cmd = new SqlCommand("dbo.GetClientById", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        await reader.ReadAsync();

                        return new Client {
                            User_id = reader.GetInt32(0),
                            Second_name = ((IDataRecord)reader)[1].ToString() ?? string.Empty,
                            First_name = ((IDataRecord)reader)[2].ToString() ?? string.Empty,
                            Last_name = ((IDataRecord)reader)[3].ToString() ?? string.Empty,
                            Phone_number = ((IDataRecord)reader)[4].ToString() ?? string.Empty,
                            Email = ((IDataRecord)reader)[5].ToString() ?? string.Empty,
                        };
                    }
                }
            }
        }

        public async Task<bool> TryUpdateAsync(User user) {
            using (var connection = new SqlConnection(_connectionString)) {
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                var openConnTask = connection.OpenAsync(tokenSource.Token);

                if (Task.WaitAny(openConnTask, Task.Delay(Program.ConnectionWaitingTime, tokenSource.Token)) == 1 || openConnTask.IsFaulted) {
                    tokenSource.Cancel();
                    throw new TimeoutException();
                }

                using (var cmd = new SqlCommand("dbo.UpdateUser", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", user.User_id));
                    cmd.Parameters.Add(new SqlParameter("@login", user.Login));
                    cmd.Parameters.Add(new SqlParameter("@password", user.Password));

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        await reader.ReadAsync();

                        return reader.GetBoolean(0);
                    }
                }
            }
        }

        public async Task<bool> TryUpdateClientAsync(Client client) {
            using (var connection = new SqlConnection(_connectionString)) {
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                var openConnTask = connection.OpenAsync(tokenSource.Token);

                if (Task.WaitAny(openConnTask, Task.Delay(Program.ConnectionWaitingTime, tokenSource.Token)) == 1 || openConnTask.IsFaulted) {
                    tokenSource.Cancel();
                    throw new TimeoutException();
                }

                using (var cmd = new SqlCommand("dbo.UpdateClient", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", client.User_id));
                    cmd.Parameters.Add(new SqlParameter("@second_name", client.Second_name));
                    cmd.Parameters.Add(new SqlParameter("@first_name", client.First_name));
                    cmd.Parameters.Add(new SqlParameter("@last_name", client.Last_name));
                    cmd.Parameters.Add(new SqlParameter("@phone_number", client.Phone_number));
                    cmd.Parameters.Add(new SqlParameter("@email", client.Email));

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

                using (var cmd = new SqlCommand("dbo.GetUserXml", connection)) {
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

                using (var cmd = new SqlCommand("dbo.GetUsersXml", connection)) {
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
