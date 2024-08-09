using api_crud_stationerys.Models;
using Npgsql;

namespace api_crud_stationerys.Database
{
    public class JobDb
    {
        public bool Add(Job job)
        {
            bool result = false;

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"INSERT INTO jobs " +
                                         @"(name, salary, description) " +
                                         @"VALUES " +
                                         @"(@name, @salary, @description);";

                    command.Parameters.AddWithValue("@name", job.Name);
                    command.Parameters.AddWithValue("@salary", job.Salary);
                    command.Parameters.AddWithValue("@description", job.Description);

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

        public Job Get(int id)
        {
            Job result = new Job();
            AccessDb db = new AccessDb();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"SELECT * FROM jobs " +
                                          @"WHERE id = @id;";

                    command.Parameters.AddWithValue("@id", id);

                    using (command.Connection = db.OpenConnection())
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Id = Convert.ToInt32(reader["id"]);
                            result.Name = reader["name"].ToString();
                            result.Salary = float.Parse(reader["salary"].ToString());
                            result.Description = reader["description"].ToString();
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

        public List<Job> GetAll()
        {
            List<Job> result = new List<Job>();
            AccessDb db = new AccessDb();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"SELECT * FROM jobs ORDER BY id;";

                    using (command.Connection = db.OpenConnection())
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Job job = new Job();
                            job.Id = Convert.ToInt32(reader["id"]);
                            job.Name = reader["name"].ToString();
                            job.Salary = float.Parse(reader["salary"].ToString());
                            job.Description = reader["description"].ToString();
                            result.Add(job);
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

        public bool Update(Job job)
        {
            bool result = false;
            AccessDb db = new AccessDb();
            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"UPDATE jobs " +
                                      @"SET name = @name, " +
                                      @"salary = @salary, " +
                                      @"description = @description " +
                                      @"WHERE id = @id;";

                    command.Parameters.AddWithValue("@id", job.Id);
                    command.Parameters.AddWithValue("@name", job.Name);
                    command.Parameters.AddWithValue("@salary", job.Salary);
                    command.Parameters.AddWithValue("@description", job.Description);

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
                    command.CommandText = @"DELETE FROM jobs WHERE id = @id;";

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
