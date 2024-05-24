
using AventStack.ExtentReports.Model;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow.Bindings;
using Test_Framework.Driver;

namespace Selenium_XUnit_Specflow.Hooks;

[Binding]
public class Initialization
{
    private static ExtentReports _extentReports;
    private readonly ScenarioContext _scenarioContext;
    private readonly FeatureContext _featureContext;
    private readonly IDriverFixture _driverFixture;
    private ExtentTest _scenario;

    public Initialization(ScenarioContext scenarioContext, FeatureContext featureContext, IDriverFixture driverFixture)
    {
        _scenarioContext = scenarioContext;
        _featureContext = featureContext;
        _driverFixture = driverFixture;
    }

    [BeforeTestRun]
    public static void InitializationExtentReports()
    {
        //Get Current Directory and WHERE to store the test report.
        var extentReport = 
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $"../../../../Reports/Test_Report.html";
        _extentReports = new ExtentReports();
        var spark = new ExtentSparkReporter(extentReport);
        _extentReports.AttachReporter(spark);
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        var feature = _extentReports.CreateTest<Feature>(_featureContext.FeatureInfo.Title);
        _scenario = feature.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title); 
    }

    [AfterStep]
    public void AfterScenario() 
    {
        var fileName = 
            $"{_featureContext.FeatureInfo.Title.Trim()}_" + 
            $"{Regex.Replace(_scenarioContext.ScenarioInfo.Title, @"\s", "")}"; //Removes any empty spaces in the Scenario Name.

        if (_scenarioContext.TestError == null) //Pass Flow
        {
            switch (_scenarioContext.StepContext.StepInfo.StepDefinitionType)
            {
                case StepDefinitionType.Given:
                    _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                    break;
                case StepDefinitionType.When:
                    _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                    break;
                case StepDefinitionType.Then:
                    _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                    break;
                default: 
                    throw new ArgumentOutOfRangeException();
            }
        }
        else //Fail Flow
        {
            switch (_scenarioContext.StepContext.StepInfo.StepDefinitionType)
            {
                case StepDefinitionType.Given:
                    _scenario
                        .CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text)
                        .Fail(_scenarioContext.TestError.Message, new ScreenCapture()
                        {
                            Path = _driverFixture.TakeScreenshotAsPath(fileName),
                            Title = "Failed test: Sceenshot of the 'Given' step page"
                        });
                    break;
                case StepDefinitionType.When:
                    _scenario
                        .CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text)
                        .Fail(_scenarioContext.TestError.Message, new ScreenCapture()
                        {
                            Path = _driverFixture.TakeScreenshotAsPath(fileName),
                            Title = "Failed test: Sceenshot of the 'When' step page"
                        });
                    break;
                case StepDefinitionType.Then:
                    _scenario
                        .CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text)
                        .Fail(_scenarioContext.TestError.Message, new ScreenCapture()
                        {
                            Path = _driverFixture.TakeScreenshotAsPath(fileName),
                            Title = "Failed test: Sceenshot of the 'Then' step page"
                        });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    [AfterTestRun]
    public static void TearDownReport() => _extentReports.Flush();

}
