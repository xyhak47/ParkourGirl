using System.Collections;
using UnityEngine;

public class PlayerConroller : MonoBehaviour
{
    public static PlayerConroller Instance { get; private set; }
    PlayerConroller()
    {
        Instance = this;
    }

    /************* Player Animation Control ****/
    public Animator PawnAnimator { get; private set; }
    private bool CanTurn;
    private float TargetPositionX;
    public Mover SelfMover;

    private static class TargetPosition
    {
        public const float Left = -1.5f;
        public const float Right = 1.5f;
        public const float Middle = 0f;
    }


    /************* Player Atttibute ****/
    public GameObject LolaPawn;
    public int CurrentLife = 0;
    public int TotalLife = 1000;
    public int CurrentScore = 0;
    private int CurrentPower = 0;
    private int TotalPower = 100;
    private int PowerToIncrease = 50;

    /************* Item Effect ****/
    public MagnetWorker ChildMagnetWorker;
    public ParticularWorker ChildParticularWorker;
    private bool Invincible { get; set; }
    private bool InSpeedingUp { get; set; }

    /************* Jumping & Check Input ****/
    private Rigidbody SelfRigidbody;
    public float JumpSpeed;
    public  bool ReceiveHMDJump = false;

    /*************** Tutorial **************/
    private bool LockInputLeft;
    private bool LockInputRight;
    private bool LockInputJump;

    /*************** Speed up **************/
    public int MaxSpeed = 10;
    public int SpeedUpDistancePer = 500;
    private int CurrentSpeedUpDistancePer;

    void Start()
    {
        // Girl run
        PawnAnimator = LolaPawn.GetComponent<Animator>();
        PawnAnimator.SetTrigger(Config.Run);

        CanTurn = false;
        TargetPositionX = TargetPosition.Middle;

        CurrentLife = TotalLife;

        Invincible = false;
        InSpeedingUp = false;

        ChildMagnetWorker.ShutDown();

        SelfRigidbody = GetComponent<Rigidbody>();

        LockInput(false);

        CurrentSpeedUpDistancePer = SpeedUpDistancePer;
    }

    void Update()
    {
        if (IsAlive())
        {
            CheckInput();
            UpdateDistance();
        }
    }

    /******************** Check Input  *******************/
    private void CheckInput()
    {
        if (!LockInputLeft && Input.GetKeyDown(KeyCode.Alpha1))
        {
            PawnAnimator.SetTrigger(Config.Turn);
            GameController.Instance.PlayMusic(Config.MJump);

            TargetPositionX += TargetPosition.Left;
            TargetPositionX = Mathf.Clamp(TargetPositionX, TargetPosition.Left, TargetPosition.Right);
            CanTurn = true;
        }
        else if (!LockInputRight && Input.GetKeyDown(KeyCode.Alpha2))
        {
            PawnAnimator.SetTrigger(Config.Turn);
            GameController.Instance.PlayMusic(Config.MJump);

            TargetPositionX += TargetPosition.Right;
            TargetPositionX = Mathf.Clamp(TargetPositionX, TargetPosition.Left, TargetPosition.Right);
            CanTurn = true;
        }
        else if (!LockInputJump && IsGrounded() && ReceiveHMDJump)
        {
            SelfRigidbody.AddForce(Vector3.up * JumpSpeed, ForceMode.VelocityChange);
            PawnAnimator.SetTrigger(Config.Jump);
            GameController.Instance.PlayMusic(Config.MJump);
            ReceiveHMDJump = false;
        }

        if (CanTurn)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(TargetPositionX, transform.position.y, transform.position.z), 6 * Time.deltaTime);

