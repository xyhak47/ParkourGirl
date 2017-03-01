using UnityEngine;

public class Follower : MonoBehaviour
{
    public GameObject TargetToFollow;
    public Vector3 Offset;
    public enum Locking { X, Y, Z, Null }
    public Locking SelfLocking;
    public float SmoothTime = 0.05f;

    private Vector3 TargetOriginPosition;
    private Vector3 SmoothVelocity = Vector3.zero;

    void Start()
    {
        TargetOriginPosition = TargetToFollow.transform.position;
        transform.position = TargetOriginPosition + Offset;
    }
	
	void LateUpdate ()
    {
        Vector3 NewPosition = transform.position;
        if (SelfLocking == Locking.X)
        {
            NewPosition = new Vector3(TargetOriginPosition.x, TargetToFollow.transform.position.y, TargetToFollow.transform.position.z);
        }
        else if (SelfLocking == Locking.Y)
        {
            NewPosition = new Vector3(TargetToFollow.transform.position.x, TargetOriginPosition.y, TargetToFollow.transform.position.z);
        }
        else if (SelfLocking == Locking.Z)
        {
            NewPosition = new Vector3(TargetToFollow.transform.position.x, TargetToFollow.transform.position.x, TargetOriginPosition.z);
        }
        else if (SelfLocking == Locking.Null)
        {
            NewPosition = TargetToFollow.transform.position;
        }
        else { } // Do nothing

        NewPosition += Offset;
        transform.position = NewPosition;
        //transform.position = Vector3.SmoothDamp(transform.position, NewPosition, ref SmoothVelocity, SmoothTime);
    }
}
