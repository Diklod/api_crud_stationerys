using api_crud_stationerys.Database;
using api_crud_stationerys.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_crud_stationerys.Controllers
{
    [ApiController]
    [Route("api/Employee")]
    public class EmployeeController : Controller
    {
        [HttpPost]
        [Route("Add")]
        public JsonResult Add(Employee employee)
        {
            EmployeeDb employeeDb = new EmployeeDb();
            bool response = employeeDb.Add(employee);

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
            EmployeeDb employeeDb = new EmployeeDb();
            Employee employee = employeeDb.Get(id);

            if (employee != null && employee.Id > 0)
            {
                return new JsonResult(new { success = true, data = employee });
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
            EmployeeDb employeeDb = new EmployeeDb();
            List<Employee> employees = employeeDb.GetAll();
            if (employees.Count() > 0)
            {
                return new JsonResult(new { success = true, data = employees });
            }
            else
            {
                return new JsonResult(new { success = false, data = "Erro" });
            }
        }

        [HttpPut]
        [Route("Update")]
        public JsonResult Put([FromBody] Employee employee)
        {
            EmployeeDb employeeDb = new EmployeeDb();
            bool success = employeeDb.Update(employee);

            if (success)
                return new JsonResult(new { success = true, data = "Alterado" });
            else
                return new JsonResult(new { success = false, data = "Erro" });
        }

        [HttpDelete]
        [Route("Delete")]
        public JsonResult Delete(int id)
        {
            EmployeeDb employeeDb = new EmployeeDb();
            bool success = employeeDb.Delete(id);

            if (success)
                return new JsonResult(new { success = true, data = "Excluído" });
            else
                return new JsonResult(new { success = false, data = "Erro" });
        }
    }
}
