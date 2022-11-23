
using Laba3;

namespace Laba3_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db = new ApplicationContext();  
        public MainWindow()
        {
            InitializeComponent();
        }
        
      private void Z1_OnClick(object sender, RoutedEventArgs e)
      {
         var z1 = db.Cars.Where(p => p.Name == "Alfa Romeo").Where(p => p.IsStock == true).ToList();
         Table1.ItemsSource = z1;
      }

      private void Z2_OnClick(object sender, RoutedEventArgs e)
      {
          var z2 = db.Cars.Where(p => p.Name.Contains("BMW")).Select(p => p.Stock).Distinct().ToList();
          Table1.ItemsSource = z2;
      }

      private void Z3_OnClick(object sender, RoutedEventArgs e)
      {
          var z3 = db.Cars.Where(p => p.Cost < 10000).ToList();
          Table1.ItemsSource = z3;
      }

      private void Z4_OnClick(object sender, RoutedEventArgs e)
      {
          var z4 = db.Cars.Where(p => p.Remark != "").OrderBy(p => p.Name).ToList();
          Table1.ItemsSource = z4;

      }

      private void Z5_OnClick(object sender, RoutedEventArgs e)
      {
          var z5 = db.Cars.Where(p => p.DataRelease >= 2000 && p.DataRelease <= 2005).GroupBy(c => c.Stock.Town).Select(g => new { Name = g.Key, Count = g.Count() }).ToList();
          Table1.ItemsSource = z5;
      }

      private void Z6_OnClick(object sender, RoutedEventArgs e)
      {
          var z6 = db.Cars.Where(p => p.DataRelease < 2000).OrderBy(p => p.DataRelease).ToList();
          Table1.ItemsSource = z6;
      }

      private void Z7_OnClick(object sender, RoutedEventArgs e)
      {
          DbReport DBRep = new DbReport() { DateBase = db };
          DBRep.WriteAllReport();
          MessageBox.Show("Файл успешно создан");
      }

      private void CreateBD_OnClick(object sender, RoutedEventArgs e)
      {
          var stocks = new List<Stock>
          {
              new() { Town = "Бийск" },
              new() { Town = "Барнаул" },
              new() { Town = "село Черга" },
              new() { Town = "Санкт-Петербург" }
          };
          var cars = CarGenerator.GetCars(stocks);

          db.Database.EnsureDeleted();
          db.Database.EnsureCreated();

          db.Cars.AddRange(cars);
          db.Stocks.AddRange(stocks);
          db.Cars.AddRange(cars);
          db.SaveChanges();
      }

      private void WriteBD_OnClick(object sender, RoutedEventArgs e)
      {
          var WriteAllDB = db.Cars.Include(p => p.Stock).ToList();
          Table1.ItemsSource = WriteAllDB;
      }
    }
    }