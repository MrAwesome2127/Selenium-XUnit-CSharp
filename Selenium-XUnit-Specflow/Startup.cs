using Selenium_XUnit_Specflow.Pages;
using Test_Framework.Config;
using Test_Framework.Driver;

namespace Selenium_XUnit_Specflow;

public class Startup
{
    [ScenarioDependencies]
    public static IServiceCollection CreatServices()
    {
        var services = new ServiceCollection();

        services
            .AddSingleton(ConfigReader.ReadConfig()) //Reads Config on startup

            //Below can be added into the Test.cs public access modifier to incoporated
            //Example: public <TestName>(IDriverFixture driverFixture)
            .AddScoped<IDriverFixture, DriverFixture>()
            .AddScoped<IDriverWait, DriverWait>() //Not needed in test since inherited as an autowait when searching

            //Below adds POM.
            //Each new POM must be added below.
            //Also added to Test.cs public access modifier to incoporated
            //public <TestName>(IDriverFixture driverFixture, IHomePage homePage, IProductPage productPage)
            .AddScoped<IHomePage, HomePage>()
            .AddScoped<IProductPage, ProductPage>();

        return services;
    }
}
