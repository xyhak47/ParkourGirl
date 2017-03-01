using UnityEngine;

public class Magnet : MonoBehaviour
{
    public int Second;

    void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag(Config.TPlayer))
        {
            PlayerConroller.Instance.MagnetWork(Second);
            Destroy(gameObject);
        }
    }
}
