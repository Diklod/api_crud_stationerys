using api_crud_stationerys.Database;
using api_crud_stationerys.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_crud_stationerys.Controllers
{
    [ApiController]
    [Route("api/Client")]
    public class ClientController : Controller
    {
        [HttpPost]
        [Route("Add")]
        public JsonResult Add(Client client)
        {
            ClientDb clientDb = new ClientDb();
            bool response = clientDb.Add(client);

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
            ClientDb clientDb = new ClientDb();
            Client client = clientDb.Get(id);

            if (client != null && client.Id > 0)
            {
                return new JsonResult(new { success = true, data = client });
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
            ClientDb clientDb = new ClientDb();
            List<Client> client = clientDb.GetAll();
            if (client.Count() > 0)
            {
                return new JsonResult(new { success = true, data = client });
            }
            else
            {
                return new JsonResult(new { success = false, data = "Erro" });
            }
        }

        [HttpPut]
        [Route("Update")]
        public JsonResult Put([FromBody] Client client)
        {
            ClientDb clientDb = new ClientDb();
            bool success = clientDb.Update(client);

            if (success)
                return new JsonResult(new { success = true, data = "Alterado" });
            else
                return new JsonResult(new { success = false, data = "Erro" });
        }

        [HttpDelete]
        [Route("Delete")]
        public JsonResult Delete(int id)
        {
            ClientDb clientDb = new ClientDb();
            bool success = clientDb.Delete(id);

            if (success)
                return new JsonResult(new { success = true, data = "Excluído" });
            else
                return new JsonResult(new { success = false, data = "Erro" });
        }
    }
}
