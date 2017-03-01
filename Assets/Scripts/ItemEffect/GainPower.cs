using UnityEngine;

public class GainPower : MonoBehaviour
{
    public int PowerValue = 1;

    void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag(Config.TPlayer))
        {
             PlayerConroller.Instance.GainPower(PowerValue);
             Destroy(gameObject);
        }
    }
}
