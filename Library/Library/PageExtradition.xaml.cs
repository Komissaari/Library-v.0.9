using Microsoft.Win32;
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
    /// Логика взаимодействия для PageExtradition.xaml
    /// </summary>
    public partial class PageExtradition : Page
    {
        public PageExtradition()
        {
            InitializeComponent();
            DGridExtradition.ItemsSource = Manager.GetContext().Extradition.ToList();
        }

        

        private void DGridExtradition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonReturnBook.IsEnabled = false;
            var bb = (Extradition)DGridExtradition.SelectedItem;
            if(bb.Date_Return==null)
            {
                ButtonReturnBook.IsEnabled = true;
            }
        }

        private void ButtonReturnBook_Click(object sender, RoutedEventArgs e)
        {
            var bb = (Extradition)DGridExtradition.SelectedItem;
            bb.Date_Return = DateTime.Now;
            Manager.GetContext().SaveChanges();

            DGridExtradition.ItemsSource = Manager.GetContext().Extradition.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var allDljniki = Manager.GetContext().Extradition.Where(w => w.Date_Return == null).ToList();
            
            string Text = "";
            foreach(var dljniki in allDljniki)
            {
                Text += $"{dljniki.ID_Extradition}\t{dljniki.Login_Readers}\t{dljniki.ID_Publication}\t{dljniki.Date_Issue}\t{dljniki.Date_Delivery}\n";
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files(*.txt)|*.txt";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.AddExtension = true;
            if (saveFileDialog.ShowDialog() == false)
                return;
            
            string filename = saveFileDialog.FileName;
            // сохраняем текст в файл

            
            System.IO.File.WriteAllText(filename, Text);
            MessageBox.Show("Файл сохранен");

        }
    }
}
