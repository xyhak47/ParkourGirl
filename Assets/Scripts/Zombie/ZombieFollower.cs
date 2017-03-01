using UnityEngine;
using System.Collections;

public class ZombieFollower : MonoBehaviour
{
    public float SpeedAddedRate = 0.05f;
    public float StartInvincibleTime = 3.0f;
    public float CurrentRelativeSpeed = 0.2f;
    public int PlayerRecoverBloodWhenDie = 30;

    private bool Invincible = true;
    private int ShowCount = 0;

    void Start()
    {
    }

    public void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(BeInvincible());
        CurrentRelativeSpeed += (1.0f * ShowCount * SpeedAddedRate);
        ShowCount++;
        GetComponent<Mover>().ResetSpeed(CurrentRelativeSpeed);
        GetComponent<Animation>().Play("run");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Die()
    {
        if(Invincible && !gameObject.active)
        {
            return;
        }

        PlayerConroller.Instance.Recover(PlayerRecoverBloodWhenDie);
        GetComponent<Animation>().Play("fallToFace");
        StartCoroutine(Dead());
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(1);
        Hide();
    }

    public void Attack()
    {
        GetComponent<Animation>().Play("attack1");
    }

    public void WinPose()
    {
        GetComponent<Animation>().Play("happy");
        GetComponent<Mover>().ResetSpeed(0);
    }

    private IEnumerator BeInvincible()
    {
        Invincible = true;
        yield return new WaitForSeconds(StartInvincibleTime);
        Invincible = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Config.TPlayer))
        {
            StartCoroutine(Win());
        }
        //else  if (other.CompareTag(Config.TBarrier) || other.CompareTag(Config.TBomb))
        //{
        //    Die();
        //}
    }

    private IEnumerator Win()
    {
        Attack();
        GameController.Instance.PlayMusic(Config.MHit);
        yield return new WaitForSeconds(0.3f);

        // kill player immediately
        PlayerConroller.Instance.Hurt(PlayerConroller.Instance.TotalLife);
        WinPose();
    }

}
