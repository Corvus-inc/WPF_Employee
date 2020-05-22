using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Http;


namespace WPF_Employee
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Model model = new Model();
        public static ObservableCollection<Employees> listE;
        Employees inc ;
        public  MainWindow()    
        {
           
            InitializeComponent();
            
            ListEmployeeAsync();

          
            


        }
        public async void ListEmployeeAsync()
        {
            listE = new ObservableCollection<Employees>(await model.ListQueryJson());
            listBox1.ItemsSource = listE;
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                inc = new Employees(listBox1.SelectedItem as Employees);
                listE.Remove((Employees)listBox1.SelectedItem);               
                model.Delete(inc);
            }
            //new EditWindow(listBox1.SelectedItem as Employees).ShowDialog();
            else MessageBox.Show("Необходимо выбрать сотрудника");
        }

        private void button2_Click(object sender, RoutedEventArgs e) //Кнопка Изменить и Добавить
        {
            if (listBox1.SelectedItem != null)
            
                new EditWindow(listBox1.SelectedItem as Employees).ShowDialog();

            else new EditWindow( inc= new Employees() {Name="Новый сотрудник", Departament="Выберите отдел" }).ShowDialog(); ;
        }

        private void button3_Click(object sender, RoutedEventArgs e) //Кнопка Редактора отделов
        {
            new EditWindowDep().ShowDialog();
        }

        //public void DeleteE() //Удаляет все значения В таблице основываясь на джейсон запросе.
        //{
        //    string url = "https://localhost:44375/deleteEmployee";

        //    HttpClient httpClient = new HttpClient();

        //    string myObj =
        //        @"{
        //        'Name':'Раб1',
        //        'Departament':'ОТД1'
        //        }";
        //    var content = new StringContent(myObj, Encoding.UTF8, "application/json");
        //    var res = httpClient.PostAsync(url, content).Result;

        //    MessageBox.Show(res.ToString());
        //}
        //public void CreateE()
        //{
        //    string url = "https://localhost:44375/addEmployee";

        //    HttpClient httpClient = new HttpClient();

        //    string myObj =
        //        @"{
        //        'Name':'Раб1',
        //        'Departament':'ОТД1'
        //        }";
        //    var content = new StringContent(myObj, Encoding.UTF8, "application/json");
        //    var res = httpClient.PostAsync(url, content).Result;

        //    MessageBox.Show(res.ToString());

        //}
        //public void AddDepartament()
        //{
        //    string url = "https://localhost:44375/addDepartament";

        //    HttpClient httpClient = new HttpClient();

        //    string myObj =
        //        @"{
        //        'Отдел_5'
        //        }";
        //    var content = new StringContent(myObj, Encoding.UTF8, "application/json");
        //    var res = httpClient.PostAsync(url, content).Result;

        //    MessageBox.Show(res.ToString());

        //}public void DeleteDepartament()
        //{
        //    string url = "https://localhost:44375/deleteDepartament";

        //    HttpClient httpClient = new HttpClient();

        //    string myObj =
        //        @"
        //        'Отдел_5'
        //        ";
        //    var content = new StringContent(myObj, Encoding.UTF8, "application/json");
        //    var res = httpClient.PostAsync(url, content).Result;

        //    MessageBox.Show(res.ToString());

        //}

    }
}
