
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

		private int _latestHumidity;

		private float _latestMax;

		private float _latestMin;

		private string _latestDescription;

		private float _latestWindSpeed;

		private float _latestFeelsLike;

		private DateTime _sunrise;

		private DateTime _sunset;

		private int _dateTime;

		private int _latestPressure;

		private GPSModule _gpsmodule;

		public double LatestTemperature
		{
			get { return _latestTemperature; }
			set { _latestTemperature = value; OnPropertyChanged(); }
		}

		public int LatestHumidity { get { return _latestHumidity; } set { _latestHumidity = value; OnPropertyChanged(); } }

		public float LatestMax { get { return _latestMax; } set { _latestMax = value; OnPropertyChanged(); } }

		public float LatestMin { get { return _latestMin; } set { _latestMin = value; OnPropertyChanged(); } }

		public string LatestDescription { get { return _latestDescription; } set { _latestDescription = value; OnPropertyChanged(); } }

		public float LatestWindSpeed { get { return _latestWindSpeed; } set { _latestWindSpeed = value; OnPropertyChanged(); } }

		public float LatestFeelsLike { get { return _latestFeelsLike; } set { _latestFeelsLike = value; OnPropertyChanged(); } }

		public DateTime Sunrise { get { return _sunrise; } set { _sunrise = value; OnPropertyChanged(); } }

		public DateTime Sunset { get { return _sunset; } set { _sunset = value; OnPropertyChanged(); } }

		public int DateTime { get { return _dateTime; } set { _dateTime = value; OnPropertyChanged(); } }

		public int LatestPressure { get { return _latestPressure; }set { _latestPressure= value; OnPropertyChanged(); } }	


		public MainPage()
		{
			InitializeComponent();
			RefreshCommand = new Command(GetWeatherForecast);
			_httpClient = new HttpClient();
			_httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
			
			GPSModule module = new GPSModule();
			BindingContext = this;

		}

		public ICommand RefreshCommand { get; set; }

		public async void GetWeatherForecast(object parameters)
		{
			

			Location location = await _gpsmodule.GetCurrentLocation();
			double lat = location.Latitude;
			double lon = location.Longitude;

			string appId = "1f7b0a77bf23fecae05e9c7c63d98867";
			string response = await _httpClient.GetStringAsync(new Uri($"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}.99&appid={appId}&units=metric"));

			WeatherInfo responseWeather = JsonConvert.DeserializeObject<WeatherInfo>(response);

			if (responseWeather != null)
			{
				 LatestTemperature=(int)Math.Round(responseWeather.main.temp);
				 LatestWindSpeed=responseWeather.wind.speed;
				 LatestMax= responseWeather.main.temp_max;
				 LatestMin = responseWeather.main.temp_min;
				 LatestHumidity = responseWeather.main.humidity;
				 LatestFeelsLike= responseWeather.main.feels_like;
				 LatestPressure= responseWeather.main.pressure;
				 
				if(responseWeather.weather.Count() >= 0)
				{
					LatestDescription= responseWeather.weather[0].description.ToUpper();
				}

				DateTimeOffset dtOffset = DateTimeOffset.FromUnixTimeSeconds(responseWeather.sys.sunrise);
				Sunrise = dtOffset.UtcDateTime;
				_ = DateTimeOffset.FromUnixTimeSeconds(responseWeather.sys.sunset);
				Sunset = dtOffset.UtcDateTime; 

				
			}
		}
	}
}
		

		
	
