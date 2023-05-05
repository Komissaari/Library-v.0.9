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

namespace Library
{
    /// <summary>
    /// Логика взаимодействия для PageAuthorsEdit.xaml
    /// </summary>
    public partial class PageAuthorsEdit : Page
    {
        private Authors _currentAuthors = new Authors(); 
        public PageAuthorsEdit()
        {
            InitializeComponent();
            /*if (selectedAuthors != null)
            {
                TB_ID_Publication.IsEnabled = false;
                selectedAuthors = selectedAuthors;
            }
            DataContext = _currentAuthors;*/
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentAuthors.Au_Surname))
                errors.AppendLine("Пустое поле фамилии автора!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            try
            {
                Manager.GetContext().Authors.Add(_currentAuthors);
                Manager.GetContext().SaveChanges();
                MessageBox.Show("сохранено ...");
                Manager.MainFrame.GoBack();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }
        }
    }
}