            if (transform.position.x == TargetPositionX)
            {
                CanTurn = false;
            }
        }
    }

    public bool IsGrounded()
    {
         return Physics.Raycast(transform.position, Vector3.down, 0.05f, LayerMask.GetMask(Config.MGround)); 
    }

    /******************** Item effect *******************/
    public void Hurt(int How)
    {
        if(!Invincible)
        {
            CurrentLife -= How;
            CurrentLife = Mathf.Clamp(CurrentLife, 0, CurrentLife);
            UIController.Instance.CalculateBloodToUI(1.0f * CurrentLife / TotalLife);
            GameController.Instance.PlayMusic(Config.MHit);

            if (!IsAlive())
            {
                Die();
            }
        }
    }

    public void Recover(int How)
    {
        CurrentLife += How;
        CurrentLife = Mathf.Clamp(CurrentLife, CurrentLife, TotalLife);
        UIController.Instance.CalculateBloodToUI(1.0f * CurrentLife / TotalLife);
    }

    public void MagnetWork(int Second)
    {
        StartCoroutine(MagnetWorkForSecond(Second));
    }

    private IEnumerator MagnetWorkForSecond(int Second)
    {
        ChildMagnetWorker.Work();
        GameObject MagnetParticular = ChildParticularWorker.SpawnParticularPerfab(Config.PMagnet);
        yield return new WaitForSeconds(Second);
        ChildMagnetWorker.ShutDown();
        Destroy(MagnetParticular);
    }

    public void GainScore(int Score)
    {
        CurrentScore += Score;
        UIController.Instance.CalculateScoreToUI(CurrentScore);
    }

    public void GainPower(int Power)
    {
        CurrentPower += Power;
        CurrentPower = Mathf.Clamp(CurrentPower, 0, TotalPower);
        UIController.Instance.CalculatePowerToUI(1.0f * CurrentPower / TotalPower);
        CheckPowerFull();
    }

    private void UpdateDistance()
    {
        int Distance = (int)LolaPawn.transform.position.z;
        Distance = Mathf.Clamp(Distance, 0, Mathf.Abs(Distance));
        UIController.Instance.CalculateDistanceToUI(Distance);

        // Check speed up
        if(Distance / CurrentSpeedUpDistancePer == 1)
        {
            CurrentSpeedUpDistancePer += SpeedUpDistancePer;
            SelfMover.CurrentSpeed++;
            SelfMover.CurrentSpeed = Mathf.Clamp(SelfMover.CurrentSpeed, SelfMover.CurrentSpeed, MaxSpeed);
            SelfMover.ResetSpeed(SelfMover.CurrentSpeed);
        }
    }

    private void CheckPowerFull()
    {
        if(CurrentPower == TotalPower)
        {
            TotalPower += PowerToIncrease;
            CurrentPower = 0;
            StartCoroutine(SpeedUpAndInvincible());
        }
    }

    private IEnumerator SpeedUpAndInvincible()
    {
        SelfMover.ResetSpeed(SelfMover.CurrentSpeed * Config.ESpeedUpPercent);
        // StartCoroutine(LerpToZero(Config.ESpeedUpLastSecond));
        UIController.Instance.CalculatePowerToUI(0);
        UIController.Instance.Invincible(true);
        GameObject InvincibleParticular = ChildParticularWorker.SpawnParticularPerfab(Config.PInvincible);
        Invincible = true;
        InSpeedingUp = true;
        yield return new WaitForSeconds(Config.ESpeedUpLastSecond);
        UIController.Instance.Invincible(false);
        SelfMover.ResumeSpeed();
        Destroy(InvincibleParticular);
        InSpeedingUp = false;
        Invincible = false;
    }

    //private IEnumerator LerpToZero(float Second)
    //{
    //    Debug.Log("begin LerpToZero");
    //    yield return new WaitForSeconds(Second / 100);
    //    int LosePower = -(int)Mathf.Lerp(0, TotalPower, 1.0f / 100);
    //    GainPower(LosePower);

    //    if (CurrentPower != 0)
    //    {
    //        StartCoroutine(LerpToZero(Second));
    //    }
    //}

    public void SpawnParticular(string ParticularName)
    {
        ChildParticularWorker.SpawnParticularPerfab(ParticularName);
    }

    private bool IsAlive()
    {
        return CurrentLife > 0;
    }

    private void Die()
    {
        SelfMover.ResetSpeed(0);
        print("ResetSpeed(0) in die");
        PawnAnimator.SetTrigger(Config.Dead);
        GameController.Instance.GameOver();
    }

    /*************** Tutorial for lock inout **************/
    public void LockInput(bool Lock)
    {
        LockInputLeft = Lock;
        LockInputRight = Lock;
        LockInputJump = Lock;
    }

    public void UnlockInputLeft()
    {
        LockInputLeft = false;
    }

    public void UnlockInputRight()
    {
        LockInputRight = false;
    }

    public void UnlockInputJump()
    {
        LockInputJump = false;
    }
}