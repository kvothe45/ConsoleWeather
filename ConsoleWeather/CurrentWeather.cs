using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace ConsoleWeather
{
    class CurrentWeather
    {
        public void GetCurrentWeather()
        {
            Console.Write("Name a city: ");
            string city = Console.ReadLine();
            string jsonCurrentWeather = AccessWebPage.HttpGet("http://api.openweathermap.org/data/2.5/weather?q=" + city + "&APPID=47992ad1b3261b707350bf13aac83023");
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(OpenWeatherMap.CurrentRoot));
            //WeatherNow weatherNow = new WeatherNow();
            //OpenWeatherMap.Root rootString = DeserializeJson<OpenWeatherMap.Root>(test);

            //defining the objects in this way makes them available outside of the using brackets
            OpenWeatherMap.CurrentRoot root = new OpenWeatherMap.CurrentRoot();

            using (Stream s = GenerateStreamFromString(jsonCurrentWeather))
            {

                root = (OpenWeatherMap.CurrentRoot)ser.ReadObject(s);
            }
            double kelvin = root.main.temp;
            ConvertTempTo convertTempTo = new ConvertTempTo();
            double celsius = convertTempTo.Celsius(kelvin);
            double fahrenheit = convertTempTo.Fahrenheit(celsius);
            Console.WriteLine("{0} is located at lat {1}, lon {2}", city, root.coord.lat, root.coord.lon);
            //round fahrenheit to 2 decimal values for consistent look.  placed in writeline so that follow-on math can be more accurate
            Console.WriteLine("The current temperature is {0} fahrenheit ({1} celsius)", Math.Round(fahrenheit, 2), celsius);

            //defining the objects in this way means the objects are only available inside the using brackets
            //using (Stream s = GenerateStreamFromString(jsonCurrentWeather))
            //{
            //    OpenWeatherMap.CurrentRoot root = (OpenWeatherMap.CurrentRoot)ser.ReadObject(s);
            //    Console.WriteLine("the coordinates are lat {0}, lon {1}", root.coord.lat, root.coord.lon);
            //}

            Console.WriteLine("");
            Console.WriteLine(jsonCurrentWeather);
            Console.ReadLine();
        }

        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }


        //public OpenWeatherMap.Root DeserializeJson<T>(string Json)
        //{
        //    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        //    return javaScriptSerializer.Deserialize<T>(Json);
        //}

        [DataContract]
        internal class WeatherNow
        {

            [DataMember]
            public OpenWeatherMap.Coord coord { get; set; }
            [DataMember]
            public OpenWeatherMap.Sys sys { get; set; }
            [DataMember]
            public List<OpenWeatherMap.Weather> weather { get; set; }
            [DataMember]
            public string @base { get; set; }
            [DataMember]
            public OpenWeatherMap.Main main { get; set; }
            [DataMember]
            public OpenWeatherMap.Wind wind { get; set; }
            [DataMember]
            public Dictionary<string, double> rain { get; set; }
            [DataMember]
            public OpenWeatherMap.Clouds clouds { get; set; }
            [DataMember]
            public int dt { get; set; }
            [DataMember]
            public int id { get; set; }
            [DataMember]
            public string name { get; set; }
            [DataMember]
            public int cod { get; set; }

        }

    }
}
