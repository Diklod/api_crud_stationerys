namespace api_crud_stationerys.Models
{
    public class Sales
    {
        public int Id { get; set; }
        public DateTime Log { get; set; }
        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public float UnitaryValue { get; set; }
        public float TotalValue { get; set; }
    }
}
