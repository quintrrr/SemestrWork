using SemestrWork.ApiClients;
using System.Configuration;

namespace SemestrWork
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUri"] ?? "")
            };

            var groupsApiClient = new GroupsApiClient(httpClient);
            var propertiesApiClient = new PropertiesApiClient(httpClient);
            var relationsApiClient = new RelationsApiClient(httpClient);
            Application.Run(new Form1(groupsApiClient, propertiesApiClient, relationsApiClient));
        }
    }
}