using UnityEngine;
using UnityEngine.UI;

public class AimPoint : MonoBehaviour
{
    public Image Target;

    private Color TargetOriginColor;

    void Start()
    {
        TargetOriginColor = Target.color;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Config.TTipJump) && PlayerConroller.Instance.IsGrounded())
        {
            PlayerConroller.Instance.ReceiveHMDJump = true;

            if(TutorialController.Instance.InTutorial)
            {
                TutorialController.Instance.ReceiveHMDJump = true;
            }

            Target.color += new Color(0.2f,0.2f,0.2f);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Config.TTipJump))
        {
            Target.color = TargetOriginColor;
        }
    }
}
