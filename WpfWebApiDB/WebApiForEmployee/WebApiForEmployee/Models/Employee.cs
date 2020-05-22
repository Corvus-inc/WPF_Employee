using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;



namespace WebApiForEmployee.Models
{
    public class Employee:IEquatable<Employee>
    {
        public string Name { get; set; }
        public string Departament { get; set; }


        public bool Equals(Employee other)
        {

            //Check whether the compared object is null. 
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal. 
            return Departament.Equals(other.Departament) && Name.Equals(other.Name);

        }
        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null. 
            int hashProductName = Name == null ? 0 : Name.GetHashCode();

            //Get hash code for the Dep field. 
            int hashProductDepartament = Departament.GetHashCode();

            //Calculate the hash code for the product. 
            return hashProductName ^ hashProductDepartament;
        }

        
        
    }
}