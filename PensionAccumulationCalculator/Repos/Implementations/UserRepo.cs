using Microsoft.Data.SqlClient;

using PensionAccumulationCalculator.Entities;
using PensionAccumulationCalculator.Repos.Interfaces;

using System.Configuration;
using System.Data;

namespace PensionAccumulationCalculator.Repos.Implementations {
    internal class UserRepo : IUserRepo {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        public async void Create(User user) {
            using (var connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (var cmd = new SqlCommand("dbo.CreateUser", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@login", user.Login));
                    cmd.Parameters.Add(new SqlParameter("@password", user.Password));
                    
                    using (var reader = cmd.ExecuteReaderAsync()) {
                        await reader;
                    }
                }
            }
        }

        public async void Delete(int id) {
            using (var connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (var cmd = new SqlCommand("dbo.DeleteUser", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", id)); 

                    using (var reader = cmd.ExecuteReaderAsync()) {
                        await reader;
                    }
                }
            }
        }

        public ICollection<User> GetEntities() {
            using (var connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (var cmd = new SqlCommand("dbo.GetAllUsers", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader()) {
                        ICollection<User> entities = new List<User>();

                        while (reader.Read()) {
                            entities.Add(new User { 
                                Login = ((IDataRecord)reader)[1].ToString() ?? string.Empty, 
                                Password = ((IDataRecord)reader)[2].ToString() ?? string.Empty 
                            });
                        }
                        return entities;
                    }
                }
            }
        }

        public User GetEntity(int id) {
            using (var connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (var cmd = new SqlCommand("dbo.GetUserById", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    using (var reader = cmd.ExecuteReader()) {
                        reader.Read();
                        
                        return new User {
                            Login = ((IDataRecord)reader)[1].ToString() ?? string.Empty,
                            Password = ((IDataRecord)reader)[2].ToString() ?? string.Empty
                        };
                    }
                }
            }
        }

        public async void Update(User user) {
            using (var connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (var cmd = new SqlCommand("dbo.UpdateUser", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", user.User_id));
                    cmd.Parameters.Add(new SqlParameter("@login", user.Login));
                    cmd.Parameters.Add(new SqlParameter("@password", user.Password));

                    using (var reader = cmd.ExecuteReaderAsync()) {
                        await reader;
                    }
                }
            }
        }
    }
}
