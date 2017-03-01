using UnityEngine;

public class Weather : MonoBehaviour
{
    public float Duration = 10;
    public GameObject CurrentWeather;

    void Awake()
    {
        CurrentWeather.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Config.TPlayer))
        {
            GetComponent<BoxCollider>().enabled = false;
            WeatherController.Instance.AttachWeather(gameObject, Duration);
            CurrentWeather.SetActive(true);
        }
    }
}
