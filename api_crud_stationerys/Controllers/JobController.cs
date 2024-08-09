using api_crud_stationerys.Database;
using api_crud_stationerys.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_crud_stationerys.Controllers
{
    [ApiController]
    [Route("api/Job")]
    public class JobController : Controller
    {
        [HttpPost]
        [Route("Add")]
        public JsonResult Add(Job job)
        {
            JobDb jobDb = new JobDb();
            bool response = jobDb.Add(job);

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
            JobDb jobDb = new JobDb();
            Job job = jobDb.Get(id);

            if (job != null && job.Id > 0)
            {
                return new JsonResult(new { success = true, data = job });
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
            JobDb jobDb = new JobDb();
            List<Job> jobs = jobDb.GetAll();
            if (jobs.Count() > 0)
            {
                return new JsonResult(new { success = true, data = jobs });
            }
            else
            {
                return new JsonResult(new { success = false, data = "Erro" });
            }
        }

        [HttpPut]
        [Route("Update")]
        public JsonResult Put([FromBody] Job job)
        {
            JobDb jobDb = new JobDb();
            bool success = jobDb.Update(job);

            if (success)
                return new JsonResult(new { success = true, data = "Alterado" });
            else
                return new JsonResult(new { success = false, data = "Erro" });
        }

        [HttpDelete]
        [Route("Delete")]
        public JsonResult Delete(int id)
        {
            JobDb jobDb = new JobDb();
            bool success = jobDb.Delete(id);

            if (success)
                return new JsonResult(new { success = true, data = "Excluído" });
            else
                return new JsonResult(new { success = false, data = "Erro" });
        }
    }
}
