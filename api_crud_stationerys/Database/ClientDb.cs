using api_crud_stationerys.Database;
using api_crud_stationerys.Models;
using Npgsql;

namespace api_crud_stationerys.Database
{
    public class ClientDb
    {
        public bool Add(Client client)
        {
            bool result = false;

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"INSERT INTO clients " +
                                         @"(name, cpf, email, phone, city, state) " +
                                         @"VALUES " +
                                         @"(@name, @cpf, @email, @phone, @city, @state);";

                    command.Parameters.AddWithValue("@name", client.Name);
                    command.Parameters.AddWithValue("@cpf", client.Cpf);
                    command.Parameters.AddWithValue("@email", client.Email);
                    command.Parameters.AddWithValue("@phone", client.Phone);
                    command.Parameters.AddWithValue("@city", client.City);
                    command.Parameters.AddWithValue("@state", client.State);

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

        public Client Get(int id)
        {
            Client result = new Client();
            AccessDb db = new AccessDb();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"SELECT * FROM clients " +
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
                            result.City = reader["city"].ToString();
                            result.State = reader["state"].ToString();
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

        public List<Client> GetAll()
        {
            List<Client> result = new List<Client>();
            AccessDb db = new AccessDb();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"SELECT * FROM clients ORDER BY id;";

                    using (command.Connection = db.OpenConnection())
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Client client = new Client();
                            client.Id = Convert.ToInt32(reader["id"]);
                            client.Name = reader["name"].ToString();
                            client.Cpf = reader["cpf"].ToString();
                            client.Email = reader["email"].ToString();
                            client.Phone = reader["phone"].ToString();
                            client.City = reader["city"].ToString();
                            client.State = reader["state"].ToString();
                            result.Add(client);
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

        public bool Update(Client client)
        {
            bool result = false;
            AccessDb db = new AccessDb();
            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"UPDATE clients " +
                                      @"SET name = @name, " +
                                      @"cpf = @cpf, " +
                                      @"email = @email " +
                                      @"phone = @phone " +
                                      @"city = @city " +
                                      @"state = @state " +
                                      @"WHERE id = @id;";

                    command.Parameters.AddWithValue("@name", client.Name);
                    command.Parameters.AddWithValue("@cpf", client.Cpf);
                    command.Parameters.AddWithValue("@email", client.Email);
                    command.Parameters.AddWithValue("@phone", client.Phone);
                    command.Parameters.AddWithValue("@city", client.City);
                    command.Parameters.AddWithValue("@state", client.State);

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
                    command.CommandText = @"DELETE FROM clients WHERE id = @id;";

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
