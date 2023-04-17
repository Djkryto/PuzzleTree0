using System;
using System.IO;
using UnityEngine;

public static class GameLogger
{
    public static void WriteToLog(Exception exception, bool isError = false)
    {
        var isDevelopVersion = Application.version[0] == 'd';
        if (isError)
            Debug.LogError(exception);
        else
            Debug.LogWarning(exception);
        if (isDevelopVersion)
            WriteToLog(exception.ToString());
    }

    public static void WriteToLog(string text)
    {
        string path = GetPath();
        var appendText = DateTime.Now.ToString() + ": " + text + "\n";
        File.AppendAllText(path, appendText);
    }

    private static string GetPath()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return Path.Combine(Application.persistentDataPath, "log.txt");
#else
        return Path.Combine(Application.dataPath, "log.txt");
#endif
    }
}
