using UnityEngine;

namespace CoreLib.Common
{
    public static class Dbg
    {
        public static void Log(string message, Color color = default)
        {
            if (color == default)
                color = Color.white;

            var hexColor = ColorUtility.ToHtmlStringRGB(color);
            Debug.Log($"<color=#{hexColor}>{message}</color>");
        }

        public static void LogError(string message)
        {
            var hexColor = ColorUtility.ToHtmlStringRGB(Color.crimson);
            Debug.LogError($"<color=#{hexColor}>{message}</color>");
        }

        public static void LogWarning(string message)
        {
            var hexColor = ColorUtility.ToHtmlStringRGB(Color.yellowNice);
            Debug.LogError($"<color=#{hexColor}>{message}</color>");
        }

        public static void LogInfra(string message)
        {
            var hexColor = ColorUtility.ToHtmlStringRGB(Color.orangeRed);
            Debug.LogError($"Infra: <color=#{hexColor}>{message}</color>");
        }
    }
}