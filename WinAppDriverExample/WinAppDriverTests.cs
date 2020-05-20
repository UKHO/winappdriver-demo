using System;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace WinAppDriverExample
{
    public class WinAppDriverTests
    {
        private WindowsDriver<WindowsElement> _winDriver;

        [SetUp]
        public void SetUp()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("deviceName", "WindowsPC");

            //If you already have the process open then use the below:
            //    options.AddAdditionalCapability("appTopLevelWindow", topLevelWindowHandle);
            //Otherwise use this to launch the app:
            options.AddAdditionalCapability("app", GetPathToApplicationUnderTest());

             _winDriver =  new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options);
        }

        [TearDown]
        public void TearDown()
        {
            _winDriver.Quit();
        }

        [Test]
        public void When_the_button_is_pressed_the_input_should_be_put_into_the_output()
        {
            //use FindElementByAccessibilityId for things tagged with AutomationProperties.AutomationId
            var inputBox = _winDriver.FindElementByAccessibilityId("Input");
            inputBox.SendKeys("stuff");

            //you can use class name to find an element but try to avoid this when possible
            var button = _winDriver.FindElementByClassName("Button");
            button.Click();

            //you can also use FindElementByAccessibilityId for x:Name.
            //if AutomationProperties.AutomationId is also on the element then FindElementByAccessibilityId will expect that value instead
            var result = _winDriver.FindElementByAccessibilityId("ResultWindow");
            Assert.That(result.Text, Is.EqualTo("Input was stuff"));
        }

        private static string GetPathToApplicationUnderTest()
        {
            return @"C:\Dev\WinAppDriverExample\TestApp\bin\Debug\netcoreapp3.1\TestApp.exe";
        }
    }
}