using UnityEngine;
using System.IO;

public class LanguageSelector : MonoBehaviour
{
    private const string LanguageFilePath = "D:\\ChildVR\\Language.txt";

    public static LanguageSelector Instacne = null;

    // Default is chinese
    public SystemLanguage CurrentSystemLanguage = SystemLanguage.Chinese;

    LanguageSelector()
    {
        Instacne = this;
    }

    void Awake()
    {
        CurrentSystemLanguage = Get();
       // print("CurrentSystemLanguage =" + CurrentSystemLanguage);
    }

    private SystemLanguage Get()
    {
        if (File.Exists(LanguageFilePath))
        {
            string FileContent = File.ReadAllText(LanguageFilePath);

            // 这里用正则更好，
            if (FileContent.Contains("zh-CN") || FileContent.Contains("chinese") || FileContent.Contains("Chinese"))    return SystemLanguage.Chinese;
            if (FileContent.Contains("en-US") || FileContent.Contains("english") || FileContent.Contains("English"))    return SystemLanguage.English;
            if (FileContent.Contains("ko-KR") || FileContent.Contains("korean") || FileContent.Contains("Korean"))      return SystemLanguage.Korean;
        }

        return SystemLanguage.Chinese;
    }
}
