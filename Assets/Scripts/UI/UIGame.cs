using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    public Text Score;
    public Text Distance;
    public Image Blood;
    public Image Power; // being 100, upgrade 50 , auto release, speed * 200%, 无敌， 10s

    [SerializeField]
    private GameObject Logo;

    [SerializeField]
    private float LogoShowDuration = 6;

    [SerializeField]
    private float DelayLogoShowTime = 6;

    void Start()
    {
        StartCoroutine(ShowLogo());
    }

    private IEnumerator ShowLogo()
    {
        yield return new WaitForSeconds(DelayLogoShowTime);
        Logo.SetActive(true);
        yield return new WaitForSeconds(LogoShowDuration);
        Destroy(Logo.gameObject);
    }

    public void Invincible(bool Begin)
    {
        GetComponent<Animator>().SetBool("Invincible", Begin);
    }
}
