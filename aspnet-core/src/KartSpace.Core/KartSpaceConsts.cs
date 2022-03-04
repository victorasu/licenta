using KartSpace.Debugging;

namespace KartSpace
{
    public class KartSpaceConsts
    {
        public const string LocalizationSourceName = "KartSpace";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "3ee31736a6394c109b9798a95cf58506";
    }
}
