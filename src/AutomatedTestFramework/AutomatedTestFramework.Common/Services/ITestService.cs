using AutomatedTestFramework.Common.DTOs;

namespace AutomatedTestFramework.Common.Services {
    public interface ITestService {
        TestContext PrepareTestContext(AutomaticTest test);
    }
}