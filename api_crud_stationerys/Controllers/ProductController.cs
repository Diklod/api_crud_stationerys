using api_crud_stationerys.Database;
using api_crud_stationerys.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_crud_stationerys.Controllers
{
    [ApiController]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        [HttpPost]
        [Route("Add")]
        public JsonResult Add(Product product)
        {
            ProductDb productDb = new ProductDb();
            bool response = productDb.Add(product);

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
            ProductDb productDb = new ProductDb();
            Product product = productDb.Get(id);

            if (product != null && product.Id > 0)
            {
                return new JsonResult(new { success = true, data = product });
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
            ProductDb productDb = new ProductDb();
            List<Product> products = productDb.GetAll();
            if (products.Count() > 0)
            {
                return new JsonResult(new { success = true, data = products });
            }
            else
            {
                return new JsonResult(new { success = false, data = "Erro" });
            }
        }

        [HttpPut]
        [Route("Update")]
        public JsonResult Put([FromBody] Product product)
        {
            ProductDb productDb = new ProductDb();
            bool success = productDb.Update(product);

            if (success)
                return new JsonResult(new { success = true, data = "Alterado" });
            else
                return new JsonResult(new { success = false, data = "Erro" });
        }

        [HttpDelete]
        [Route("Delete")]
        public JsonResult Delete(int id)
        {
            ProductDb productdb = new ProductDb();
            bool success = productdb.Delete(id);

            if (success)
                return new JsonResult(new { success = true, data = "Excluído" });
            else
                return new JsonResult(new { success = false, data = "Erro" });
        }
    }
}
