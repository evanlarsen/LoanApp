using System;
using System.Configuration;

namespace Merchant.Api.Infrastructure
{
    /// <summary>
    /// Standard connection string.
    /// </summary>
    public static class ConnectionString
    {
        /// <summary>
        /// Gets the default connection string for the application. 
        /// </summary>
        /// <returns>string. The default connection stirng of the application.</returns>
        /// <remarks>
        /// This methods attemps to find a app setting named "DefaultConnectionStringKey" in the application's configuration file. This
        /// setting should contain the name of the connection string defined in the &lt;connectionStrings&gt; that will be used as the
        /// application's default connection string.
        /// </remarks>
        public static string Default()
        {
            var @default = ConfigurationManager.AppSettings["DefaultConnectionString"];
            if (string.IsNullOrEmpty(@default))
                throw new ApplicationException(
                    "No default connection string was found for the application. To add a default connection string " +
                    "add an entry in <AppSettings> configuration section with the key \"DefaultConnectionString\" and specify the " +
                    "connection string name that will be used as the default connection string.");

            return ConfigurationManager.ConnectionStrings[@default].ConnectionString;
        }

        /// <summary>
        /// Gets a connection string that can be used for the current machine, or if no applicable connection string found
        /// then gets the default connection string by calling <see cref="Default"/>.
        /// </summary>
        /// <returns>string. A connection stirng applicable for the current machine, or the default connection string.</returns>
        public static string ForMachine()
        {
            var machineBasedConnection = ConfigurationManager.ConnectionStrings[Environment.MachineName];
            if (machineBasedConnection == null)
                return Default();
            return machineBasedConnection.ConnectionString;
        }
    }
}
