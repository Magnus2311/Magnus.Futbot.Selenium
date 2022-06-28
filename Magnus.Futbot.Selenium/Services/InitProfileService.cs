using Magnus.Futbot.Common;
using Magnus.Futbot.Common.Models.Responses;
using Magnus.Futbot.Common.Models.Selenium.Profiles;
using OpenQA.Selenium;

namespace Magnus.Futbot.Services
{
    public class InitProfileService : BaseService
    {
        public static Task<InitProfileResponse> InitProfile(AddProfileDTO profile)
        {
            return Task.Run(() =>
            {
                var driverInstance = GetInstance(profile.Email);
                var driver = driverInstance.Driver;
                driver.Navigate().GoToUrl("https://www.ea.com/fifa/ultimate-team/web-app/");

                IWebElement? loginBtn = driver.FindElement(By.CssSelector("#Login > div > div > button.btn-standard.call-to-action"), 6000);
                loginBtn?.Click();
                Thread.Sleep(5000);

                var emailInput = driver.FindElement(By.CssSelector("#email"), 1000);
                if (emailInput is not null)
                {
                    var loginResponse = LoginSeleniumService.Login(profile.Email, profile.Password);
                    return new InitProfileResponse(loginResponse.LoginStatus);
                }

                var transferBtn = driver.FindElement(By.CssSelector("body > main > section > nav > button.ut-tab-bar-item.icon-transfer"), 10000);
                if (transferBtn is not null) return new InitProfileResponse(ProfileStatusType.Logged);

                return new InitProfileResponse(ProfileStatusType.CaptchaNeeded);
            });
        }
    }
}
