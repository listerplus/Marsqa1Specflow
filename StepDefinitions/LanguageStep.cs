using Marsqa1Specflow.Pages;
using Marsqa1Specflow.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Marsqa1Specflow.StepDefinitions
{
    [Binding]
    public class LanguageStep
    {
        private IWebDriver driver;

        public LanguageStep(IWebDriver driver)
        {
            this.driver = driver;
        }

        [Given(@"User at Profile Page")]
        public void NavigateProfilePage()
        {
            AppConfig config = AppConfig.LoadConfiguration();
            driver.Navigate().GoToUrl(config.Url);
            HomePage homepage = new HomePage(driver);
            ProfilePage profilepage = new ProfilePage(driver);
            if (!profilepage.isAtProfilePage() ) {
                homepage.Login();
            }
            Thread.Sleep(2000);
        }

        [Given(@"User click Language Tab")]
        public void ClickLanguageTab()
        {
            ProfilePage profilepage = new ProfilePage(driver);
            profilepage.ClickLanguageTab();
        }

        [Given(@"There are 3 or less languages added")]
        public void SetThreeOrLessLanguages()
        {
            //TODO
            ProfilePage profilepage = new ProfilePage(driver);
            int numOfRows = profilepage.GetRowCount(1);
            if (numOfRows > 3) { profilepage.DeleteLastRow(1); }
        }

        [Then(@"Choose Language dropdown has four level options")]
        public void VerifyLanguageOptions()
        {
            ProfilePage profilepage = new ProfilePage(driver);
            profilepage.VerifyChooseLanguage();
        }

        [Then(@"User is able to Add Language")]
        public void VerifyAddLanguage()
        {
            // Adding specific language should have been tested manually
            ProfilePage profilepage = new ProfilePage(driver);
            var langTuple = profilepage.AddRandomLanguage();
            bool value = profilepage.IsLanguageOrSkillPresent(1, langTuple.Item1, langTuple.Item2);
            Assert.IsTrue(value);
        }

        [Given(@"Atleast one language present")]
        public void AtleastOneLanguage()
        {
            ProfilePage profilepage = new ProfilePage(driver);
            int numRows = profilepage.GetRowCount(1);
            Console.WriteLine($"Num of Lang rows: {numRows}");
            if (numRows == 0)
            {
                //var rowLangTuple = profilepage.AddRandomLanguage();
                profilepage.AddRandomLanguage();
            }
        }

        [Then(@"User is able to Update Language")]
        public void VerifyUpdateLanguage()
        {
            ProfilePage profilepage = new ProfilePage(driver);
            int numRows = profilepage.GetRowCount(1);

            var langTuple = profilepage.UpdateLastLanguage();
            bool value = profilepage.IsLanguageOrSkillPresent(1, langTuple.Item1, langTuple.Item2, numRows);
            Assert.IsTrue(value);
        }

        [Then(@"User is able to Delete Language")]
        public void VerifyDeleteLanguage()
        {
            ProfilePage profilepage = new ProfilePage(driver);
            int rowCount = profilepage.GetRowCount(1);

            profilepage.DeleteLastRow(1);
            int newRowCount = profilepage.GetRowCount(1);
            Assert.AreEqual(rowCount - 1, newRowCount);
        }
    }
}
