using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectImage : MonoBehaviour
{
    public Sprite Image_chinese;
    public Sprite Image_korean;
    public Sprite Image_English;

    void Start ()
    {
        if (LanguageSelector.Instacne.CurrentSystemLanguage == SystemLanguage.Korean)
        {
            GetComponent<Image>().sprite = Image_korean;
        }
        else if (LanguageSelector.Instacne.CurrentSystemLanguage == SystemLanguage.English)
        {
            GetComponent<Image>().sprite = Image_English;
        }
    }
}
