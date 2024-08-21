using api_crud_stationerys.Database;
using api_crud_stationerys.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_crud_stationerys.Controllers
{
    [ApiController]
    [Route("api/Sale")]
    public class SaleController : Controller
    {
        [HttpPost]
        [Route("Add")]
        public JsonResult Add(Sale sale)
        {
            SaleDb saleDb = new SaleDb();
            bool response = saleDb.Add(sale);

            if (response)
            {
                return new JsonResult(new { success = true, data = "Cadastrado" });
            }

            else
            {
                return new JsonResult(new { success = false, data = "Erro" });
            }
        }

        [HttpGet]
        [Route("Get")]
        public JsonResult Get(int id)
        {
            SaleDb saleDb = new SaleDb();
            Sale sale = saleDb.Get(id);

            if (sale != null && sale.Id > 0)
            {
                return new JsonResult(new { success = true, data = sale });
            }

            else
            {
                return new JsonResult(new { success = false, data = "Erro" });
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public JsonResult GetAll()
        {
            SaleDb saleDb = new SaleDb();
            List<Sale> sale = saleDb.GetAll();
            if (sale.Count() > 0)
            {
                return new JsonResult(new { success = true, data = sale });
            }
            else
            {
                return new JsonResult(new { success = false, data = "Erro" });
            }
        }

        [HttpPut]
        [Route("Update")]
        public JsonResult Put([FromBody] Sale sale)
        {
            SaleDb saleDb = new SaleDb();
            bool success = saleDb.Update(sale);

            if (success)
                return new JsonResult(new { success = true, data = "Alterado" });
            else
                return new JsonResult(new { success = false, data = "Erro" });
        }

        [HttpDelete]
        [Route("Delete")]
        public JsonResult Delete(int id)
        {
            SaleDb saleDb = new SaleDb();
            bool success = saleDb.Delete(id);

            if (success)
                return new JsonResult(new { success = true, data = "Excluído" });
            else
                return new JsonResult(new { success = false, data = "Erro" });
        }

        [HttpGet]
        [Route("SalesByEmployee")]
        public JsonResult SalesByEmployee(int employeeId)
        {
            SaleDb saleDb= new SaleDb();
            List<Sale> sale = saleDb.GetAllByEmployee(employeeId);
            if (sale.Count() > 0)
            {
                return new JsonResult(new { success = true, data = sale });
            }
            else
            {
                return new JsonResult(new { success = false, data = "Erro" });
            }
        }

        [HttpGet]
        [Route("SalesByProduct")]
        public JsonResult SalesByProduct(int productId)
        {
            SaleDb saleDb = new SaleDb();
            List<Sale> sale = saleDb.GetAllByProduct(productId);
            if (sale.Count() > 0)
            {
                return new JsonResult(new { success = true, data = sale });
            }
            else
            {
                return new JsonResult(new { success = false, data = "Erro" });
            }
        }

        [HttpGet]
        [Route("SalesByDate")]
        public JsonResult SalesByDate(DateTime firstDate, DateTime secondDate)
        {
            SaleDb saleDb = new SaleDb();
            List<Sale> sale = saleDb.GetAllByDate(firstDate, secondDate);
            if (sale.Count() > 0)
            {
                return new JsonResult(new { success = true, data = sale });
            }
            else
            {
                return new JsonResult(new { success = false, data = "Erro" });
            }
        }
    }
}
