using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace WinAppDriverExample
{
    [Explicit("These tests should only ever be run manually as needed in order to produce baseline screenshots")]
    [TestFixture]
    internal class GetBaseLineScreenshots
    {
        private IConfigurationRoot _config;
        private WindowsDriver<WindowsElement> _winDriver;

        [OneTimeSetUp]
        public void SetUpAppConfig()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public static string BaselineScreenshotDirectory
        {
            get
            {
                var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
                while (true)
                {
                    if (directory.GetDirectories("BaselineScreenshots").Any())
                        return directory.GetDirectories("BaselineScreenshots").Single().FullName;

                    if (directory.Parent == null)
                        return null;

                    directory = directory.Parent;
                }
            }
        }

        [SetUp]
        public void SetUpAppiumAndWinAppDriver()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            options.AddAdditionalCapability("app", _config["PathToApplicationUnderTest"]);

            _winDriver = new WindowsDriver<WindowsElement>(new Uri(_config["WinAppDriverUrl"]), options);
        }

        [TearDown]
        public void TearDown()
        {
            _winDriver?.Quit();
        }

        [Test]
        public void GetStartupScreenBaselineScreenshot()
        {
            File.WriteAllBytes(Path.Combine(BaselineScreenshotDirectory, "StartupScreen.jpg"),
                _winDriver.GetScreenshot().AsByteArray);
        }
    }
}