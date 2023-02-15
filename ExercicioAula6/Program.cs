namespace ExercicioAula6_
{
    internal class Program
    {
        public class WeatherService
        {
            private const string WeatherApiUrl = "https://api.openweathermap.org/data/2.5/weather";
            private readonly HttpClient _httpClient;

            public WeatherService()
            {
                _httpClient = new HttpClient();
            }

            public async Task<string> GetWeatherAsync(string city)
            {
                var url = $"{WeatherApiUrl}?q={city}&appid=c7b45911a08172a9b7070a5789754f89";
                var response = await _httpClient.GetAsync(url);
                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
        }
        public class CityListReader
        {
            public async Task<string[]> ReadCityListAsync(string filePath)
            {
                var cityList = await File.ReadAllLinesAsync(filePath);
                return cityList;
            }
        }
        public static async Task Main()
        {
            string cityName = "Sao Luis,BR";
            var weatherService = new WeatherService();
            var cityListReader = new CityListReader();

            var getWeatherTask = Task.Run(() => weatherService.GetWeatherAsync(cityName));
            var readCityListTask = Task.Run(() => cityListReader.ReadCityListAsync("cidades.txt"));

            await Task.WhenAll(getWeatherTask, readCityListTask);

            var weatherInfo = await getWeatherTask;
            var cityList = await readCityListTask;

            Console.WriteLine($"Informações sobre o tempo em : {cityName}");
            Console.WriteLine(weatherInfo);
            Console.WriteLine();

            Console.WriteLine("Lista de cidades:");
            foreach (var city in cityList)
            {
                Console.WriteLine(city);
            }
        }
    }
}