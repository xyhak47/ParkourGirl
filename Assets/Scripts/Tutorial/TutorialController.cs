using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public static TutorialController Instance;
    private TutorialController()
    {
        Instance = this;
    }

    [System.Serializable]
    public class Tutorial
    {
        public GameObject TutorialPerfab;
        public string Name;
    }

    private TutorialStep CurrentSetp;
    public bool InTutorial { get; private set; }
    private bool WaitForInputLeft;
    private bool WaitForInputRight;
    private bool WaitForInputJump;

    [System.NonSerialized]
    public bool ReceiveHMDJump = false;

    public List<Tutorial> List_Tutorial;

    private float ForceCompleteTime = 0;

    private Coroutine CurrentCoroutine = null;

    void Start()
    {
        WaitForInputLeft = false;
        WaitForInputRight = false;
        WaitForInputJump = false;
    }

    public void EnterStep(TutorialStep WhichStep)
    {
        // CurrentSetp change parent to this
        WhichStep.transform.parent = this.transform;
        WhichStep.transform.localPosition = Vector3.zero;
        CurrentSetp = WhichStep;
        if(CurrentCoroutine != null)
        {
            StopCoroutine(CurrentCoroutine);
           // print("StopCoroutine when CurrentCoroutine != null");
        }

        // Get step tye
        WaitForInputLeft = CurrentSetp.gameObject.name == Config.TTRTurnLeft;
        WaitForInputRight = CurrentSetp.gameObject.name == Config.TTRTurnRight;
        WaitForInputJump = CurrentSetp.gameObject.name == Config.TTRJump;

        // Stop player moving
        PlayerConroller.Instance.SelfMover.ResetSpeed(0);
        //print("ResetSpeed(0) in EnterStep:" + CurrentSetp.gameObject.name);

        // Ugly here, check input lock
        PlayerConroller.Instance.LockInput(true);
        if (WaitForInputLeft)
        {
            PlayerConroller.Instance.UnlockInputLeft();
        }
        else if (WaitForInputRight)
        {
            PlayerConroller.Instance.UnlockInputRight();
        }
        else if (WaitForInputJump)
        {
            PlayerConroller.Instance.UnlockInputJump();
        }

        // Wait for Force Complete Curren tStep
        CurrentCoroutine = StartCoroutine(ForceCompleteCurrentStep());

        // Final we open tutorial after logic above
        InTutorial = true;
    }

    private void Update()
    {
        if (InTutorial)
        {
            CheckInputForStep();
        }
    }

    // Check device input for step, wait or continue
    private void CheckInputForStep()
    {
        if (WaitForInputLeft && Input.GetKeyDown(KeyCode.Alpha1))
        {
            CompleteCurrentStep();
        }
        else if (WaitForInputRight && Input.GetKeyDown(KeyCode.Alpha2))
        {
            CompleteCurrentStep();
        }
        else if (WaitForInputJump && ReceiveHMDJump) 
        {
            ReceiveHMDJump = false;
            CompleteCurrentStep();
        }
        else { } // Do nothing
    }

    private void CompleteCurrentStep()
    {
        // Stop force, cause we have already complete it here
        StopCoroutine(CurrentCoroutine);
        CurrentCoroutine = null;

        // Reset some flag
        InTutorial = false;
        WaitForInputLeft = false;
        WaitForInputRight = false;
        WaitForInputJump = false;

        // Reset PlayerConroller
        PlayerConroller.Instance.LockInput(false);
        PlayerConroller.Instance.SelfMover.ResumeSpeed();

        // Destroy current tutorial gameobject
        Destroy(CurrentSetp.gameObject);
    }

    private IEnumerator ForceCompleteCurrentStep()
    {
        yield return new WaitForSeconds(CurrentSetp.ForceCompleteTime);
        CompleteCurrentStep();
    }
}
