using api_crud_stationerys.Models;
using Npgsql;

namespace api_crud_stationerys.Database
{
    public class SaleDb
    {
        public bool Add(Sale sale)
        {
            bool result = false;

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"INSERT INTO sales " +
                                         @"(log, client_id, employee_id, product_id, qty, unit_val, total_val) " +
                                         @"VALUES " +
                                         @"(@log, @client_id, @employee_id, @product_id, @qty, @unit_val, @total_val);";

                    command.Parameters.AddWithValue("@log", sale.Log);
                    command.Parameters.AddWithValue("@client_id", sale.ClientId);
                    command.Parameters.AddWithValue("@employee_id", sale.EmployeeId);
                    command.Parameters.AddWithValue("@product_id", sale.ProductId);
                    command.Parameters.AddWithValue("@qty", sale.Quantity);
                    command.Parameters.AddWithValue("@unit_val", sale.UnitaryValue);
                    command.Parameters.AddWithValue("@total_val", sale.TotalValue);

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

        public Sale Get(int id)
        {
            Sale result = new Sale();
            AccessDb db = new AccessDb();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"SELECT * FROM sales " +
                                          @"WHERE id = @id;";

                    command.Parameters.AddWithValue("@id", id);

                    using (command.Connection = db.OpenConnection())
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Id = Convert.ToInt32(reader["id"]);
                            result.Log = Convert.ToDateTime(reader["log"]);
                            result.ClientId = Convert.ToInt32(reader["client_id"]);
                            result.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                            result.ProductId = Convert.ToInt32(reader["product_id"]);
                            result.Quantity = Convert.ToInt32(reader["qty"]);
                            result.UnitaryValue = float.Parse(reader["unit_val"].ToString());
                            result.TotalValue = float.Parse(reader["total_val"].ToString());
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

        public List<Sale> GetAll()
        {
            List<Sale> result = new List<Sale>();
            AccessDb db = new AccessDb();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"SELECT * FROM sales ORDER BY id;";

                    using (command.Connection = db.OpenConnection())
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Sale sale = new Sale();
                            sale.Id = Convert.ToInt32(reader["id"]);
                            sale.Log = Convert.ToDateTime(reader["log"]);
                            sale.ClientId = Convert.ToInt32(reader["client_id"]);
                            sale.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                            sale.ProductId = Convert.ToInt32(reader["product_id"]);
                            sale.Quantity = Convert.ToInt32(reader["qty"]);
                            sale.UnitaryValue = float.Parse(reader["unit_val"].ToString());
                            sale.TotalValue = float.Parse(reader["total_val"].ToString());
                            result.Add(sale);
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

        public bool Update(Sale sale)
        {
            bool result = false;
            AccessDb db = new AccessDb();
            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"UPDATE sales " +
                                      @"SET log = @log, " +
                                      @"client_id = @client_id, " +
                                      @"employee_id = @employee_id, " +
                                      @"product_id = @product_id, " +
                                      @"qty = @qty, " +
                                      @"unit_val = @unit_val," +
                                      @"total_val = @total_val " +
                                      @"WHERE id = @id;";

                    command.Parameters.AddWithValue("@id", sale.Id);
                    command.Parameters.AddWithValue("@log", sale.Log);
                    command.Parameters.AddWithValue("@client_id", sale.ClientId);
                    command.Parameters.AddWithValue("@employee_id", sale.EmployeeId);
                    command.Parameters.AddWithValue("@product_id", sale.ProductId);
                    command.Parameters.AddWithValue("@qty", sale.Quantity);
                    command.Parameters.AddWithValue("@unit_val", sale.UnitaryValue);
                    command.Parameters.AddWithValue("@total_val", sale.TotalValue);

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
                    command.CommandText = @"DELETE FROM sales WHERE id = @id;";

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

        // Métodos Adicionais
        public List<Sale> GetAllByDate(DateTime firstDate, DateTime secondDate)
        {
            if (firstDate > secondDate)
            {
                DateTime auxDate = firstDate;
                firstDate = secondDate;
                secondDate = auxDate;
            }
            List<Sale> result = new List<Sale>();
            AccessDb db = new AccessDb();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"SELECT * FROM sales WHERE log >= @firstDateTime and log <= @secondDateTime ORDER BY log;
";

                    command.Parameters.AddWithValue("@firstDateTime", firstDate);
                    command.Parameters.AddWithValue("@secondDateTime", secondDate);

                    using (command.Connection = db.OpenConnection())
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Sale sale = new Sale();
                            sale.Id = Convert.ToInt32(reader["id"]);
                            sale.Log = Convert.ToDateTime(reader["log"]);
                            sale.ClientId = Convert.ToInt32(reader["client_id"]);
                            sale.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                            sale.ProductId = Convert.ToInt32(reader["product_id"]);
                            sale.Quantity = Convert.ToInt32(reader["qty"]);
                            sale.UnitaryValue = float.Parse(reader["unit_val"].ToString());
                            sale.TotalValue = float.Parse(reader["total_val"].ToString());
                            result.Add(sale);
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
        public List<Sale> GetAllByEmployee(int employeeId)
        {
            List<Sale> result = new List<Sale>();
            AccessDb db = new AccessDb();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"SELECT * FROM sales WHERE employee_id = @employee_id ORDER BY log;";

                    command.Parameters.AddWithValue("@employee_id", employeeId);

                    using (command.Connection = db.OpenConnection())
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Sale sale = new Sale();
                            sale.Id = Convert.ToInt32(reader["id"]);
                            sale.Log = Convert.ToDateTime(reader["log"]);
                            sale.ClientId = Convert.ToInt32(reader["client_id"]);
                            sale.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                            sale.ProductId = Convert.ToInt32(reader["product_id"]);
                            sale.Quantity = Convert.ToInt32(reader["qty"]);
                            sale.UnitaryValue = float.Parse(reader["unit_val"].ToString());
                            sale.TotalValue = float.Parse(reader["total_val"].ToString());
                            result.Add(sale);
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

        public List<Sale> GetAllByProduct(int productId)
        {
            List<Sale> result = new List<Sale>();
            AccessDb db = new AccessDb();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"SELECT * FROM sales WHERE product_id = @product_id ORDER BY log;";

                    command.Parameters.AddWithValue("@product_id", productId);

                    using (command.Connection = db.OpenConnection())
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Sale sale = new Sale();
                            sale.Id = Convert.ToInt32(reader["id"]);
                            sale.Log = Convert.ToDateTime(reader["log"]);
                            sale.ClientId = Convert.ToInt32(reader["client_id"]);
                            sale.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                            sale.ProductId = Convert.ToInt32(reader["product_id"]);
                            sale.Quantity = Convert.ToInt32(reader["qty"]);
                            sale.UnitaryValue = float.Parse(reader["unit_val"].ToString());
                            sale.TotalValue = float.Parse(reader["total_val"].ToString());
                            result.Add(sale);
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
    }
}
