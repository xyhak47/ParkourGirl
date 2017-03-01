using UnityEngine;

public class ZombieFollowerController : MonoBehaviour
{
    public static ZombieFollowerController Instance;
    private ZombieFollowerController()
    {
        Instance = this;
    }

    public ZombieFollower ChildZombieFollower;
    public int LowBloodValue = 20;
    public int ChildZombieFollowerLife = 1;
    private bool GameOver = false;

    void Start()
    {
        ChildZombieFollower.Hide();
    }

    void Update()
    {
        if (NeedToShowZombie() )
        {
            ChildZombieFollower.Show();
        }
    }

    private bool NeedToShowZombie()
    { 
        bool LowBlood = PlayerConroller.Instance.CurrentLife < LowBloodValue;
        bool InHiden = !ChildZombieFollower.gameObject.active;
        return !GameOver && LowBlood && InHiden;
    }

    public void Hurt(int HurtValue)
    {
        ChildZombieFollowerLife -= HurtValue;

        if(ChildZombieFollowerLife <= 0)
        {
            ChildZombieFollower.Die();
        }
    }

    public void Stop()
    {
        GameOver = true;
        ChildZombieFollower.Hide();
    }
}
