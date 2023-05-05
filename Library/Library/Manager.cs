using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Library
{
    internal class Manager
    {
        private static LibraryEntities1 _context;
        public static Frame MainFrame { get; set; }
        public static string Login { get; set; }

        public static MainWindow mainWindow  { get; set; }


        public static LibraryEntities1 GetContext()
        {
            if (_context == null)
                _context = new LibraryEntities1();
            return _context;
        }
    }
}
