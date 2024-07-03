using Microsoft.Data.SqlClient;

using PensionAccumulationCalculator.Entities;
using PensionAccumulationCalculator.Repos.Interfaces;

using System.Configuration;
using System.Data;

namespace PensionAccumulationCalculator.Repos.Implementations {
    internal class InsuranceRecordRepo : IInsuranceRecordRepo {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        public async Task CreateAsync(Insurance_record entity) {
            using (var connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("dbo.CreateInsuranceRecord", connection)) {
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
                using (var cmd = new SqlCommand("dbo.DeleteInsuranceRecord", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    await cmd.ExecuteReaderAsync();
                }
            }
        }

        public async Task<ICollection<Insurance_record>> GetAllAsync() {
            using (var connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("dbo.GetAllInsuranceRecords", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        var entities = new List<Insurance_record>();

                        while (await reader.ReadAsync()) {
                            entities.Add(new Insurance_record {
                                Insurance_exp_id = reader.GetInt32(0),
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

        public async Task<Insurance_record> GetByIdAsync(int id) {
            using (var connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("dbo.GetInsuranceRecordById", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        await reader.ReadAsync();

                        return new Insurance_record {
                            Insurance_exp_id = reader.GetInt32(0),
                            User_id = reader.GetInt32(1),
                            Individual_pension_coefficient = reader.GetDecimal(2),
                            Year = reader.GetInt32(3),
                        };
                    }
                }
            }
        }

        public async Task UpdateAsync(Insurance_record entity) {
            using (var connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("dbo.UpdateInsuranceRecord", connection)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", entity.Insurance_exp_id));
                    cmd.Parameters.Add(new SqlParameter("@user_id", entity.User_id));
                    cmd.Parameters.Add(new SqlParameter("@individual_pension_coefficient", entity.Individual_pension_coefficient));
                    cmd.Parameters.Add(new SqlParameter("@year", entity.Year));

                    await cmd.ExecuteReaderAsync();
                }
            }
        }
    }
}
