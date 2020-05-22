using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace WebApiForEmployee.Models
{
    public class GetData
    {
        SqlConnection connection;
        string script;
        public GetData()
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "Lesson8_inc",
                IntegratedSecurity = true
            };
            connection = new SqlConnection(connectionStringBuilder.ConnectionString);
            connection.Open();

        }
        private void StartCommand()
        {
            using (SqlCommand command = new SqlCommand(script, connection))
            {
                command.ExecuteNonQuery();
            }
        }
        public List<Employee> GetListEmployees()
        {
            List<Employee> Employees = new List<Employee>();

            script = "SELECT Employee, Departament FROM Employees"; //Строка запроса подключения к БД и запрос 2х столбцов данных 


            using (SqlCommand command = new SqlCommand(script, connection)) //Подключение к БД, чтение данных из нее и возвращение списка сотрудников на основе запросов даных
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Employee part = new Employee();
                    part.Name = $"{reader.GetString(0)}";
                    part.Departament = $"{reader.GetString(1)}";

                    Employees.Add(part);
                }


            }
            return Employees;
        }
        public List<string> GetListDepartament()
        {
            List<string> Dep = new List<string>();

            script = "SELECT Departament FROM Departaments"; //Строка запроса подключения к БД и запрос 2х столбцов данных 


            using (SqlCommand command = new SqlCommand(script, connection)) //Подключение к БД, чтение данных из нее и возвращение списка сотрудников на основе запросов даных
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string part = $"{reader.GetString(0)}";
                    

                    Dep.Add(part);
                }


            }
            return Dep;
        }
        public Employee GetEmployee(int id)
        {
            Employee man = new Employee();
            script = $"SELECT Employee, Departament FROM Employees WHERE Id={id}";
            using (SqlCommand command = new SqlCommand(script, connection)) //Подключение к БД, чтение данных из нее и возвращение сотрудника по номеру
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    man.Name = $"{reader.GetString(0)}";
                    man.Departament = $"{reader.GetString(1)}";
                }
                return man;
            }
        }
        public void AddEmployee(Employee value) //Открывает подключение к БД и добавляет в таблицу строку исходя из поле входящего класса
        {
            script = $@"INSERT Employees VALUES(N'{value.Name}',N'{value.Departament}')";

            StartCommand();
        }

        public void DeleteEmployee(Employee value)
        {
            script = $@"DELETE Employees WHERE Employee = N'{value.Name}' AND Departament = N'{value.Departament}'";

            StartCommand();
        }
        public void UpdateDepartament(Employee value) //Меняет отдел у входящего сотрудника. Входящий аргкумент Должен иметь в поле изменяемого свойства разделительный символ '|', между старым и новым отделом.
        {
            string[] otd = value.Departament.Split('|');

            script = $@"UPDATE Employees
                               SET Departament = N'{otd[1]}'
                               WHERE Employee =N'{value.Name}' AND Departament= N'{otd[0]}'";

            StartCommand();
        }
        public void AddDepartament(string value) //Открывает подключение к БД и добавляет в таблицу департаментов строку исходя из поля входящего класса
        {
            script = $@"INSERT Departaments VALUES(N'{value}')";

            StartCommand();
        }
        public void DeleteDepartament(string value) //Удаляет департамент и чистит все поля отделов в таблице сотрудников.
        {
            script = $@"UPDATE Employees
                               SET Departament = N'deleted'
                               WHERE Departament= N'{value}'";

            StartCommand();
            script = $@"DELETE Departaments WHERE Departament = N'{value}'";

            StartCommand();

        }

    }
}