using System;
using System.Configuration;

namespace Voting
{
    public class GlobalEnv
    {
        private static readonly EnvironmentType Environment;

        static GlobalEnv()
        {
            Enum.TryParse(ConfigurationManager
                .AppSettings["Env"], true, out Environment);
        }

        public static string ConnectionString
        {
            get
            {
                return "Voting.Development.ConnectionString";
            }
        }
        
        public static bool InDevelopment
        {
            get { return Environment == EnvironmentType.Development; }
        }

        public static bool InStaging
        {
            get { return Environment == EnvironmentType.Staging; }
        }

        public static bool InTesting
        {
            get { return Environment == EnvironmentType.Testing; }
        }

        public enum EnvironmentType
        {
            Development,
            Staging,
            Testing,
            Production
        }
    }
}
