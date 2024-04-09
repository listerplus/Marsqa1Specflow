using System;
using System.Reflection.Metadata.Ecma335;
using Marsqa1Specflow.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Marsqa1Specflow.Pages
{
    public class ProfilePage
    {
        private readonly IWebDriver _driver;
        public ProfilePage(IWebDriver driver)
        {
            this._driver = driver;
        }

        // By Locators
        public static By TabProfileBy = By.XPath("//a[@href='/Account/Profile']");
        public static By TabLanguagesBy = By.XPath("//a[@data-tab='first']");
        public static By TabSkillsBy = By.XPath("//a[@data-tab='second']");
        public static By BtnAddNewLanguageBy = By.XPath("//div[@data-tab='first']//table//div[@class='ui teal button '][(text()='Add New')]");
        public static By BtnAddNewSkillBy = By.XPath("//div[@data-tab='second']//table//div[@class='ui teal button'][(text()='Add New')]");
        public static By BtnAddLanguageBy = By.XPath("//div[@data-tab='first']//input[@value='Add']");
        public static By BtnAddSkillBy = By.XPath("//div[@data-tab='second']//input[@value='Add']");
        public static By BtnCancelLanguageBy = By.XPath("//div[@data-tab='first']//input[@value='Cancel']");
        public static By IconRemoveLanguageBy = By.XPath("//div[@data-tab='first']//table/tbody//i[@class='remove icon']");
        public static By IconPencilLastBy = By.XPath("//table[1]/tbody[last()]//i[@class='outline write icon']");
        public static By IconRemoveLastBy = By.XPath("//table[1]/tbody[last()]//i[@class='remove icon']");
        public static By IconPencilLastSkillBy = By.XPath("//div[@data-tab='second']//table/tbody[last()]//i[@class='outline write icon']");
        public static By IconRemoveLastSkillBy = By.XPath("//div[@data-tab='second']//table/tbody[last()]//i[@class='remove icon']");
        public static By FieldLanguageBy = By.XPath("//input[@placeholder='Add Language']");
        public static By ChooseLanguageBy = By.XPath("//select[@class='ui dropdown']");
        public static By FieldLanguageLastBy = By.XPath("//table[1]/tbody[last()]//input[@placeholder='Add Language']");
        public static By ChooseLanguageLastBy = By.XPath("//table[1]/tbody[last()]//select[@class='ui dropdown']");
        public static By FieldSkillBy = By.XPath("//input[@placeholder='Add Skill']");
        public static By ChooseSkillBy = By.XPath("//select[@name='level']");
        public static By BtnUpdateBy = By.XPath("//input[@value='Update']");
        public static By LanguageOptionsBy = By.XPath("//select[@class='ui dropdown']/option");
        public static By SkillOptionsBy = By.XPath("//div[@data-tab='second']//select[@name='level']/option");
        public static By LanguageRowsBy = By.XPath("//div[@data-tab='first']//table/tbody");
        public static By SkillsRowsBy = By.XPath("//div[@data-tab='second']//table/tbody");
        public static By ActiveTabBy = By.XPath("//a[@class='item active']");

        // Texts
        public string[] LangDrop = { "Choose Language Level", "Basic", "Conversational", "Fluent", "Native/Bilingual" };
        public string[] SkillDrop = { "Choose Skill Level", "Beginner", "Intermediate", "Expert" };
        public IDictionary<int, string> tabIndexNames = new Dictionary<int, string>(){
            {1, "first"},
            {2, "second" } };
        public string popupSuccessColor = "rgba(43, 60, 97, 1)";

        // Methods
        public void ClickSkillsTab()
        {
            WaitUtil.WaitVisible(_driver, TabSkillsBy).Click();
        }

        public void ClickLanguageTab()
        {
            _driver.FindElement(TabLanguagesBy).Click();
        }

        public void ClickAddNew()
        {
            _driver.FindElement(BtnAddNewLanguageBy).Click();
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                _driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool isAtProfilePage()
        {
            return IsElementPresent(TabProfileBy);
        }

        public string SelectRandomLevel(By by, string[] dropDownList)
        {
            // Make sure Add New Button is clicked and Language Level visible
            // Select any Language Level
            Random random = new Random();
            int index = random.Next(1, dropDownList.Count());
            SelectElement dropDown = new SelectElement(_driver.FindElement(by));
            dropDown.SelectByValue(dropDownList[index]);
            return dropDownList[index];
        }

        public void AddLanguage(string language, string level)
        {
            // Click Add New button
            _driver.FindElement(BtnAddNewLanguageBy).Click();
            Thread.Sleep(1000);
            WaitUtil.WaitVisible(_driver, FieldLanguageBy).SendKeys(language);

            SelectElement dropDown = new SelectElement(_driver.FindElement(ChooseLanguageBy));
            dropDown.SelectByValue(level);
            _driver.FindElement(BtnAddLanguageBy).Click();
            Thread.Sleep(1000);
        }

        public void Remove(int tabIndex)
        {
            _driver.FindElement(By.XPath($"//div[@data-tab='{tabIndexNames[tabIndex]}']//table/tbody//i[@class='remove icon']")).Click();
            Thread.Sleep(1000);
        }

        public Tuple<string, string> AddRandomLanguage()
        {
            // Click Add New button
            _driver.FindElement(BtnAddNewLanguageBy).Click();
            Thread.Sleep(1000);

            string charSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ";
            Random random = new Random();
            int length = random.Next(3, 21); // 21 is exclusive, so it generates number from 3 to 20
            // Generate the random string
            string randomString = new string(Enumerable.Range(0, length)
                                      .Select(_ => charSet[random.Next(charSet.Length)]).ToArray());
            WaitUtil.WaitVisible(_driver, FieldLanguageBy).SendKeys(randomString);

            string langLevel = SelectRandomLevel(ChooseLanguageBy, LangDrop);
            _driver.FindElement(BtnAddLanguageBy).Click();
            Thread.Sleep(1000);

            return new Tuple<string, string> (randomString, langLevel);
            // https://www.c-sharpcorner.com/UploadFile/9b86d4/how-to-return-multiple-values-from-a-function-in-C-Sharp/
        }

        public Tuple<string, string> AddRandomSkill()
        {
            // Click Add New button
            _driver.FindElement(BtnAddNewSkillBy).Click();
            Thread.Sleep(1000);

            string charSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ";
            Random random = new Random();
            int length = random.Next(3, 21); // 21 is exclusive, so it generates number from 3 to 20
            // Generate the random string
            string randomString = new string(Enumerable.Range(0, length)
                                      .Select(_ => charSet[random.Next(charSet.Length)]).ToArray());
            WaitUtil.WaitVisible(_driver, FieldSkillBy).SendKeys(randomString);

            string skillLevel = SelectRandomLevel(ChooseSkillBy, SkillDrop);
            _driver.FindElement(BtnAddSkillBy).Click();
            Thread.Sleep(1000);

            return new Tuple<string, string>(randomString, skillLevel);
            // https://www.c-sharpcorner.com/UploadFile/9b86d4/how-to-return-multiple-values-from-a-function-in-C-Sharp/
        }

        public bool IsLanguageOrSkillPresent(int tabIndex, string name, string level, int rowNum = 0) // 1: Languages, 2: Skills
        {
            bool isPresent = false;
            string getName, getLevel;
            Console.WriteLine($"[To check] Name: {name}, Level: {level}");

            if (rowNum == 0)
            {
                for (int i = 1; i <= GetRowCount(tabIndex); i++)
                {
                    try
                    {
                        getName = _driver.FindElement(By.XPath($"//div[@data-tab='{tabIndexNames[tabIndex]}']//table/tbody[{i}]/tr/td[1]")).Text;
                        getLevel = _driver.FindElement(By.XPath($"//div[@data-tab='{tabIndexNames[tabIndex]}']//table/tbody[{i}]/tr/td[2]")).Text;
                        //Console.WriteLine($"Lang: {getLang}, Level: {getLevel}");
                        if (name.Equals(getName) && (level.Equals(getLevel)))
                        {
                            Console.WriteLine($"Checking Name tries: {i}");
                            isPresent = true;
                            break;
                        }
                    }
                    catch (NoSuchElementException)
                    {
                        continue;
                    }
                }
            }
            else
            {
                getName = WaitUtil.WaitVisible(_driver, By.XPath($"//div[@data-tab='{tabIndexNames[tabIndex]}']//table/tbody[{rowNum}]/tr/td[1]")).Text;
                getLevel = WaitUtil.WaitVisible(_driver, By.XPath($"//div[@data-tab='{tabIndexNames[tabIndex]}']//table/tbody[{rowNum}]/tr/td[2]")).Text;
                Console.WriteLine($"Retrieved [row {rowNum}]: {getName}, Level: {getLevel}");
                if (name.Equals(getName) && (level.Equals(getLevel))) { isPresent = true; }
            }
            
            return isPresent;
        }

        public void VerifyDropdownOptions(By by, string[] OptionsList)
        {
            List<string> list = new List<string>();
            IList<IWebElement> getDropdownList = _driver.FindElements(by);
            foreach (IWebElement i in getDropdownList)
            {
                //dropdownListArray[] = i.Text;
                list.Add(i.Text);
            }
            string[] dropdownListArray = list.ToArray();
            for (int i = 0; i < getDropdownList.Count; i++)
            {
                Assert.AreEqual(OptionsList[i], getDropdownList[i].Text);
            }
            // int iRowsCount = driver.FindElements(By.XPath("/html/body/..../table/tbody/tr")).Count;
        }

        public void VerifyChooseLanguage()
        {
            ClickAddNew();
            VerifyDropdownOptions(LanguageOptionsBy, LangDrop);
        }

        public void VerifyChooseSkill()
        {
            _driver.FindElement(BtnAddNewSkillBy).Click();
            VerifyDropdownOptions(SkillOptionsBy, SkillDrop);
        }

        public int GetRowCount(int tabIndex)    // 1: Languages, 2: Skills
        {
            int numOfRows = 0;
            switch(tabIndex)
            {
                case 1:
                    //div[@data-tab='first']//table/tbody
                    // decimal numOfRows = selenium.GetXpathCount($"//div[@data-tab='first']//table/tbody");
                    numOfRows = _driver.FindElements(LanguageRowsBy).Count;
                    break;
                case 2:
                    numOfRows = _driver.FindElements(SkillsRowsBy).Count;
                    break;
            }
            return numOfRows;
        }

        public void DeleteLastRow(int tabIndex)    // 1: Languages, 2: Skills
        {
            //Console.WriteLine($"Tab value: {tabIndexNames[tabIndex]}");
            _driver.FindElement(By.XPath($"//div[@data-tab='{tabIndexNames[tabIndex]}']//table/tbody[last()]//i[@class='remove icon']")).Click();
            Thread.Sleep(1000);
        }

        public Tuple<string, string> UpdateLastLanguage()
        {
            _driver.FindElement(IconPencilLastBy).Click();
            Thread.Sleep(1000);

            string charSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ";
            Random random = new Random();
            int length = random.Next(3, 21); // 21 is exclusive, so it generates number from 3 to 20
            // Generate the random string
            string randomString = new string(Enumerable.Range(0, length)
                                      .Select(_ => charSet[random.Next(charSet.Length)]).ToArray());
            WaitUtil.WaitVisible(_driver, FieldLanguageBy).Clear();
            _driver.FindElement(FieldLanguageBy).SendKeys(randomString);

            string langLevel = SelectRandomLevel(ChooseLanguageLastBy, LangDrop);
            Thread.Sleep(1000);
            _driver.FindElement(BtnUpdateBy).Click();
            Thread.Sleep(1000);
            VerifyBubble(randomString, "updated");

            return new Tuple<string, string>(randomString, langLevel);
        }

        public Tuple<string, string> UpdateLastSkill() // TODO, just copy pasted, need to update
        {
            _driver.FindElement(IconPencilLastSkillBy).Click();
            Thread.Sleep(1000);

            string charSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ";
            Random random = new Random();
            int length = random.Next(3, 21); // 21 is exclusive, so it generates number from 3 to 20
            // Generate the random string
            string randomString = new string(Enumerable.Range(0, length)
                                      .Select(_ => charSet[random.Next(charSet.Length)]).ToArray());
            WaitUtil.WaitVisible(_driver, FieldSkillBy).Clear();
            _driver.FindElement(FieldSkillBy).SendKeys(randomString);

            string skillLevel = SelectRandomLevel(ChooseSkillBy, SkillDrop);
            Thread.Sleep(1000);
            _driver.FindElement(BtnUpdateBy).Click();
            Thread.Sleep(1000);
            VerifyBubble(randomString, "updated");

            return new Tuple<string, string>(randomString, skillLevel);
        }

        public void VerifyBubble(string expectedName, string action) // action: updated, deleted, added
        {
            string tab = _driver.FindElement(ActiveTabBy).Text.ToLower(); // languages, skills
            IWebElement bubble;
            string bgcolor, popupMsg;

            bubble = WaitUtil.WaitVisible(_driver, By.XPath($"//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']/div"));
            popupMsg = bubble.Text;
            bgcolor = bubble.GetCssValue("color");
            Console.WriteLine($"color: {bgcolor}");
            switch (action)
            {
                case "updated":
                    Assert.AreEqual($"{expectedName} has been updated to your {tab}", popupMsg);
                    break;
                case "added":
                    Assert.AreEqual($"{expectedName} has been added to your {tab}", popupMsg);
                    break;
                case "deleted":
                    if (tab == "languages") { Assert.AreEqual($"{expectedName} has been deleted from your languages", popupMsg); }
                    else if (tab == "skills") { Assert.AreEqual($"{expectedName} has been deleted", popupMsg); }
                    break;
                default:
                    Assert.Fail();
                    break;
            }
            Assert.AreEqual(popupSuccessColor, bgcolor);
        }

    }
}
