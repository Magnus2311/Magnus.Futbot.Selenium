﻿using Magnus.Futbot.Common;
using Magnus.Futbot.Common.Models;
using Magnus.Futbot.Common.Models.Selenium.Profiles;
using OpenQA.Selenium;

namespace Magnus.Futbot.Services
{
    public class InitProfileService : BaseService
    {
        public static ProfileDTO InitProfile(AddProfileDTO profile)
        {
            var profileDTO = new ProfileDTO()
            {
                Email = profile.Email,
                UserId = profile.UserId,
                Status = ProfileStatusType.CaptchaNeeded
            };

            var driverInstance = GetInstance(profile.Email);
            var driver = driverInstance.Driver;
            driver.Navigate().GoToUrl("https://www.ea.com/fifa/ultimate-team/web-app/");

            IWebElement? loginBtn = driver.FindElement(By.CssSelector("#Login > div > div > button.btn-standard.call-to-action"), 6000);
            loginBtn?.Click();
            Thread.Sleep(5000);

            var emailInput = driver.FindElement(By.CssSelector("#email"), 1000);
            if (emailInput is not null)
            {
                profileDTO.Status = LoginSeleniumService.Login(profile.Email, profile.Password);
            }

            var transferBtn = driver.FindElement(By.CssSelector("body > main > section > nav > button.ut-tab-bar-item.icon-transfer"), 10000);
            if (transferBtn is not null) profileDTO.Status = ProfileStatusType.Logged;

            return profileDTO;
        }
    }
}
