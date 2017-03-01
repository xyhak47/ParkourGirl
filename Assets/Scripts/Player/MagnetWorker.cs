using System.Collections;
using UnityEngine;

public class MagnetWorker : MonoBehaviour
{
    public float MagnetSpeed = 10;

    void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag(Config.TCoin))
        {
            print("MagnetWorker OnTriggerEnter");
            StartCoroutine(MoveToPlayer(Other.gameObject));
        }
    }

    public void ShutDown()
    {
        gameObject.active = false;
    }

    public void Work()
    {
        gameObject.active = true;
    }

    private IEnumerator MoveToPlayer(GameObject Target)
    {
        yield return new WaitForSeconds(Time.smoothDeltaTime);
        if (Target)
        {
            Target.transform.position = Vector3.MoveTowards(Target.transform.position, PlayerConroller.Instance.transform.position, Time.smoothDeltaTime * MagnetSpeed);
            if (Vector3.Distance(Target.transform.position, PlayerConroller.Instance.transform.position) >= 0.7f)
            {
                StartCoroutine(MoveToPlayer(Target));
            }
        }
    }


}
