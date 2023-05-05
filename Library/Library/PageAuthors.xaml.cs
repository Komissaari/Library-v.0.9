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
    /// Логика взаимодействия для PageAuthors.xaml
    /// </summary>
    public partial class PageAuthors : Page
    {
        public PageAuthors()
        {
            InitializeComponent();
            //DGridAuthors.ItemsSource = LibraryEntities.GetContext().Authors.ToList();

        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Manager.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridAuthors.ItemsSource = Manager.GetContext().Authors.ToList();
            }
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageAuthorsEdit());
        }

       
    }
}
