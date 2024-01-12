using ItemE2E.Services;

namespace ItemE2E.Hooks
{
    [Binding]
    public sealed class ScreenShotHook
    {
        private readonly IPageDependencyService _pageDependencyService;

        public ScreenShotHook(IPageDependencyService pageDependencyService)
        {
            _pageDependencyService = pageDependencyService;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var path = $"./Screenshots/{_pageDependencyService.ScenarioContext.ScenarioInfo.Title}";
            _pageDependencyService.ScreenShotService.CreateDirectory(path);
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            //Fail
            if (_pageDependencyService.ScenarioContext.TestError != null)
                await _pageDependencyService.ScreenShotService.TakeScreenshot(_pageDependencyService.ScenarioContext.ScenarioInfo.Title, null);
        }
    }
}