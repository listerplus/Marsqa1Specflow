﻿using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Xml.Linq;

namespace Marsqa1Specflow.Utilities
{
    public class WaitUtil
    {
        private const int DefaultTimeout = 10;

        public static IWebElement WaitVisible(IWebDriver driver, By by, int timeoutInSeconds = DefaultTimeout)
        {
            // The element variable is inferred to have the type IWebElement
            // The WebDriverWait class returns an IWebElement once the expected condition (in this case, visibility of the element) is met
            var element = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds)).Until(ExpectedConditions.ElementIsVisible(by));
            return element;
        }

        public static void JustWaitVisible(IWebDriver driver, By by, int timeoutInSeconds = DefaultTimeout)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(ExpectedConditions.ElementIsVisible(by));
        }

        public static IWebElement WaitClickable(IWebDriver driver, By by, int timeoutInSeconds = DefaultTimeout)
        {
            var element = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds)).Until(ExpectedConditions.ElementToBeClickable(by));
            return element;
        }

        public static IWebElement WaitExists(IWebDriver driver, By by, int timeoutInSeconds = DefaultTimeout)
        {
            var element = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds)).Until(ExpectedConditions.ElementExists(by));
            return element;
        }

        public static void WaitSelected(IWebDriver driver, By by, int timeoutInSeconds = DefaultTimeout)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(ExpectedConditions.ElementToBeSelected(by));
        }

        // Add more methods based on your needs

        // Example usage:
        // WaitUtil.WaitVisible(driver, By.Id("exampleId"));
    }
}
