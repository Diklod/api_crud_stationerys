using api_crud_stationerys.Models;
using Npgsql;

namespace api_crud_stationerys.Database
{
    public class EmployeeDb
    {
        public bool Add(Employee employee)
        {
            bool result = false;

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    //// VERIFICAR CASO SEJA ALTERADO
                    //if (employee.City == null)
                    //    // Default pasa cidade, caso não informado
                    //    employee.City = "Caxias do Sul";
                    //if (employee.State == null)
                    //    // Default pasa estado, caso não informado
                    //    employee.State = "RS";

                    command.CommandText = @"INSERT INTO employees " +
                                         @"(name, cpf, email, phone, job_id) " +
                                         @"VALUES " +
                                         @"(@name, @cpf, @email, @phone, @job_id);";

                    command.Parameters.AddWithValue("@name", employee.Name);
                    command.Parameters.AddWithValue("@cpf", employee.Cpf);
                    command.Parameters.AddWithValue("@email", employee.Email);
                    command.Parameters.AddWithValue("@phone", employee.Phone);
                    command.Parameters.AddWithValue("@job_id", employee.JobId);

                    AccessDb db = new AccessDb();

                    using (command.Connection = db.OpenConnection())
                    {
                        command.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public Employee Get(int id)
        {
            Employee result = new Employee();
            AccessDb db = new AccessDb();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"SELECT * FROM employees " +
                                          @"WHERE id = @id;";

                    command.Parameters.AddWithValue("@id", id);

                    using (command.Connection = db.OpenConnection())
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Id = Convert.ToInt32(reader["id"]);
                            result.Name = reader["name"].ToString();
                            result.Cpf = reader["cpf"].ToString();
                            result.Email = reader["email"].ToString();
                            result.Phone = reader["phone"].ToString();
                            result.JobId = Convert.ToInt32(reader["job_id"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return result;
        }

        public List<Employee> GetAll()
        {
            List<Employee> result = new List<Employee>();
            AccessDb db = new AccessDb();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"SELECT * FROM employees ORDER BY id;";

                    using (command.Connection = db.OpenConnection())
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee employee = new Employee();
                            employee.Id = Convert.ToInt32(reader["id"]);
                            employee.Name = reader["name"].ToString();
                            employee.Cpf = reader["cpf"].ToString();
                            employee.Email = reader["email"].ToString();
                            employee.Phone = reader["phone"].ToString();
                            employee.JobId = Convert.ToInt32(reader["job_id"]);
                            result.Add(employee);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return result;
        }

        public bool Update(Employee employee)
        {
            bool result = false;
            AccessDb db = new AccessDb();
            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"UPDATE employees " +
                                      @"SET name = @name, " +
                                      @"cpf = @cpf, " +
                                      @"email = @email, " +
                                      @"phone = @phone, " +
                                      @"job_id = @job_id " +
                                      @"WHERE id = @id;";

                    command.Parameters.AddWithValue("@id", employee.Id);
                    command.Parameters.AddWithValue("@name", employee.Name);
                    command.Parameters.AddWithValue("@cpf", employee.Cpf);
                    command.Parameters.AddWithValue("@email", employee.Email);
                    command.Parameters.AddWithValue("@phone", employee.Phone);
                    command.Parameters.AddWithValue("@job_id", employee.JobId);
                    using (command.Connection = db.OpenConnection())
                    {
                        command.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (Exception ex) { }

            return result;
        }

        public bool Delete(int id)
        {
            bool result = false;
            AccessDb db = new AccessDb();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"DELETE FROM employees WHERE id = @id;";

                    command.Parameters.AddWithValue("@id", id);

                    using (command.Connection = db.OpenConnection())
                    {
                        command.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (Exception ex) { }

            return result;
        }
    }
}
