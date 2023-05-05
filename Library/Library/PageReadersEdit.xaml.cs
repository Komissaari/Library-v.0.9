using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Library
{
    /// <summary>
    /// Логика взаимодействия для PageReadersEdit.xaml
    /// </summary>
    public partial class PageReadersEdit : Page
    {
        private Readers _currentReaders = new Readers();
        public PageReadersEdit()
        {
            InitializeComponent();
            DataContext = _currentReaders;
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentReaders.Surname))
                errors.AppendLine("Пустое поле фамилии читателя!");
            if (string.IsNullOrWhiteSpace(_currentReaders.Name))
                errors.AppendLine("Пустое поле имени читателя!");
            if (string.IsNullOrWhiteSpace(_currentReaders.Reader_Password))
                errors.AppendLine("Пустое поле пароля читателя!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            try
            {
                Manager.GetContext().Readers.Add(_currentReaders);
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
