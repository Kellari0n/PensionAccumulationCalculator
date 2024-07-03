using Microsoft.Data.SqlClient;

using PensionAccumulationCalculator.Entities;
using PensionAccumulationCalculator.Repos.Interfaces;

using System.Configuration;
using System.Data;

namespace PensionAccumulationCalculator.Repos.Implementations {
    internal class WorkRecordRepo : IWorkRecordRepo {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        public async Task CreateAsync(Work_record entity) {
            using (var connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("dbo.CreateWorkRecord", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@user_id", entity.User_id));
                    cmd.Parameters.Add(new SqlParameter("@individual_pension_coefficient", entity.Individual_pension_coefficient));
                    cmd.Parameters.Add(new SqlParameter("@year", entity.Year));

                    await cmd.ExecuteReaderAsync();
                }
            }
        }

        public async Task DeleteAsync(int id) {
            using (var connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("dbo.DeleteWorkRecord", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    await cmd.ExecuteReaderAsync();
                }
            }
        }

        public async Task<ICollection<Work_record>> GetAllAsync() {
            using (var connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
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
                await connection.OpenAsync();
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

        public async Task UpdateAsync(Work_record entity) {
            using (var connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("dbo.UpdateWorkRecord", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", entity.Work_exp_id));
                    cmd.Parameters.Add(new SqlParameter("@user_id", entity.User_id));
                    cmd.Parameters.Add(new SqlParameter("@individual_pension_coefficient", entity.Individual_pension_coefficient));
                    cmd.Parameters.Add(new SqlParameter("@year", entity.Year));

                    await cmd.ExecuteReaderAsync();
                }
            }
        }
    }
}
