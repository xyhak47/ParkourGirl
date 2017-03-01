using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectFont : MonoBehaviour
{
    public Font Font_chinese;
    public Font Font_korean;

    void Start()
    {
        if (LanguageSelector.Instacne.CurrentSystemLanguage == SystemLanguage.Korean)
        {
            GetComponent<Text>().font = Font_korean;
        }
    }
}
