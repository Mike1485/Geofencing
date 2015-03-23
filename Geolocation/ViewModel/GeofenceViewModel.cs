
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Windows.Devices.Enumeration;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Geolocation;
using ViewModelBase;

namespace Geolocation.ViewModel
{
    public class GeofenceViewModel : PropertyChangedBase
    {
        private readonly CoreDispatcher dispatcher;
        private string fencekey = "Newton";

        private double radius = 75.0;
        private bool singleUse = false;
        private TimeSpan dwellTime = new TimeSpan(0, 0, 10);
        private TimeSpan duration = new TimeSpan(0, 3, 0);
        //private DateTimeOffset StartTime;

        public string Latitude { get; set; }
        public string Longitude { get; set; }
        private string appfencestate;
        public string AppFenceState
        {
            get
            {
                return this.appfencestate;
            }
            set
            {
                this.appfencestate = value;
                OnPropertyChanged();
            }
        }

        public GeofenceViewModel()
        {

            AppFenceState = "Not created";
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                return;
            }
            this.dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;


            GeofenceMonitor.Current.GeofenceStateChanged += OnGeofenceStateChanged;
        }

        public void CreateGeofence()
        {


            if (GeofenceMonitor.Current.Geofences.Count != 0)
            {
                GeofenceMonitor.Current.Geofences.Clear();
            }

            Geofence geofence = null;

            BasicGeoposition position;
            position.Latitude = Double.Parse(Latitude);
            position.Longitude = Double.Parse(Longitude);
            position.Altitude = 0.0;

            DateTimeOffset StartTime = new DateTimeOffset(DateTime.Now);
            // the geofence is a circular region
            Geocircle geocircle = new Geocircle(position, radius);


            // want to listen for enter geofence, exit geofence and remove geofence events
            // you can select a subset of these event states
            MonitoredGeofenceStates mask = 0;

            mask |= MonitoredGeofenceStates.Entered;
            mask |= MonitoredGeofenceStates.Exited;
            mask |= MonitoredGeofenceStates.Removed;

            geofence = new Geofence(fencekey, geocircle, mask, singleUse, dwellTime, StartTime, duration);

            GeofenceMonitor.Current.Geofences.Add(geofence);

            AppFenceState = "Fence created";

        }

        public async void OnGeofenceStateChanged(GeofenceMonitor sender, object e)
        {
            var reports = sender.ReadReports();

            await this.dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                foreach (GeofenceStateChangeReport report in reports)
                {
                    GeofenceState state = report.NewState;

                    Geofence geofence = report.Geofence;

                    if (state == GeofenceState.Removed)
                    {
                        // remove the geofence from the geofences collection
                        GeofenceMonitor.Current.Geofences.Remove(geofence);
                        AppFenceState = "Removed";
                    }
                    else if (state == GeofenceState.Entered)
                    {
                        AppFenceState = "Entered";

                    }
                    else if (state == GeofenceState.Exited)
                    {
                        AppFenceState = "Exited";

                    }
                }
            });
        }

    }
}
