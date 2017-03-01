using UnityEngine;

public class MoveByLerp : MonoBehaviour
{
    public Vector3 LerpOffSet = Vector3.zero;
    public float Speed = 1.0f;
    private bool CanLerp = false;

    void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag(Config.TPlayer))
        {
            CanLerp = true;
        }
    }

    void OnTriggerExit(Collider Other)
    {
        if (Other.CompareTag(Config.TPlayer))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(CanLerp)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + LerpOffSet, Time.deltaTime * Speed);
        }
    }
}
