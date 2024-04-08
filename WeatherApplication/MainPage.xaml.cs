
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Google.Crypto.Tink.Signature;



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

		public double LatestTemperature
		{
			get { return _latestTemperature; }
			set { _latestTemperature = value; OnPropertyChanged(); }
		}

		public int LatestHumidity { get { return _latestHumidity; } set { _latestHumidity = value; OnPropertyChanged(); } }

		public float LatestMax { get { return _latestMax; } set { _latestMax = value; OnPropertyChanged(); } }

		public float LatestMin { get { return _latestMin; } set { _latestMin = value; OnPropertyChanged(); } }

		public string LatestDescription { get { return _latestDescription; } set { _latestDescription = value; OnPropertyChanged(); } }


		public ICommand NewTemperatureCommand { get; set; }
		public ICommand NewHumidityCommand { get; set; }


		public MainPage()
		{
			InitializeComponent();
			NewTemperatureCommand = new Command(GetTemperature);
			NewHumidityCommand = new Command(GetHumidity);

			_httpClient = new HttpClient();
			_httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
			GetMax(_httpClient);
			GetMin(_httpClient);



			BindingContext = this;

		}



		public async void GetTemperature(object parameters)
		{
			string response = await _httpClient.GetStringAsync(new Uri("https://api.openweathermap.org/data/2.5/weather?lat=44.34&lon=10.99&appid=1f7b0a77bf23fecae05e9c7c63d98867&units=metric"));
			WeatherInfo responseTemperature = JsonConvert.DeserializeObject<WeatherInfo>(response);

			if (responseTemperature != null)
			{
				LatestTemperature = responseTemperature.main.temp;
			}
		}

		public async void GetHumidity(object parameters)
		{
			string response = await _httpClient.GetStringAsync(new Uri("https://api.openweathermap.org/data/2.5/weather?lat=44.34&lon=10.99&appid=1f7b0a77bf23fecae05e9c7c63d98867&units=metric"));
			WeatherInfo responseHumidity = JsonConvert.DeserializeObject<WeatherInfo>(response);

			if (responseHumidity != null)
			{
				LatestHumidity = responseHumidity.main.humidity;
			}
		}

		public async void GetMax(object parameters)
		{
			string response = await _httpClient.GetStringAsync(new Uri("https://api.openweathermap.org/data/2.5/weather?lat=44.34&lon=10.99&appid=1f7b0a77bf23fecae05e9c7c63d98867&units=metric"));
			WeatherInfo responseMax = JsonConvert.DeserializeObject<WeatherInfo>(response);

			if (responseMax != null)
			{
				LatestMax = responseMax.main.temp_max;
			}
		}

		public async void GetMin(object parameters)
		{
			string response = await _httpClient.GetStringAsync(new Uri("https://api.openweathermap.org/data/2.5/weather?lat=44.34&lon=10.99&appid=1f7b0a77bf23fecae05e9c7c63d98867&units=metric"));
			WeatherInfo responseMin = JsonConvert.DeserializeObject<WeatherInfo>(response);

			if (responseMin != null)
			{
				LatestMin = responseMin.main.temp_min;
			}
		}

		public async void GetDescription(object parameters)
		{
			string response = await _httpClient.GetStringAsync(new Uri("https://api.openweathermap.org/data/2.5/weather?lat=44.34&lon=10.99&appid=1f7b0a77bf23fecae05e9c7c63d98867&units=metric"));
			WeatherInfo responseDescription = JsonConvert.DeserializeObject<WeatherInfo>(response);
			if (responseDescription != null)
			{
				




			}
		}

	}
}
		

		
	
