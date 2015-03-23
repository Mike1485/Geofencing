using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Geolocation.Model;
using Windows.Devices.Geolocation.Geofencing;

namespace Geolocation.ViewModel
{
    public class MainPageViewModel
    {
        private Geolocator geo = null;
        public NCLocator AppLocation { get; set; }
        public GeofenceViewModel GeofenceVM { get; set; }

        public MainPageViewModel()
        {
            AppLocation = new NCLocator();
            geo = new Geolocator();
            GeofenceVM = new GeofenceViewModel();

        }

        public async void GetPosition()
        {
            Geoposition pos = await geo.GetGeopositionAsync();
            AppLocation.Latitude = pos.Coordinate.Point.Position.Latitude.ToString();
            AppLocation.Longitude = pos.Coordinate.Point.Position.Longitude.ToString();
            AppLocation.Accuracy = pos.Coordinate.Accuracy.ToString();

        }

        public void RegisterGeofence()
        {
            
            GeofenceVM.Latitude = AppLocation.Latitude;
            GeofenceVM.Longitude = AppLocation.Longitude;

            GeofenceVM.CreateGeofence();


        }

       
    }
}
