
namespace Selenium_XUnit_Specflow.Hooks;

[Binding]
public class Initialization
{
    private static ExtentReports _extentReports;
    private readonly ScenarioContext _scenarioContext;
    private readonly FeatureContext _featureContext;
    private ExtentTest _scenario;

    public Initialization(ScenarioContext scenarioContext, FeatureContext featureContext)
    {
        _scenarioContext = scenarioContext;
        _featureContext = featureContext;
    }

    [BeforeTestRun]
    public static void InitializationExtentReports()
    {
        //Get Current Directory and where to store the report
        var extentReport = 
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/extentreport.html";
        _extentReports = new ExtentReports();
        var spark = new ExtentSparkReporter(extentReport);
        _extentReports.AttachReporter(spark);
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        var feature = _extentReports.CreateTest<Feature>(_featureContext.FeatureInfo.Title); //Will name the test by the Feature name
        _scenario = feature.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title); //Will name the test by the Feature name
    }

    [AfterTestRun]
    public static void TearDownReport() => _extentReports.Flush();
}
