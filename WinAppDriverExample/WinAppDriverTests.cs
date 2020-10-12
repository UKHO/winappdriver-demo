using System;
using System.Buffers.Text;
using System.IO;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.ImageComparison;
using OpenQA.Selenium.Appium.Windows;

namespace WinAppDriverExample
{
    [TestFixture]
    public class WinAppDriverTests
    {
        private WindowsDriver<WindowsElement> _winDriver;
        private IConfigurationRoot _config;

        [OneTimeSetUp]
        public void SetUpAppConfig()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        [SetUp]
        public void SetUpAppiumAndWinAppDriver()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("platformName", "Windows");
            options.AddAdditionalCapability("platformVersion", "10");
            options.AddAdditionalCapability("deviceName", "WindowsPC");

            //If you already have the process open then use the below:
            //    options.AddAdditionalCapability("appTopLevelWindow", topLevelWindowHandle);
            //Otherwise use this to launch the app:
            options.AddAdditionalCapability("app", _config["PathToApplicationUnderTest"]);

             _winDriver =  new WindowsDriver<WindowsElement>(new Uri(_config["WinAppDriverUrl"]), options);
        }

        [TearDown]
        public void TearDown()
        {
            _winDriver?.Quit();
        }

        [Test]
        public void When_the_button_is_pressed_the_input_should_be_put_into_the_output()
        {
            //use FindElementByAccessibilityId for things tagged with AutomationProperties.AutomationId
            var inputBox = _winDriver.FindElementByAccessibilityId("Input");
            inputBox.SendKeys("stuff");

            //you can use class name to find an element but try to avoid this when possible
            var button = _winDriver.FindElementByClassName("Button");
            Assert.That(button.Text, Is.EqualTo("Press me"), "Button text is incorrect");
            button.Click();

            //you can also use FindElementByAccessibilityId for x:Name.
            //if AutomationProperties.AutomationId is also on the element then FindElementByAccessibilityId will expect that value instead
            var result = _winDriver.FindElementByAccessibilityId("ResultWindow");
            Assert.That(result.Text, Is.EqualTo("Input was stuff"), "Result text is incorrect");
        }

        [Test]
        public void Startup_screen_ui_matches_expected_screenshot()
        {
            var expected = Convert.ToBase64String(File.ReadAllBytes(Path.Combine(GetBaseLineScreenshots.BaselineScreenshotDirectory, "StartupScreen.jpg")));

            var actual = Convert.ToBase64String(_winDriver.GetScreenshot().AsByteArray);

            var similarityMatchingOptions = new SimilarityMatchingOptions() {Visualize = true};
            SimilarityMatchingResult result = _winDriver.GetImagesSimilarity(expected, actual,
                similarityMatchingOptions);

            Assert.That(result.Score, Is.GreaterThan(0));
        }
    }
}