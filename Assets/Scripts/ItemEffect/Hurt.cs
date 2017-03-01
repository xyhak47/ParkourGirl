using UnityEngine;

public class Hurt : MonoBehaviour
{
    public int HurtValue = 0;

    void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag(Config.TPlayer))
        {
            PlayerConroller.Instance.Hurt(HurtValue);

            if (gameObject.tag == Config.TBarrier)
            {
                GameController.Instance.PlayMusic(Config.MHit);
                PlayerConroller.Instance.SpawnParticular(Config.PHurt);
            }
            else if (gameObject.tag == Config.TBarrier)
            {
                GameController.Instance.PlayMusic(Config.MBomb);
                PlayerConroller.Instance.SpawnParticular(Config.PBomb);
            }

            GameController.Instance.PlayMusic(Config.MHurt);

            Destroy(gameObject);
        }
        else if (Other.CompareTag(Config.TAI))
        {
            ZombieFollowerController.Instance.Hurt(HurtValue);
            Destroy(gameObject);
        }
    }
}
