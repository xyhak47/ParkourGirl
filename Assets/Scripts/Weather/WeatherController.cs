using System.Collections;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public static WeatherController Instance;
    private WeatherController()
    {
        Instance = this;
       // RenderSettings.skybox = sky_night;

    }

    private GameObject CurrentWeather = null;

    public void AttachWeather(GameObject NewWeather, float Duration)
    {
        DetachPreviousWeather();

        CurrentWeather = NewWeather;
        NewWeather.transform.parent = this.transform;
        NewWeather.transform.localPosition = Vector3.zero;

        // Force
        StartCoroutine(ForceDetachPreviousWeather(Duration));
    }

    private void DetachPreviousWeather()
    {
        if (CurrentWeather != null)
        {
            Destroy(CurrentWeather);
        }
    }

    private IEnumerator ForceDetachPreviousWeather(float Duration)
    {
        yield return new WaitForSeconds(Duration);
        DetachPreviousWeather();
    }
}
