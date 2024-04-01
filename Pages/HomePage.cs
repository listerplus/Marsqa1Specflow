﻿using System;
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
        private readonly IWebDriver _driver;

        public HomePage(IWebDriver driver)
        {
            this._driver = driver;
        }

        // By Locators
        public static By BtnSignInBy = By.XPath("//a[@class='item'][(text()='Sign In')]");
        public static By FieldEmailBy = By.XPath("//input[@name='email']");
        public static By FieldPasswordBy = By.XPath("//input[@name='password']");
        public static By BtnLoginBy = By.XPath("//button[@class='fluid ui teal button']");

        // Text
        public readonly string emailValid = "one@test.com";
        public readonly string passwordValid = "Password1.";

        // Methods
        public void ClickSignIn()
        {
            WaitUtil.WaitVisible(_driver, BtnSignInBy).Click();
        }

        public void Login()
        {
            ClickSignIn();
            _driver.FindElement(FieldEmailBy).SendKeys(emailValid);
            _driver.FindElement(FieldPasswordBy).SendKeys(passwordValid);
            _driver.FindElement(BtnLoginBy).Click();
        }
    }
}