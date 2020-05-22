using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiForEmployee.Models;
using System.Data.SqlClient;
using System.Runtime.Serialization.Json;


///api/Employees
namespace WebApiForEmployee.Controllers
{
    public class EmployeesController : ApiController
    {
        GetData getData = new GetData();

        [Route("getEmployees")]
        public List<Employee> GetListEmployees() // Ответ на запрос, возвращающий значения в БД
        {
            GetData listCreate = new GetData();
            List<Employee> insert = listCreate.GetListEmployees();

            return insert;

        }

        [Route("getDepartament")]
        public List<string> GetListDepartament() // Ответ на запрос, возвращающий значения в БД
        {
            GetData listCreate = new GetData();
            List<string> insert = listCreate.GetListDepartament();

            return insert;

        }

        [Route("addEmployee")]
        public HttpResponseMessage PostAdd([FromBody]Employee value) //Метод, принимающий из тела запроса значение класса Сотрудника и добавляющий его в БД. Возвращает код операции.
        {
            getData.AddEmployee(value);
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [Route("deleteEmployee")]
        public HttpResponseMessage PostDelete([FromBody]Employee value)
        {
            getData.DeleteEmployee(value);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("updateDepartament")]
        public HttpResponseMessage PostUpdate([FromBody]Employee value)
        {
            getData.UpdateDepartament(value);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("addDepartament")]
        public HttpResponseMessage PostAddDepartament([FromBody]string value)
        {
            getData.AddDepartament(value);
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [Route("deleteDepartament")]
        public HttpResponseMessage PostDeleteDepartament([FromBody]string value)
        {
            getData.DeleteDepartament(value);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
        
        
        //[Route("distinctEmployees")]
        //public List<Employee> GetListEmployeesDistinct() //Вернуть лист сотрудников, без повторений.
        //{
        //    List<Employee> insert = GetListEmployees();

        //    var noduble = insert.Distinct();

        //    List <Employee> listnodouble = new List<Employee>();
        //    foreach (var el in noduble)
        //    {
        //       listnodouble.Add(el);
        //    }
        //    return listnodouble;
            
        //}

    }
}
