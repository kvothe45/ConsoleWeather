﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWeather
{
    class Program
    {
        static void Main(string[] args)
        {
            CurrentWeather weatherNow = new CurrentWeather();
            weatherNow.GetCurrentWeather();
        }

    }
}
