using Marsqa1Specflow.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Marsqa1Specflow.StepDefinitions
{
    [Binding]
    public class SkillsStep
    {
        private IWebDriver driver;
        ProfilePage profilepage;

        public SkillsStep(IWebDriver driver)
        {
            this.driver = driver;
            profilepage = new ProfilePage(driver);
        }

        [Given(@"User click Skills Tab")]
        public void ClickSkillsTab()
        {
            profilepage.ClickSkillsTab();
        }

        [Then(@"Choose Skill dropdown has three level options")]
        public void VerifySkillOptions()
        {
            profilepage.VerifyChooseSkill();
        }

        [Then(@"User is able to Add Skill")]
        public void VerifyAddSkill()
        {
            var skillTuple = profilepage.AddRandomSkill();
            bool value = profilepage.IsLanguageOrSkillPresent(2, skillTuple.Item1, skillTuple.Item2);
            Assert.IsTrue(value);
        }

        [Then(@"User is able to Update Skill")]
        public void VerifyUpdateSkill()
        {
            int numRows = profilepage.GetRowCount(2);

            var langTuple = profilepage.UpdateLastSkill();
            bool value = profilepage.IsLanguageOrSkillPresent(2, langTuple.Item1, langTuple.Item2, numRows);
            Assert.IsTrue(value); ;
        }

        [Given(@"Atleast one skill present")]
        public void AtleastOneSkill()
        {
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
            int rowCount = profilepage.GetRowCount(2);

            profilepage.DeleteLastRow(2);
            int newRowCount = profilepage.GetRowCount(2);
            Assert.AreEqual(rowCount - 1, newRowCount);
        }
    }
}
