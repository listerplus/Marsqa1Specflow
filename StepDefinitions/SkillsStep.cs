using System.Reflection.Metadata.Ecma335;
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

        [Given(@"One skill is present")]
        public void OneSkillPresent()
        {
            int numRows = profilepage.GetRowCount(2);
            //Console.WriteLine($"Num of Lang rows: {numRows}");
            profilepage.AddRandomSkill();
        }

        [Then(@"User is able to Delete Skill")]
        public void VerifyDeleteSkill()
        {
            int rowCount = profilepage.GetRowCount(2);

            profilepage.DeleteLastRow(2);
            int newRowCount = profilepage.GetRowCount(2);
            Assert.AreEqual(rowCount - 1, newRowCount);
        }

        [When(@"User adds skill: (.*) (.*)")]
        public void AddSkill(string skill, string level)
        {
            profilepage.AddSkill(skill, level);
        }

        [Then(@"User is able to see skill details: (.*) (.*)")]
        public void VerifySkillPresent(string skill, string level)
        {
            bool value = profilepage.IsLanguageOrSkillPresent(2, skill, level);
            Assert.IsTrue(value);
        }

        [AfterScenario("removeSkillTearDown")]
        public void RemoveSkill()
        {
            profilepage.Remove(2);
        }

        [Then(@"User is able to add (.*) skills")]
        public void AddNumberOfSkills(int num)
        {
            int rowCount = profilepage.GetRowCount(2);
            Console.WriteLine($"Num of rows: {rowCount}");
            for (int i = 1; i <= num; i++)
            {
                var skillTuple = profilepage.AddRandomSkill();
                bool value = profilepage.IsLanguageOrSkillPresent(2, skillTuple.Item1, skillTuple.Item2, rowCount + i);
                Assert.IsTrue(value);
            }

            int newRowCount = profilepage.GetRowCount(2);
            Assert.AreEqual(rowCount + 10, newRowCount);

            for (int i = 1; i <= num; i++)
            {
                profilepage.DeleteLastRow(2);
            }
        }

    }
}
