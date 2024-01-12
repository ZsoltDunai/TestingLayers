namespace ItemE2E.Services;

public interface IScreenShotService
{
    void CreateDirectory(string path);

    void DeleteFolderContent(string path);

    Task TakeScreenshot(string title, string? text);
}

public class ScreenShotService : IScreenShotService
{
    private readonly Task<IPage> _page;

    public ScreenShotService(Task<IPage> page)
    {
        _page = page;
    }

    public void CreateDirectory(string path)
    {
        CreateDirectoryOrDeleteFilesInDirectory(path);
    }

    public void AddSubDownloadFolder(string path)
    {
        CreateDirectoryOrDeleteFilesInDirectory(path);
    }

    public void DeleteFolderContent(string path)
    {
        DeleteFilesFromFolder(path);
    }

    public static void CreateDirectoryOrDeleteFilesInDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        else
        {
            DeleteFilesFromFolder(path);
        }
    }

    public static void DeleteFilesFromFolder(string path)
    {
        var di = new DirectoryInfo(path);
        foreach (var file in di.GetFiles()) file.Delete();
    }

    public async Task TakeScreenshot(string title, string? text)
    {
        string basePath = $"./Screenshots/{title}/";

        var timeAndExtension = $"{DateTime.Now:yyyy-MM-dd-HH-mm-ss-fff}.png";
        if (text is null)
        {
            await _page.Result.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = $"{basePath}_Last_failed_picture_{timeAndExtension}"
            });
        }
        else
        {
            await _page.Result.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = $"{basePath}_Step_{text}_{timeAndExtension}"
            });
        }
    }
}
