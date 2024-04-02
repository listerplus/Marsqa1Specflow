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
        public static By IconPencilLastBy = By.XPath("//table[1]/tbody[last()]//i[@class='outline write icon']");
        public static By IconRemoveLastBy = By.XPath("//table[1]/tbody[last()]//i[@class='remove icon']");
        public static By FieldLanguageBy = By.XPath("//input[@placeholder='Add Language']");
        public static By ChooseLanguageBy = By.XPath("//select[@class='ui dropdown']");
        public static By FieldLanguageLastBy = By.XPath("//table[1]/tbody[last()]//input[@placeholder='Add Language']");
        public static By ChooseLanguageLastBy = By.XPath("//table[1]/tbody[last()]//select[@class='ui dropdown']");
        public static By BtnUpdateBy = By.XPath("//input[@value='Update']");
        public static By LanguageOptionsBy = By.XPath("//select[@class='ui dropdown']/option");
        public static By LanguageRowsBy = By.XPath("//div[@data-tab='first']//table/tbody");
        public static By SkillsRowsBy = By.XPath("//div[@data-tab='second']//table/tbody");

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

        public string SelectRandomLangLevel(By by)
        {
            // Make sure Add New Button is clicked and Language Level visible
            // Select any Language Level
            Random random = new Random();
            int index = random.Next(1, LangDrop.Count());
            SelectElement dropDown = new SelectElement(_driver.FindElement(by));
            dropDown.SelectByValue(LangDrop[index]);
            return LangDrop[index];
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

            string langLevel = SelectRandomLangLevel(ChooseLanguageBy);
            _driver.FindElement(BtnAddLanguageBy).Click();
            Thread.Sleep(1000);

            return new Tuple<string, string> (randomString, langLevel);
            // https://www.c-sharpcorner.com/UploadFile/9b86d4/how-to-return-multiple-values-from-a-function-in-C-Sharp/
        }

        public bool IsLanguagePresent(string language, string level, int rowNum = 0)
        {
            bool isPresent = false;
            string getLang, getLevel;
            Console.WriteLine($"[To check] Lang: {language}, Level: {level}");

            if (rowNum == 0)
            {
                for (int i = 1; i < 5; i++)
                {
                    try
                    {
                        getLang = _driver.FindElement(By.XPath($"//div[@data-tab='first']//table/tbody[{i}]/tr/td[1]")).Text;
                        getLevel = _driver.FindElement(By.XPath($"//div[@data-tab='first']//table/tbody[{i}]/tr/td[2]")).Text;
                        //Console.WriteLine($"Lang: {getLang}, Level: {getLevel}");
                        if (language.Equals(getLang) && (level.Equals(getLevel)))
                        {
                            Console.WriteLine($"Checking Language tries: {i}");
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
                getLang = WaitUtil.WaitVisible(_driver, By.XPath($"//div[@data-tab='first']//table/tbody[{rowNum}]/tr/td[1]")).Text;
                getLevel = WaitUtil.WaitVisible(_driver, By.XPath($"//div[@data-tab='first']//table/tbody[{rowNum}]/tr/td[2]")).Text;
                Console.WriteLine($"Retrieved Lang [row {rowNum}]: {getLang}, Level: {getLevel}");
                if (language.Equals(getLang) && (level.Equals(getLevel))) { isPresent = true; }
                getLevel = WaitUtil.WaitVisible(_driver, By.XPath($"//div[@data-tab='first']//table/tbody[{rowNum}]/tr/td[2]")).Text;
            }
            
            return isPresent;
        }

        public void VerifyChooseLanguage()
        {
            ClickAddNew();

            List<string> list = new List<string>();
            IList<IWebElement> dropdownList = _driver.FindElements(LanguageOptionsBy);
            foreach (IWebElement i in dropdownList)
            {
                //dropdownListArray[] = i.Text;
                list.Add(i.Text);
            }
            string[] dropdownListArray = list.ToArray();
            for (int i = 0; i < dropdownList.Count; i++)
            {
                Assert.AreEqual(LangDrop[i], dropdownList[i].Text);
            }
            // int iRowsCount = driver.FindElements(By.XPath("/html/body/..../table/tbody/tr")).Count;
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

            string langLevel = SelectRandomLangLevel(ChooseLanguageLastBy);
            Thread.Sleep(1000);
            _driver.FindElement(BtnUpdateBy).Click();
            Thread.Sleep(1000);
            //VerifyBubble(randomString, "updated");

            return new Tuple<string, string>(randomString, langLevel);
        }

        public void VerifyBubble(string expectedLang, string action ) // action: updated, deleted, added
        {
            IWebElement bubble;
            string bgcolor, popupMsg;

            bubble = WaitUtil.WaitVisible(_driver, By.XPath($"//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']/div"));
            popupMsg = bubble.Text;
            bgcolor = bubble.GetCssValue("color");
            Console.WriteLine($"color: {bgcolor}");
            switch (action)
            {
                case "updated":
                    Assert.AreEqual($"{expectedLang} has been updated to your languages", popupMsg);
                    break;
                case "added":
                    Assert.AreEqual($"{expectedLang} has been added to your languages", popupMsg);
                    break;
                case "deleted":
                    Assert.AreEqual($"{expectedLang} has been deleted from your languages", popupMsg);
                    break;
                default:
                    Assert.Fail();
                    break;
            }
            Assert.AreEqual(popupSuccessColor, bgcolor);
        }

        public void VerifyChooseSkill()
        {
            ClickAddNew();

            List<string> list = new List<string>();
            IList<IWebElement> dropdownList = _driver.FindElements(LanguageOptionsBy);
            foreach (IWebElement i in dropdownList)
            {
                //dropdownListArray[] = i.Text;
                list.Add(i.Text);
            }
            string[] dropdownListArray = list.ToArray();
            for (int i = 0; i < dropdownList.Count; i++)
            {
                Assert.AreEqual(LangDrop[i], dropdownList[i].Text);
            }
            // int iRowsCount = driver.FindElements(By.XPath("/html/body/..../table/tbody/tr")).Count;
        }
    }
}
