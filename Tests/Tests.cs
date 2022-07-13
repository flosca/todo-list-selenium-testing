using System;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Tests;

public class Tests
{
    private const string ApplicationUrl = "https://beatrizsmerino.github.io/vue-todolist/";

    [Fact]
    public void TestGetDoneTasksCount()
    {
        using var driver = new ChromeDriver();
        driver.Navigate().GoToUrl(ApplicationUrl);
        driver.Manage().Window.Maximize();

        var tasksDoneCountChip = driver.FindElement(GetLocatorByString("xpath:(//span[@class='tag_value'])[2]"));
        tasksDoneCountChip.Text.Should().Be("0");
    }

    [Fact]
    public void TestAddNewTodo()
    {
        using var driver = new ChromeDriver();

        driver.Navigate().GoToUrl(ApplicationUrl);
        var tasksCountChip = driver.FindElement(GetLocatorByString("class:tag__value"));
        tasksCountChip.Text.Should().Be("3");

        var newTaskInput = driver.FindElement(GetLocatorByString("class:task-new__input"));
        newTaskInput.SendKeys("do your job!");
        var confirmNewTaskButton =
            driver.FindElement(GetLocatorByString("class:task-new__button-add"));
        confirmNewTaskButton.Click();

        tasksCountChip.Text.Should().Be("4");
    }

    private static By GetLocatorByString(string typeLocator)
    {
        var splitLocator = typeLocator.Split(":", 2);
        var byType = splitLocator[0];
        var locator = splitLocator[1];
        return byType switch
        {
            "xpath" => By.XPath(locator),
            "id" => By.Id(locator),
            "css" => By.CssSelector(locator),
            _ => throw new ArgumentOutOfRangeException(byType, $"Cannot get type of locator: {typeLocator}")
        };
    }
}