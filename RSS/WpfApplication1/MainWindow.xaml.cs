using RSS_db;
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

namespace WpfApplication1
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new RssContext())
            {
                var news = new News
                {
                    NewsId = 3,
                    Category = "Sport",
                    Description = "Ktos cos wygral jeszcze raz",
                    Link = "www.sport.wp.pl/lingcos221",
                    PubDate = new DateTime(2017, 04, 06),
                    SourceId = 1,
                    Title = "Wygrana!!! - po raz kolejny"
                };

                db.News.Add(news);
                db.SaveChanges();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new RssContext())
            {
                var query = from x in db.News
                             select x;

                foreach (var item in query)
                {
                    MessageBox.Show(item.Title);
                }

            }
        }
    }
}
