using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using LocatAI.NET;
using LocatAI.NET.Configuration;
using LocatAI.NET.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
namespace LocatAI_Demo
{
    public class SaucedemoTests
    {
        private IWebDriver? _driver;

        [SetUp]
        public void Setup()
        {
            // Selenium Manager will automatically handle the driver download and setup
            // No need to specify options unless customization is required
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        }

        [Test]
        public async Task TestSauceDemoLoginAndInventory()
        {
            Assert.That(_driver, Is.Not.Null);
            _driver!.Navigate().GoToUrl("https://www.saucedemo.com/");

                var usernameField = await _driver.FindElementByAIAsync("Username input field");
                var passwordField = await _driver.FindElementByAIAsync("Password input field");
                var loginButton = await _driver.FindElementByAIAsync("Login button");

                usernameField.SendKeys("standard_user");
                passwordField.SendKeys("secret_sauce");
                loginButton.Click();
                TestContext.WriteLine("Logged in successfully.");

                var inventoryContainer = await _driver.FindElementByAIAsync(
                    "The container for inventory items",
                    timeoutSeconds: 15);
                Assert.That(inventoryContainer.Displayed, Is.True,
                    "Inventory container should be displayed after login.");
                TestContext.WriteLine("Verified login by finding inventory container.");

                var inventoryItems = await _driver.FindElementsByAIAsync(
                    "All inventory item cards or divs");
                Assert.That(inventoryItems, Is.Not.Empty,
                    "Should find multiple inventory items.");
                TestContext.WriteLine($"Found {inventoryItems.Count} inventory items.");

                TestContext.WriteLine("Running find again to test cache...");
                var inventoryItemsAgain = await _driver.FindElementsByAIAsync(
                    "All inventory item cards or divs");
                Assert.That(inventoryItemsAgain.Count, Is.EqualTo(inventoryItems.Count),
                    "Should find same number of items from cache.");
                TestContext.WriteLine(
                    $"Found {inventoryItemsAgain.Count} inventory items again (from cache).");
        }

        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
            {
                try
                {
                    _driver.PrintAIUsageReport();
                    _driver.DisposeLocatAI();
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine($"Error during cleanup: {ex.Message}");
                }

                _driver.Quit();
                _driver.Dispose();
                _driver = null;
            }
        }
    }
}
