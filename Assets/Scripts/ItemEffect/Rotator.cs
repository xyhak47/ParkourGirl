using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float RotateSpeed;
    public Vector3 Direction = Vector3.up;

	void Start ()
    {
        GetComponent<Rigidbody>().angularVelocity = Direction * RotateSpeed;
    }
}
