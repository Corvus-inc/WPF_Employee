using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Threading;

namespace WPF_Employee
{

    class Model
    {
        string myObj { get; set; } //Строка которая будет определять тело запроса
        //string myObj =
        //        @"{
        //        'Name':'Раб1',
        //        'Departament':'ОТД2|ОТД8'
        //        }";
        //string myObj2 =
        //        @"{
        //        'Name':'Раб1',
        //        'Departament':'ОТД1'
        //        }";
        //string myObj3 =
        //        @"
        //        'Отдел_5'
        //        ";

        //string[] querys = { "getEmployees", "addEmployee", "deleteEmployee", "updateDepartament", "addDepartament", "deleteDepartament" };
        public void QueryJson(string query, string bodyJson) //Отфправляет Джейсон запрос.
        {
            string url = @"https://localhost:44375/" + $"{query}";

            HttpClient httpClient = new HttpClient();

            var content = new StringContent(bodyJson, Encoding.UTF8, "application/json");
            httpClient.PostAsync(url, content);
            
        }
        public async Task<List<Employees>> ListQueryJson()
        {
            string url = @"https://localhost:44375/getEmployees";
            HttpClient httpClient = new HttpClient();
            var stringTask = httpClient.GetStreamAsync(url);
           

            var streamTask = httpClient.GetStreamAsync(url);
            var repositories = await JsonSerializer.DeserializeAsync<List<Employees>>(await streamTask);
            

            return repositories;
        }

        public async Task<List<string>> ListDepQueryJson()
        {
            string url = @"https://localhost:44375/getDepartament";
            HttpClient httpClient = new HttpClient();
            var stringTask = httpClient.GetStreamAsync(url);


            var streamTask = httpClient.GetStreamAsync(url);
            var repositories = await JsonSerializer.DeserializeAsync<List<string>>(await streamTask);


            return repositories;
        }

        public void ConnectToWeb(string myQuery, string myObj)
        {
            string url = @"https://localhost:44375/"+$"{myQuery}";
            HttpClient httpClient = new HttpClient();

            var content = new StringContent(myObj, Encoding.UTF8, "application/json");
            var res = httpClient.PostAsync(url, content).Result;
        }
        public void Delete(Employees print)
        {
            string myQuery = "deleteEmployee";
            string myObj =
                @"
                  {
                  'Name':"+$"'{print.Name}'"+@",
                  'Departament':"+$"'{print.Departament}'"+@"
                }";
            ConnectToWeb(myQuery, myObj);
        }
        public void DeleteDepartament(string print) //Формирует строки запроса на удаление отдела и вызывает метод подключения к Сервису с использованием этих строк
        {
            string myQuery = "deleteDepartament";
            string myObj =
               $@"'{print}'";
            ConnectToWeb(myQuery, myObj);
            
        }
        
        public void AddDepartament(string print) //Формирует строки запроса на добавление отдела и вызывает метод подключения к Сервису с использованием этих строк
        {
            string myQuery = "addDepartament";
            string myObj =
               $@"'{print}'";
            ConnectToWeb(myQuery, myObj);
            
        }
        public void AddEmployee(Employees print)
        {
            string myQuery = "addEmployee";
            string myObj =
               @"
                {
                  'Name':"+$"'{print.Name}'"+@",
                  'Departament':"+$"'{print.Departament}'"+@"
                }";
            ConnectToWeb(myQuery, myObj);
        }

        public void UpdateDepartament(Employees print) //Метод принимает экземпляр класса Сотрудника, но значение Департамента ОБЯЗАТЕЛЬНО должно быть прописано через "|" Где слева от символа старое значение, а справа новое значение: на которое изменяется.
        {
            string myQuery = "updateDepartament";
            string myObj =
                     @"{
                     'Name':" + $"'{print.Name}'" + @",
                     'Departament':" + $"'{print.Departament}'" + @"
                     }";
            ConnectToWeb(myQuery, myObj);
        }

    }
}
