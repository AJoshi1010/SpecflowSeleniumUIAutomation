# TFL Web Automation - Ankit Joshi
UI Automation framework for TFL using SpecFlow, NUnit 3 and Selenium Chrome Web-Driver

## Synopsis

The project is for automating the UI testing for TFL website. The implementation of the steps is using Selenium Chrome WebDriver using best practices of Selenium and SpecFlow. Objects have been identified by Id mostly and Xpath has been used for complex identifications.

## Steps to run the framework
Change directory to project folder 'tlfwebautomation'
Update the chrome driver version as per chrome version on running machine in file 'tlfwebautomation.csproj' for `Selenium.WebDriver.ChromeDriver`
Command to run tests - `dotnet test`

## Generating HTML report using SpecflowLivingDoc
Once the test execution is complete run below commands:
Command to setup livivngDoc - `dotnet tool install --global SpecFlow.Plus.LivingDoc.CLI`
Command to generate report - `livingdoc test-assembly "bin\Debug\netcoreapp3.1\tlfwebautomation.dll" -t "bin\Debug\netcoreapp3.1\TestExecution.json"` 
The HTML report would be generated in folder 'tlfwebautomation'

## Installation

IDE Visual Studio Code. Install the below extensions:
.Net Core Tools, Specflow Tools, Specflow Steps Definition Generator, C#, Cucumber (Gherkin) Full Support, .Net Core Test Explorer(Optional). Using Framework: .NET 6.0.9

## Test Summary
There are total 7 tests in the project out of which 6 are passing and 1 is failing. The failing test case looks like a potential defect(not totally sure of the functionality). The latest generted report is also pushed to git.

Download the project
Build by right clicking on tlfwebautomation.csproj
Run the tests using steps provided above
