using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для PageBookEdit.xaml
    /// </summary>
    public partial class PageBookEdit : Page
    {
        private Books _currentBooks = new Books();
        bool _edit;
        public PageBookEdit(Books selectedBooks, bool edit)
        {
            InitializeComponent();
            _edit = edit;
            if (selectedBooks != null) {
                TB_ID_Publication.IsEnabled =true;
                _currentBooks = selectedBooks;
            }
            DataContext = _currentBooks;
            ComBoxAuthor.ItemsSource= Manager.GetContext().Authors.ToList();
            ComBoxPublisher.ItemsSource = Manager.GetContext().Publisher.ToList();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentBooks.Name_Publication))
            {
                errors.AppendLine("Пустое поле имя публикации");
            }
            if (_currentBooks.Authors == null)
            {
                errors.AppendLine("Выберите номер автора!");
            }
            if (_currentBooks.Publisher == null)
            {
                errors.AppendLine("Выберите номер издательства!");
            }
            if (string.IsNullOrWhiteSpace(_currentBooks.Location))
            {
                errors.AppendLine("Пустое поле места расположения!");
            }
            

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            
            try 
            {
                if(!_edit)
                {
                    Manager.GetContext().Books.Add(_currentBooks);
                }               
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
