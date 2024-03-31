using System;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Marsqa1Specflow.Utilities
{
    public class DriverSetup
    {
        public static IWebDriver BrowserSetup(string browserType)
        {
            switch (browserType.ToLower())
            {
                case "chrome":
                    return new ChromeDriver();
                case "firefox":
                    return new FirefoxDriver();
                case "edge":
                    return new EdgeDriver();
                default:
                    Console.WriteLine($"Unrecognized browser: {browserType}. Defaulting to Chrome.");
                    return new ChromeDriver();
            }
            //driver.Manage().Window.Maximize();
        }
    }
}
