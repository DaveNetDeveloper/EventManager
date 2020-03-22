using System.Globalization;

namespace EventManager.Managers
{
    public static class Translator
    {
        #region [ constants ] 

        public const string CULTURE_INFO_ESPAÑOL = "es-ES";
        public const string CULTURE_INFO_CATALAN = "ca-ES";
        public const string CULTURE_INFO_INGLES = "en-US";

        #endregion

        #region [ properties ]

        public static CultureInfo Culture { get; set; }

        #endregion

        #region [ public ]

        public static void Trasnlate()
        {
        }

        #endregion
    }
}