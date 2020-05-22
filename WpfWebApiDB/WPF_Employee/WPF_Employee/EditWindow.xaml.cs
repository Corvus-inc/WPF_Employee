using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace WPF_Employee
{
    /// <summary>getDepartament
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        Model model = new Model();
        ObservableCollection<string> deps;
        string InputName;
        string InputDepartament;
        Employees value;
        public EditWindow(Employees value)
        {
            InitializeComponent();
            ListEDepartamentAsync();
            this.value = value;
            InputName = value.Name;
            InputDepartament = value.Departament;
            this.textBox1.Text = value.Name;
            this.textBox2.Text = value.Departament;
        }
        public async void ListEDepartamentAsync()
        {
            deps = new ObservableCollection<string>(await model.ListDepQueryJson());
            comboBox1.ItemsSource = deps;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            
            if( textBox1.Text == InputName && InputName != "Новый сотрудник")
            {
                //updateEmployee
                int indexE = MainWindow.listE.IndexOf(value);
                Employees print = new Employees() { Name = textBox1.Text, Departament = $"{InputDepartament}|"+$"{textBox2.Text}" };
                MainWindow.listE.Remove(MainWindow.listE[indexE]);
                MainWindow.listE.Add(new Employees() { Name = textBox1.Text, Departament = textBox2.Text });
                model.UpdateDepartament(print);
            }
            else if (textBox1.Text == null) System.Windows.MessageBox.Show("Введите имя сотрудника");
            else
            {
                Employees print = new Employees() { Name = textBox1.Text, Departament = textBox2.Text };
                MainWindow.listE.Add(print);
                model.AddEmployee(print);
            }
            Close();
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textBox2.Text = comboBox1.SelectedItem.ToString();
        }
    }
}
