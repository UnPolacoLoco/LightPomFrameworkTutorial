﻿using AutomationResources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElementInteractions
{
    [TestClass]
    [TestCategory("Locating web elements")]
    public class IdentifyingWebElements
    {
        public IWebDriver Driver { get; private set; }
        [TestInitialize]
        public void SetupBeforeEveryTestMethod()
        {
            Driver = new WebDriverFactory().Create(BrowserType.Chrome);
        }
        [TestMethod]
        public void DifferentTypesOfSeleniumLocationStrategies()
        {
            Driver.Navigate().GoToUrl("https://www.ultimateqa.com/simple-html-elements-for-automation/");
            HighlightElementUsingJavaScript(By.ClassName("buttonClass"));
            HighlightElementUsingJavaScript(By.Id("idExample"));
            HighlightElementUsingJavaScript(By.LinkText("Click me using this link text!"));
            HighlightElementUsingJavaScript(By.Name("button1"));
            HighlightElementUsingJavaScript(By.PartialLinkText("link text!"));
            HighlightElementUsingJavaScript(By.TagName("div"));
            HighlightElementUsingJavaScript(By.CssSelector("#idExample"));
            HighlightElementUsingJavaScript(By.CssSelector(".buttonClass"));
            HighlightElementUsingJavaScript(By.XPath("//*[@id='idExample']"));
            HighlightElementUsingJavaScript(By.XPath("//*[@class='buttonClass']"));
        }

        /*
         Highlight this link using all of the different location strategies
             */
        [TestMethod]
        public void SeleniumLocationStrategiesQuiz()
        {
            Driver.Navigate().GoToUrl("https://www.ultimateqa.com/simple-html-elements-for-automation/");
            HighlightElementUsingJavaScript(By.Id("simpleElementsLink"));
            HighlightElementUsingJavaScript(By.LinkText("Click this link"));
            HighlightElementUsingJavaScript(By.Name("clickableLink"));
            HighlightElementUsingJavaScript(By.CssSelector("#simpleElementsLink"));


        }



        private void HighlightElementUsingJavaScript(By locationStrategy, int duration = 2)
        {
            var element = Driver.FindElement(locationStrategy);
            var originalStyle = element.GetAttribute("style");
            IJavaScriptExecutor JavaScriptExecutor = Driver as IJavaScriptExecutor;
            JavaScriptExecutor.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2])",
                element,
                "style",
                "border: 7px solid yellow; border-style: dashed;");

            if (duration <= 0) return;
            Thread.Sleep(TimeSpan.FromSeconds(duration));
            JavaScriptExecutor.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2])",
                element,
                "style",
                originalStyle);
        }
        [TestCleanup]
        public void CleanupAfterEveryTestMethod()
        {
            Driver.Quit();
        }

        [TestMethod]
        public void SeleniumElementLocationExam()
        {

            /*
             *-Using only XPath!!
             -When debugging and testing, make sure that you scroll the element into view, Selenium
             will not scroll for you. Not yet...
             */



            Driver.Navigate().GoToUrl("https://www.ultimateqa.com/simple-html-elements-for-automation/");
            
            
            //click any radio button, hint:  FindElement().Click();
            var radioFemale = Driver.FindElement(By.XPath("//*[@type = 'radio'][@value='female']"));
            radioFemale.Click();

            //select one checkbox
            var checkBoxBike = Driver.FindElement(By.XPath("//*[@type = 'checkbox'][@value = 'Bike']"));
            checkBoxBike.Click();

            //select Audi from the dropdown
            var optionAudi = Driver.FindElement(By.XPath("//select/option[@value = 'audi']"));
            optionAudi.Click();

            //open Tab2 and assert that it is opened. Hint, use .Text property when you find the element
            var tab2 = Driver.FindElement(By.XPath("//a[contains(text(), 'Tab 2')]"));
            tab2.Click();

            Assert.AreEqual("Tab 2 content", Driver.FindElement(
                By.XPath("//*[@class = 'et_pb_tab et_pb_tab_1 clearfix et-pb-active-slide']/div")).Text);


            //in the HTML Table with id, highlight one of the salary cells
            var salaryCell = Driver.FindElement(By.XPath("//*[@id = 'htmlTableId']//tr[2]//td[3]"));
            HighlightElementUsingJavaScript(By.XPath("//*[@id = 'htmlTableId']//tr[2]//td[3]"));


            //Highlight the center section called "Highlight me", but you can only
            //highlight the highest level div for that element. The top parent div.
            //Hint, this is the class - 
            //et_pb_column et_pb_column_1_3  et_pb_column_10 et_pb_css_mix_blend_mode_passthrough

            HighlightElementUsingJavaScript(By.XPath("//*[@class = 'et_pb_column et_pb_column_1_3 et_pb_column_10    et_pb_css_mix_blend_mode_passthrough']"));
          
        }
    }
}
