using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
        }

        private string title = string.Empty;
        public const string TitlePropertyName = "Title";

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value, TitlePropertyName); }
        }


        private bool isBusy;

        public const string IsBusyPropertyName = "IsBusy";
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value, IsBusyPropertyName); }
        }

        private string _mainImage = "MainImage.jpg";
        public string MainImage
        {
            get { return _mainImage; }
            set
            {
                _mainImage = value;
                OnPropertyChanged("MainImage");
            }
        }

        private string _ContactsLogo = "Contacts.png";
        public string ContactsLogo
        {
            get { return _ContactsLogo; }
            set
            {
                _ContactsLogo = value;
                OnPropertyChanged("ContactsLogo");
            }
        }

        protected void SetProperty<T>(
            ref T backingStore, T value,
            string propertyName,
            Action onChanged = null)
        {


            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            backingStore = value;

            if (onChanged != null)
                onChanged();

            OnPropertyChanged(propertyName);
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
