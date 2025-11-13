using NLog;
using RebuildProject;

Logger logger = LogManager.GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    app.Run();
}
catch (HostAbortedException ex)
{
    logger.Info(ex.Message);
}
catch (Exception ex)
{
    logger.Error(ex, "Failed to start API.");
}
finally
{
    logger.Info("Shut down complete");
    LogManager.Shutdown();
}