using AutomatedTestFramework.Common.DTOs;
using System.Collections.Generic;

namespace AutomatedTestFramework.Common.Services
{
    public interface IImportTestDataService
    {
        #region Methods

        IList<AutomaticTest> ParseTestCases(IList<string> paths);

        #endregion Methods
    }
}