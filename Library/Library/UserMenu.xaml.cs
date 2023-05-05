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

namespace Library
{
    /// <summary>
    /// Логика взаимодействия для UserMenu.xaml
    /// </summary>
    public partial class UserMenu : Window
    {
        List<Tuple<int,int>> _items = new List<Tuple<int, int>>();

        public UserMenu()
        {
            InitializeComponent();

            UpdateDisplay();

        }


        public void UpdateDisplay()
        {
            LibraryEntities1 model = new LibraryEntities1();
            var allBooks = model.Books.ToList();

            ComboBoxBooks.Items.Clear();
            _items.Clear();
            Vzitie.Items.Clear();

            foreach (var book in allBooks)
            {
                var ext = model.Extradition.Where(w => w.ID_Publication == book.ID_Publication && w.Date_Return == null).Count();

                if (ext == 0)
                {
                    ComboBoxBooks.Items.Add($"{book.Name_Publication} ({book.Authors.Au_Name})");
                    _items.Add(new Tuple<int, int>(ComboBoxBooks.Items.Count - 1, book.ID_Publication));
                }               

            }

            var extt = model.Extradition.Where(w => w.Login_Readers == Manager.Login && w.Date_Return==null).ToList();

            foreach (var ex in extt)
            {
                Vzitie.Items.Add($"{ex.Books.Name_Publication} ({ex.Books.Authors.Au_Name})");
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LibraryEntities1 model = new LibraryEntities1();

            if (ComboBoxBooks.SelectedIndex!=-1)
            {
                var book = _items.Single(s => s.Item1 == ComboBoxBooks.SelectedIndex);
                var modelBook = model.Books.Single(s => s.ID_Publication == book.Item2);

                Extradition extradition = new Extradition();
                extradition.ID_Publication = modelBook.ID_Publication;
                extradition.Login_Readers = Manager.Login;
                extradition.Date_Issue = DateTime.Now;
                extradition.Date_Delivery = DateTime.Now.AddMonths(1);

                model.Extradition.Add(extradition);
                model.SaveChanges();

                UpdateDisplay();
            }
        }
    }

}
