using UnityEngine;

public class LifeCircle : MonoBehaviour
{
    public Vector3 Offset;

    void Start()
    {
        GameController.Instance.Building.SpawnBuildingPosition.z = (gameObject.transform.position + Offset).z;
    }

    void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag(Config.TPlayer))
        {
             GameController.Instance.ChangeBuilding();

            // test 
            //transform.localScale = new Vector3(3f,3f,3f);
        }
    }
}
