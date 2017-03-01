using UnityEngine;

public class Recover : MonoBehaviour
{
    public int RecoverValue = 20;

    void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag(Config.TPlayer))
        {
            PlayerConroller.Instance.Recover(RecoverValue);
            GameController.Instance.PlayMusic(Config.MBenefit);
            PlayerConroller.Instance.SpawnParticular(Config.PRecover);
            Destroy(gameObject);
        }
    }
}
