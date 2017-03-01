using UnityEngine;

public class TutorialStep : MonoBehaviour
{
    public enum Step
    {
        TurnLeft, TurnRight, Jump, Watching
    }

    public Step Which;
    public GameObject ChildAnimation;

    private string[] StepName = { Config.TTRTurnLeft, Config.TTRTurnRight, Config.TTRJump, Config.TTRWatching };

    public float ForceCompleteTime = 6;

    void Awake()
    {
        ChildAnimation.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Config.TPlayer))
        {
            // Cause we will transform parent, make it disable or it will trigger for many times
            GetComponent<BoxCollider>().enabled = false;
            string CurrentStepName = StepName[(int)Which];

            this.gameObject.name = CurrentStepName;
            TutorialController.Instance.EnterStep(this);

            ChildAnimation.SetActive(true);
            ChildAnimation.GetComponent<Animator>().SetTrigger(CurrentStepName);
        }
    }  
}
