using ppedv.Weihnachtsbaeckerei.Data.EF;
using ppedv.Weihnachtsbaeckerei.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ppedv.Weihnachtsbaeckerei.UI.WPF.ViewModels
{

    public class CookieViewModel : ViewModelBase
    {
        public ObservableCollection<Cookie> CookieList { get; set; }

        public IEnumerable<Form> Formen { get { return Enum.GetValues(typeof(Form)).Cast<Form>(); } }

        public IEnumerable<Glasur> Glasuren { get; private set; }

        public IEnumerable<Zutat> Zutaten { get; private set; }

        public SaveCommand SaveCommand { get; set; }

        public ICommand SaveCommand2 { get; set; }
        public ICommand NewCommand { get; set; }


        Cookie selectedCookie;

        public Cookie SelectedCookie
        {
            get
            {
                return selectedCookie;
            }

            set
            {
                selectedCookie = value;
                OnPropertyChanged("");//alle 
                //OnPropertyChanged("SelectedCookie");
                //OnPropertyChanged("KJ");
                //OnPropertyChanged("KCal");
            }
        }

        public int KCal
        {
            get
            {
                if (selectedCookie == null)
                    return -1;
                return selectedCookie.KCal;
            }
            set
            {
                if (selectedCookie != null)
                    selectedCookie.KCal = value;


                OnPropertyChanged(nameof(Kilojoule));

            }
        }

        public int Kilojoule
        {
            get
            {
                if (SelectedCookie == null)
                    return -1;
                return SelectedCookie.KCal * 4;
            }
        }

        EfContext context = new EfContext();


        public CookieViewModel()
        {
            CookieList = new ObservableCollection<Cookie>(context.Cookies.ToList());
            Glasuren = new List<Glasur>(context.Glasuren.OrderBy(x => x.Name).ToList());
            Zutaten = new List<Zutat>(context.Zutaten.OrderBy(x => x.Name).ToList());

            SaveCommand = new SaveCommand(context);
        
            SaveCommand2 = new RelayCommand(UserWantsToSave);
            SaveCommand2 = new RelayCommand(obj => context.SaveChanges(), o => context.ChangeTracker.HasChanges());
            NewCommand = new RelayCommand(UserWantsToCreateCookie);
        }

        private void UserWantsToCreateCookie(object obj)
        {
            var neu = new Cookie() { Name = "NEU", Herstellung = DateTime.Now, Form = Form.Stern };
            context.Cookies.Add(neu);
            CookieList.Add(neu);
            SelectedCookie = neu;
        }

        private void UserWantsToSave(object obj)
        {
            context.SaveChanges();
        }
    }
}
