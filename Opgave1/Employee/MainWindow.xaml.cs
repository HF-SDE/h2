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
using System.Windows.Navigation;
using System.Windows.Shapes;
using EmployeeBLL;

namespace EmployeeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnClick1(object sender, RoutedEventArgs e)
        {
            var employee = new Employee();
            string id;
            if (sirName.Text.Length != 0 && sirName.Text.Length != 0)
            {
                id = employee.CreateEmployeeID(sirName.Text, surName.Text);
                ID.Text = id;
                MAIL.Text = id + "@fiktivefirma.dk";
            }

        }
    }
}
