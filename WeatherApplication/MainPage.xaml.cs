
using System.Windows.Input;
using Newtonsoft.Json;



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


		public ICommand NewTemperatureCommand { get; set; }

		public MainPage()
		{
			InitializeComponent();
			NewTemperatureCommand = new Command(GetTemperature);
			_httpClient = new HttpClient();
			_httpClient.DefaultRequestHeaders.Add("Accept", "application/json");	
//GetTemperature(_httpClient);


			BindingContext = this;

		}

		public async void GetTemperature(object parameters)
		{
			string response = await _httpClient.GetStringAsync(new Uri("https://api.openweathermap.org/data/2.5/weather?q=London,uk&APPID=1f7b0a77bf23fecae05e9c7c63d98867"));
			WeatherInfo responseTemperature = JsonConvert.DeserializeObject<WeatherInfo>(response);

			if (responseTemperature != null)
			{
				LatestTemperature = responseTemperature.main.temp;
			}
		}

		

		
	}

}
