using System.Collections;
using UnityEngine;

public class ZombieWalker : MonoBehaviour
{
    public int GainScore = 20;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Config.TPlayer))
        {
            PlayerConroller.Instance.PawnAnimator.SetTrigger("Attack");

            GetComponent<Animation>().Play("fallToBack");
            PlayerConroller.Instance.SpawnParticular(Config.PHitZombie);
            PlayerConroller.Instance.GainScore(GainScore);
            GameController.Instance.PlayMusic(Config.MHitZombie);
        }
    }
}
