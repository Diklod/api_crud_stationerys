using api_crud_stationerys.Models;
using Npgsql;

namespace api_crud_stationerys.Database
{
    public class ProductDb
    {
        public bool Add(Product product)
        {
            bool result = false;

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"INSERT INTO products " +
                                         @"(name, model, unit_val) " +
                                         @"VALUES " +
                                         @"(@name, @model, @unit_val);";

                    command.Parameters.AddWithValue("@name", product.Name);
                    command.Parameters.AddWithValue("@model", product.Model);
                    command.Parameters.AddWithValue("@unit_val", product.UnitaryValue);

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

        public Product Get(int id)
        {
            Product result = new Product();
            AccessDb db = new AccessDb();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"SELECT * FROM products " +
                                          @"WHERE id = @id;";

                    command.Parameters.AddWithValue("@id", id);

                    using (command.Connection = db.OpenConnection())
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Id = Convert.ToInt32(reader["id"]);
                            result.Name = reader["name"].ToString();
                            result.Model = reader["model"].ToString();
                            result.UnitaryValue = float.Parse(reader["unit_val"].ToString());
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

        public List<Product> GetAll()
        {
            List<Product> result = new List<Product>();
            AccessDb db = new AccessDb();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"SELECT * FROM products ORDER BY id;";

                    using (command.Connection = db.OpenConnection())
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product();
                            product.Id = Convert.ToInt32(reader["id"]);
                            product.Name = reader["name"].ToString();
                            product.Model = reader["model"].ToString();
                            product.UnitaryValue = float.Parse(reader["unit_val"].ToString());
                            result.Add(product);
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

        public bool Update(Product product)
        {
            bool result = false;
            AccessDb db = new AccessDb();
            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"UPDATE products " +
                                      @"SET name = @name, " +
                                      @"model = @model, " +
                                      @"unit_val = @unit_val " +
                                      @"WHERE id = @id;";

                    command.Parameters.AddWithValue("@id", product.Id);
                    command.Parameters.AddWithValue("@name", product.Name);
                    command.Parameters.AddWithValue("@model", product.Model);
                    command.Parameters.AddWithValue("@unit_val", product.UnitaryValue);

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
                    command.CommandText = @"DELETE FROM products WHERE id = @id;";

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
