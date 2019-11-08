using EatToday.Common.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace EatToday.Prism.Views
{
    public partial class MapPage : ContentPage
    {
        private readonly IGeolocatorService _geolocatorService;

        public MapPage(IGeolocatorService geolocatorService)
        {
            InitializeComponent();
            _geolocatorService = geolocatorService;

            MoveMapToCurrentPositionAsync();

        }


        //private void PutPinOnCountry()
        //{


        //    MyMap.Pins.Add(new Pin
        //    {
        //        Position = new Position(convert.ToDouble(null), convert2.ToDouble(null)),
        //        Type = PinType.Place
        //    });
        //}
        private async void MoveMapToCurrentPositionAsync()
        {
            await _geolocatorService.GetLocationAsync();
            if (_geolocatorService.Latitude != 0 && _geolocatorService.Longitude != 0)
            {
                var position = new Position(
                    _geolocatorService.Latitude,
                    _geolocatorService.Longitude);
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                    position,
                    Distance.FromKilometers(.5)));
            }
        }
    }
}
