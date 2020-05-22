using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WPF_Employee
{
    public class Employees
    {

        public string Name { get; set; }
        public string Departament { get; set; }
        public Employees()
        {
  
        }

        public Employees (Employees value)
        {
            this.Name = value.Name;
            this.Departament = value.Departament;
        }
    }

}
