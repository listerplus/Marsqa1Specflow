using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marsqa1Specflow.Utilities;
using OpenQA.Selenium;

namespace Marsqa1Specflow.Pages
{
    public class HomePage
    {
        private readonly IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // By Locators
        //public static By BtnSignInBy = By.XPath("//a[@class='item'][(text()='Sign In')]");
        //public static By FieldEmailBy = By.XPath("//input[@name='email']");
        //public static By FieldPasswordBy = By.XPath("//input[@name='password']");
        //public static By BtnLoginBy = By.XPath("//button[@class='fluid ui teal button']");

        // Web Elements
        public IWebElement BtnSignIn => WaitUtil.WaitVisible(driver, By.XPath("//a[@class='item'][(text()='Sign In')]"));
        public IWebElement FieldEmail => driver.FindElement(By.XPath("//input[@name='email']"));
        public IWebElement FieldPassword => driver.FindElement(By.XPath("//input[@name='password']"));
        public IWebElement BtnLogin => driver.FindElement(By.XPath("//button[@class='fluid ui teal button']"));

        // Text
        public readonly string emailValid = "one@test.com";
        public readonly string passwordValid = "Password1.";

        // Methods
        public void ClickSignIn()
        {
            BtnSignIn.Click();
        }

        public void Login()
        {
            ClickSignIn();
            FieldEmail.SendKeys(emailValid);
            FieldPassword.SendKeys(passwordValid);
            BtnLogin.Click();
        }
    }
}
