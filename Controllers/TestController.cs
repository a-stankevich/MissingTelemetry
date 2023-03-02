using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MissingTelemetry.Controllers
{
    public class TestController : Controller
    {
        private static readonly HttpClient httpClient = new HttpClient();

        // This dependency will have Operation Id as well as Client IP, Operation Name, Session Id, User Id (if any) taken from request operation.
        void callSyncDependency()
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadData("https://github.com/microsoft/ApplicationInsights-dotnet");
            }
        }

        // This dependency will only have Operation Id taken from request operation.
        Task callAsyncDependency()
        {
            return httpClient.GetAsync("https://github.com/microsoft/ApplicationInsights-dotnet/issues");
        }

        public ContentResult Index()
        {
            return Content("Try visiting <a href='/TestSync'>TestSync</a>, <a href='/TestAsync'>TestAsync</a>.<br>Then review collected Application Insights telemetry");
        }

        public ContentResult TestSync()
        {
            callSyncDependency();
            callAsyncDependency().Wait();
            return Content("I am sync. <a href='/'>Go back</a>");
        }

        public async Task<ContentResult> TestAsync()
        {
            callSyncDependency();
            await callAsyncDependency();
            return Content("I am async. <a href='/'>Go back</a>");
        }
    }
}