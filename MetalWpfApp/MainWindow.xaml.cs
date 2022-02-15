using EO.WebBrowser.DOM;
using MetalWpfApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace MetalWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MetalauktionEntities _context = new MetalauktionEntities();

        private readonly String[] metaller = new String[] { "Guld", "Sølv", "Platin", "Palladium" };
        private readonly String[] timer = new String[] { "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23" };
        private readonly String[] minutter = new String[] { "00", "30" };



        private ObservableCollection<DisplaySalgsudbud> auktionData = new ObservableCollection<DisplaySalgsudbud>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SalgsudbudGrid.ItemsSource = auktionData;

            MetalComboBox.ItemsSource = metaller;
            TimeComboBox.ItemsSource = timer;
            MinutComboBox.ItemsSource = minutter;



            DispatcherTimer dtimer = new DispatcherTimer();
            dtimer.Interval = TimeSpan.FromSeconds(1);
            dtimer.Tick += new EventHandler((object o, EventArgs arg) =>
            {
                var sqlJoin =
                from s in _context.Salgsudbud.ToList()
                join k in _context.Købstilbud.ToList()
                on s.Id equals k.SalgsudbudId
                into bud
                from id in bud.OrderByDescending(x => x.Pris).Take(1).DefaultIfEmpty()
                select new { s.Gram, s.MetalType, Tidsfrist = s.Tidsfrist, Pris = id?.Pris ?? 0 };

                var fromSql = sqlJoin.ToList();
                auktionData.Clear();

                foreach (var element in fromSql)
                {

                    if (element.Tidsfrist.CompareTo(DateTime.Now) < 0)
                    {
                        var temp = new DisplaySalgsudbud();
                        temp.MetalType = element.MetalType;
                        temp.Gram = element.Gram;
                        temp.Pris = element.Pris;
                        temp.Tidsfrist = "Auktion udløbet";

                        auktionData.Add(temp);
                    }
                    else
                    {
                        var temp = new DisplaySalgsudbud();
                        temp.MetalType = element.MetalType;
                        temp.Gram = element.Gram;
                        temp.Pris = element.Pris;

                        temp.Tidsfrist = element.Tidsfrist.Subtract(DateTime.Now).ToString(@"dd\.hh\:mm\:ss");

                        auktionData.Add(temp);
                    }
                }
            });
            dtimer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MetalComboBox.SelectedItem == null || GramTextBox.Text == "" || UdløbDatePicker.SelectedDate == null
                || MinutComboBox.SelectedItem == null || TimeComboBox.SelectedItem == null)
            {
                ClearCreateAuction();
                return;
            }

            String tempType = MetalComboBox.SelectedItem.ToString();

            int tempGram = 0;

            if (GramTextBox.Text != null)
            {
                tempGram = int.Parse(GramTextBox.Text);
            }

            DateTime tempDate = (DateTime)UdløbDatePicker.SelectedDate;

            tempDate = tempDate.AddMinutes(Double.Parse(MinutComboBox.SelectedItem.ToString()));
            tempDate = tempDate.AddHours(Double.Parse(TimeComboBox.SelectedItem.ToString()));

            // Opretter et nyt Salgsudbud (auktion) objekt
            Salgsudbud temp = new Salgsudbud();
            temp.MetalType = tempType;
            temp.Gram = tempGram;
            temp.Tidsfrist = tempDate;
            temp.Aktiv = true;

            _context.Salgsudbud.Add(temp);
            _context.SaveChanges();

            ClearCreateAuction();

        }
        private void ClearCreateAuction()
        {
            // Ryder felterne til oprettelse af ny auktion
            MetalComboBox.SelectedIndex = -1;
            GramTextBox.Clear();
            UdløbDatePicker.SelectedDate = null;
            MinutComboBox.SelectedIndex = -1;
            TimeComboBox.SelectedIndex = -1;
        }
    }

    public class DisplaySalgsudbud : INotifyPropertyChanged
    {
        int pris;
        String metalType;
        int gram;
        String tidsfrist;

        public int Pris
        {
            get { return this.pris; }
            set
            {
                if (this.pris != value)
                {
                    this.pris = value;
                }
            }
        }

        public String MetalType
        {
            get { return this.metalType; }
            set
            {
                if (this.metalType != value)
                {
                    this.metalType = value;
                    this.NotifyPropertyChanged("metalType");
                }
            }
        }

        public int Gram
        {
            get { return this.gram; }
            set
            {
                if (this.gram != value)
                {
                    this.gram = value;
                    this.NotifyPropertyChanged("gram");
                }
            }
        }

        public String Tidsfrist
        {
            get { return this.tidsfrist; }
            set
            {
                if (this.tidsfrist != value)
                {
                    this.tidsfrist = value;
                    this.NotifyPropertyChanged("tidsfrist");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }


    }

}
