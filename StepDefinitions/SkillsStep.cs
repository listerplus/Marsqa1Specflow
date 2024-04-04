using System;
using Marsqa1Specflow.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Marsqa1Specflow.StepDefinitions
{
    [Binding]
    public class SkillsStep
    {
        private IWebDriver driver;

        public SkillsStep(IWebDriver driver)
        {
            this.driver = driver;
        }

        [Given(@"User click Skills Tab")]
        public void ClickSkillsTab()
        {
            ProfilePage profilepage = new ProfilePage(driver);
            profilepage.ClickSkillsTab();
        }

        [Then(@"Choose Skill dropdown has three level options")]
        public void VerifySkillOptions()
        {
            ProfilePage profilepage = new ProfilePage(driver);
            profilepage.VerifyChooseSkill();
        }

        [Then(@"User is able to Add Skill")]
        public void VerifyAddSkill()
        {
            ProfilePage profilepage = new ProfilePage(driver);
            var skillTuple = profilepage.AddRandomSkill();
            bool value = profilepage.IsLanguageOrSkillPresent(2, skillTuple.Item1, skillTuple.Item2);
            Assert.IsTrue(value);
        }

        [Then(@"User is able to Update Skill")]
        public void VerifyUpdateSkill()
        {
            ProfilePage profilepage = new ProfilePage(driver);
            int numRows = profilepage.GetRowCount(2);

            var langTuple = profilepage.UpdateLastSkill();
            bool value = profilepage.IsLanguageOrSkillPresent(2, langTuple.Item1, langTuple.Item2, numRows);
            Assert.IsTrue(value); ;
        }

        [Given(@"Atleast one skill present")]
        public void AtleastOneSkill()
        {
            ProfilePage profilepage = new ProfilePage(driver);
            int numRows = profilepage.GetRowCount(2);
            Console.WriteLine($"Num of Skill rows: {numRows}");
            if (numRows == 0)
            {
                profilepage.AddRandomSkill();
            }
        }

        [Then(@"User is able to Delete Skill")]
        public void VerifyDeleteSkill()
        {
            ProfilePage profilepage = new ProfilePage(driver);
            int rowCount = profilepage.GetRowCount(2);

            profilepage.DeleteLastRow(2);
            int newRowCount = profilepage.GetRowCount(2);
            Assert.AreEqual(rowCount - 1, newRowCount);
        }
    }
}
