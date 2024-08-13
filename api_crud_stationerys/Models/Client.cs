namespace api_crud_stationerys.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
    }
}
