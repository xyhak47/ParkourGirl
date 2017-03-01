using UnityEngine;

public class Mover : MonoBehaviour
{
    public enum Direction { Up, Forward, Right, Null }
    public Direction WitchDirection = Direction.Null;
    public float CurrentSpeed = 0;
    private float PreviousSpeed;
    private Rigidbody SelfRigidbody;
    private Vector3 WhichWay;

    void Start()
    {
        SelfRigidbody = GetComponent<Rigidbody>();
        ResetSpeed(CurrentSpeed);
    }

    void FixedUpdate()
    {
        gameObject.transform.position += WhichWay;
    }

    //public void ResetSpeed(float ResetSpeed)
    //{
    //    if (WitchDirection == Direction.Up)
    //    {
    //        SelfRigidbody.velocity = transform.up * ResetSpeed;
    //    }
    //    else if (WitchDirection == Direction.Forward)
    //    {
    //         SelfRigidbody.velocity = transform.forward * ResetSpeed;
    //    }
    //    else if (WitchDirection == Direction.Right)
    //    {
    //         SelfRigidbody.velocity = transform.right * ResetSpeed;
    //    }
    //    else // Do nothing
    //    {
    //    }
    //}

    public void ResetSpeed(float ResetSpeed)
    {
        if (WitchDirection == Direction.Up)
        {
            WhichWay = Vector3.up;
        }
        else if (WitchDirection == Direction.Forward)
        {
            WhichWay = Vector3.forward;
        }
        else if (WitchDirection == Direction.Right)
        {
            WhichWay = Vector3.right;
        }
        else { } // Do nothing

        PreviousSpeed = CurrentSpeed;
        CurrentSpeed = ResetSpeed;
        WhichWay *= CurrentSpeed / 100.0f;
    }

    public void ResumeSpeed()
    {
        ResetSpeed(PreviousSpeed);
       // print("ResumeSpeed CurrentSpeed:" + CurrentSpeed);
    }
}
