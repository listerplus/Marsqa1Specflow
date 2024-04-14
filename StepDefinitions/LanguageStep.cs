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
        HomePage homepage;
        ProfilePage profilepage;

        public LanguageStep(IWebDriver driver)
        {
            this.driver = driver;
            homepage = new HomePage(driver);
            profilepage = new ProfilePage(driver);
        }

        [Given(@"User at Profile Page")]
        public void NavigateProfilePage()
        {
            if (!profilepage.isAtProfilePage() ) {
                homepage.Login();
            }
            Thread.Sleep(2000);
        }

        [Given(@"User click Language Tab")]
        public void ClickLanguageTab()
        {
            profilepage.ClickLanguageTab();
        }

        [Given(@"There are 3 or less languages added")]
        public void SetThreeOrLessLanguages()
        {
            int numOfRows = profilepage.GetRowCount(1);
            if (numOfRows > 3) { profilepage.DeleteLastRow(1); }
        }

        [When(@"Add New button is pressed")]
        public void ClickAddNewButton()
        {
            int numOfRows = profilepage.GetRowCount(1);
            if (numOfRows > 3) { profilepage.DeleteLastRow(1); }
            profilepage.ClickAddNew();
        }


        [Then(@"Choose Language dropdown has four level options")]
        public void VerifyLanguageOptions()
        {
            profilepage.VerifyChooseLanguage();
        }

        [Then(@"User is able to Add Language")]
        public void VerifyAddLanguage()
        {
            // Adding specific language should have been tested manually
            var langTuple = profilepage.AddRandomLanguage();
            bool value = profilepage.IsLanguageOrSkillPresent(1, langTuple.Item1, langTuple.Item2);
            Assert.IsTrue(value);
        }

        [Given(@"Atleast one language present")]
        public void AtleastOneLanguage()
        {
            int numRows = profilepage.GetRowCount(1);
            Console.WriteLine($"Num of Lang rows: {numRows}");
            if (numRows == 0)
            {
                //var rowLangTuple = profilepage.AddRandomLanguage();
                profilepage.AddRandomLanguage();
            }
        }

        [Given(@"One language is present")]
        public void OneLanguagePresent()
        {
            int numRows = profilepage.GetRowCount(1);
            //Console.WriteLine($"Num of Lang rows: {numRows}");
            if (numRows < 4)
            {
                profilepage.AddRandomLanguage();
            }
        }

        [Then(@"User is able to Update Language")]
        public void VerifyUpdateLanguage()
        {
            int numRows = profilepage.GetRowCount(1);

            var langTuple = profilepage.UpdateLastLanguage();
            bool value = profilepage.IsLanguageOrSkillPresent(1, langTuple.Item1, langTuple.Item2, numRows);
            Assert.IsTrue(value);
        }

        [Then(@"User is able to Delete Language")]
        public void VerifyDeleteLanguage()
        {
            int rowCount = profilepage.GetRowCount(1);

            profilepage.DeleteLastRow(1);
            int newRowCount = profilepage.GetRowCount(1);
            Assert.AreEqual(rowCount - 1, newRowCount);
        }

        [When(@"There are four languages")]
        public void WhenThereAreFourLanguages()
        {
            int numOfRows = profilepage.GetRowCount(1);
            while (numOfRows < 4) 
            { 
                profilepage.AddRandomLanguage();
                numOfRows += 1;
            }
        }

        [Then(@"User is unable to add more language")]
        public void VerifyAddNewNotPresent()
        {
            Assert.False(profilepage.isBtnAddNewPresent());

            // Remove added languages
            profilepage.Remove(1);
            profilepage.Remove(1);
            profilepage.Remove(1);
            profilepage.Remove(1);
        }

        [When(@"User adds language: (.*) (.*)")]
        public void AddLanguage(string language, string level)
        {
            profilepage.AddLanguage(language, level);
        }

        [Then(@"User is able to see Language details: (.*) (.*)")]
        public void VerifyLanguagePresent(string language, string level)
        {
            bool value = profilepage.IsLanguageOrSkillPresent(1, language, level);
            Assert.IsTrue(value); 
        }

        [AfterScenario("removeLanguageTearDown")]
        public void RemoveLanguage()
        {
            profilepage.Remove(1);
        }

    }
}
