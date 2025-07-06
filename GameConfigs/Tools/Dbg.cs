using UnityEngine;

namespace Tools
{
    public static class Dbg
    {
        public static void Log(string message, Color color = default)
        {
            if(color == default)
                color = Color.white;
            
            string hexColor = ColorUtility.ToHtmlStringRGB(color);
            Debug.Log($"<color=#{hexColor}>{message}</color>");
        }
        
        public static void LogError(string message)
        {
            string hexColor = ColorUtility.ToHtmlStringRGB(Color.crimson);
            Debug.LogError($"<color=#{hexColor}>{message}</color>");
        }
        
        public static void LogWarning(string message)
        {
            string hexColor = ColorUtility.ToHtmlStringRGB(Color.yellowNice);
            Debug.LogError($"<color=#{hexColor}>{message}</color>");
        }
    }
}