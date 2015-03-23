using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace Geolocation.Model
{
    public class NCLocator : PropertyChangedBase
    {
        private string _latitude;
        private string _longitude;
        private string _accuracy;
        public string Latitude
        {
            get
            {
                return this._latitude;
            }
            set
            {
                this._latitude = value;
                OnPropertyChanged();
            }
        }

        public string Longitude
        {
            get
            {
                return this._longitude;
            }
            set
            {
                this._longitude = value;
                OnPropertyChanged();
            }
        }

        public string Accuracy
        {
            get
            {
                return this._accuracy;
            }
            set
            {
                this._accuracy = value;
                OnPropertyChanged();
            }
        }
    }
}
