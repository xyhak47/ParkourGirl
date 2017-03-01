using UnityEngine;

public class MoverByCollider : MonoBehaviour
{
    public enum Direction { Up, Forward, Right, Null}
    public Direction WitchDirection = Direction.Null;
    public float Speed;
    private Rigidbody SelfRigidbody;

    void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag(Config.TPlayer))
        {
            SelfRigidbody = GetComponent<Rigidbody>();

            if (WitchDirection == Direction.Up)
            {
                SelfRigidbody.velocity = transform.up * Speed;
            }
            else if (WitchDirection == Direction.Forward)
            {
                SelfRigidbody.velocity = transform.forward * Speed;
            }
            else if (WitchDirection == Direction.Right)
            {
                SelfRigidbody.velocity = transform.right * Speed;
            }
            else // Do nothing
            {
            }
        }
    }

    void OnTriggerExit(Collider Other)
    {
        if (Other.CompareTag(Config.TPlayer))
        {
            Destroy(gameObject);
        }
    }
}
