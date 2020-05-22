using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseCreator
{

    class Program
    {

        public static void Main(string[] args)
        {
            Console.WriteLine(@"Нажмите Enter для создания БД 'Lesson8_inc' и начального списка сотрудников в  (localdb)\MSSQLLocalDB :");
            string datasource = Console.ReadLine();

            CreateNewData();


        }
        public static void CreateNewData() //Метод Делает 3 последовательных подключения. В первом создается база данных в локалхосте. Во втором доюавляется в базу данных таблица Employee. В третьем таблица заполняется начальными значениями.
        {
            string script = @"CREATE TABLE [dbo].[Employees] (
             [Id]          INT        IDENTITY (1, 1) NOT NULL,
             [Employee]    NVARCHAR(MAX) NULL,
             [Departament] NVARCHAR(MAX) NULL,
             PRIMARY KEY CLUSTERED ([Id] ASC)
                );";
            string script1 = @"CREATE TABLE [dbo].[Departaments]
                                (
	                    [Id] INT IDENTITY NOT NULL PRIMARY KEY, 
                        [Departament] NVARCHAR(MAX) NULL
                                 )";
            SqlCommand command;
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                IntegratedSecurity = true
            };
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();
                    command = new SqlCommand("CREATE DATABASE Lesson8_inc", connection);

                    command.ExecuteNonQuery();
                }
            }
            catch
            { }
            connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "Lesson8_inc",
                IntegratedSecurity = true
            };
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();
                    
                    command = new SqlCommand(script, connection);
                    command.ExecuteNonQuery();
                  
                    command = new SqlCommand(script1, connection);
                    command.ExecuteNonQuery();
                }
            }
            catch
            { }
            using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {

                connection.Open();
                var emp = new Employee();
                for (int i = 0; i < 11; i++)
                {
                    emp.Name = "Сотрудник_" + $"{i}";
                    emp.Depart = "Отдел_" + $"{i / 2}";
                    command = new SqlCommand($@"INSERT Employees VALUES(N'{emp.Name}',N'{emp.Depart}')", connection);
                    command.ExecuteNonQuery();
                }
                for (int i = 0; i < 5; i++)
                {
                    command = new SqlCommand($@"INSERT Departaments VALUES(N'Отдел_{i+1}')", connection);
                    command.ExecuteNonQuery();
                }
            }

        }
        class Employee
        {
            public string Name { get; set; }
            public string Depart { get; set; }

        }

    }
}
