using BoDi;
using Marsqa1Specflow.Utilities;
using OpenQA.Selenium;

namespace Marsqa1Specflow.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _container;
        public Hooks(IObjectContainer container) {
            _container = container;
        }

        [BeforeScenario]
        public void SetUp()
        {
            AppConfig config = AppConfig.LoadConfiguration();
            IWebDriver driver = DriverSetup.BrowserSetup(config.Browser);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(config.Url);
            _container.RegisterInstanceAs<IWebDriver>(driver);  
        }

        [AfterScenario]
        public void TearDown()
        {
            var driver = _container.Resolve<IWebDriver>();
            driver?.Quit(); //null propagation. If != null, then
        }
    }
}
