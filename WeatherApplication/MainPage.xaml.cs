
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Newtonsoft.Json;
using System.Linq;



namespace WeatherApplication
{
	public partial class MainPage : ContentPage
	{

		private HttpClient _httpClient;

		private double _latestTemperature;
		public double LatestTemperature
		{
			get { return _latestTemperature; }
			set { _latestTemperature = value; OnPropertyChanged(); }
		}

		private int _latestHumidity;

		public int LatestHumidity { get { return _latestHumidity; } set { _latestHumidity = value; OnPropertyChanged(); } }

		private float _latestMax;

		public float LatestMax { get { return _latestMax; } set { _latestMax = value; OnPropertyChanged(); } }

		private float _latestMin;

		public float LatestMin { get { return _latestMin; } set { _latestMin = value; OnPropertyChanged(); } }


		private string _latestDescription;

		public string LatestDescription { get { return _latestDescription; } set { _latestDescription = value; OnPropertyChanged(); } }


		private float _latestWindSpeed;

		public float LatestWindSpeed { get { return _latestWindSpeed; } set { _latestWindSpeed = value; OnPropertyChanged(); } }

		private float _latestFeelsLike;

		public float LatestFeelsLike { get { return _latestFeelsLike; } set { _latestFeelsLike = value; OnPropertyChanged(); } }

		private int _latestPressure;

		public int LatestPressure { get { return _latestPressure; } set { _latestPressure = value; OnPropertyChanged(); } }

		private int _clouds;

		public int CloudCoverage { get { return _clouds; } set { _clouds = value; OnPropertyChanged(); } }

		private string _country;

		public string Country { get { return _country; } set { _country = value; OnPropertyChanged(); } }


		private string _sunrise;

		public string Sunrise { get { return _sunrise; } set { _sunrise = value; OnPropertyChanged(); } }

		private string _sunset;

		public string Sunset { get { return _sunset; } set { _sunset = value; OnPropertyChanged(); } }

		private string _dateUpdate;

		public string DateUpdate { get { return _dateUpdate; } set { _dateUpdate = value; OnPropertyChanged(); } }

		private GPSModule _gpsmodule;

		public MainPage()
		{
			 InitializeComponent();
			 Refresh = new Command(GetWeatherForecast);
			_httpClient = new HttpClient();
			_httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
			_gpsmodule = new GPSModule();
			BindingContext = this;

		}

		public ICommand Refresh { get; set; }
		

		public async void GetWeatherForecast(object parameters)
		{

			await DisplayAlert("Notice", "Getting latest forecast", "Okay");

				Location location = await _gpsmodule.GetCurrentLocation();
				double lat = location.Latitude;
				double lon = location.Longitude;

				string appId = "1f7b0a77bf23fecae05e9c7c63d98867";
				string response = await _httpClient.GetStringAsync(new Uri($"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid=1f7b0a77bf23fecae05e9c7c63d98867&units=metric"));

				WeatherInfo responseWeather = JsonConvert.DeserializeObject<WeatherInfo>(response);

				if (responseWeather != null)
				{
					LatestTemperature = (int)Math.Round(responseWeather.main.temp);
					LatestWindSpeed = responseWeather.wind.speed;
					LatestMax = responseWeather.main.temp_max;
					LatestMin = responseWeather.main.temp_min;
					LatestHumidity = responseWeather.main.humidity;
					LatestFeelsLike = responseWeather.main.feels_like;
					LatestPressure = responseWeather.main.pressure;
				    Country=responseWeather.sys.country;
				    CloudCoverage = responseWeather.clouds.all;
				    DateTimeOffset dtOffset= DateTimeOffset.FromUnixTimeSeconds(responseWeather.sys.sunrise);
				    Sunrise = dtOffset.UtcDateTime.ToString();
				    
				    dtOffset = DateTimeOffset.FromUnixTimeSeconds(responseWeather.sys.sunset);
				    Sunset = dtOffset.UtcDateTime.ToString();

				if (responseWeather.weather.Count() >= 0)
					{
						LatestDescription = responseWeather.weather[0].description.ToUpper();
					}

					


				}

				
			
			
		}


	}
}
		

		
	
