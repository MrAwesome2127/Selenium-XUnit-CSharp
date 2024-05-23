using static Test_Framework.Driver.DriverFixture;

namespace Test_Framework.Config;

public class TestSettings
{
    public BrowserType BrowserType { get; set; }
    public Uri ApplicationUrl { get; set; }
    public float? TimeoutInternal{ get; set; }  
}
