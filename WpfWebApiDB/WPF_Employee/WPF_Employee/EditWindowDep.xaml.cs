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
    /// <summary>
    /// Логика взаимодействия для EditWindowDep.xaml
    /// </summary>
    public partial class EditWindowDep : Window
    {   ObservableCollection<string> deps;
        Model model = new Model();
        public EditWindowDep()
        {
            InitializeComponent();
            ListEDepartamentAsync();
        }
        public async void ListEDepartamentAsync()
        {
            deps = new ObservableCollection<string>(await model.ListDepQueryJson());
            listBoxDep.ItemsSource = deps;
        }

        private void listBoxDep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonDep2.IsEnabled = true;
        }

        private void buttonDep2_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxDep.SelectedItem != null)
            {
                string del = listBoxDep.SelectedItem.ToString();
                deps.Remove(del);
              
                model.DeleteDepartament(del);
            }
            else MessageBox.Show("Необходимо выбрать отдел");
        }

         private void textBoxDep1_SelectionChanged(object sender, RoutedEventArgs e)
        {
            buttonDep1.IsEnabled = true;
        }
        private void buttonDep1_Click_1(object sender, RoutedEventArgs e)
        {
            if (textBoxDep1.Text != null&& !deps.Contains(textBoxDep1.Text))
            {
                string add = textBoxDep1.Text;
                deps.Add(add);
                
               model.AddDepartament(add);
            }
            else MessageBox.Show("Необходимо ввести отдел, либо такой отдел уже существует");
        }

       

        // MessageBox.Show("Test");
    }
}
