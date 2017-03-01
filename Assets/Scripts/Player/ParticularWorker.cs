using System.Collections.Generic;
using UnityEngine;

public class ParticularWorker : MonoBehaviour
{
    [System.Serializable]
    public class ParticluarPerfab
    {
        public GameObject ParticularPerfab;
        public string Name;
    }

    public List<ParticluarPerfab> List_ParticluarPerfab;
    public Vector3 Offset;

    public GameObject SpawnParticularPerfab(string Name)
    {
        // Spawn
        GameObject ParticularPerfab = List_ParticluarPerfab.Find(it => it.Name == Name).ParticularPerfab;
        if(ParticularPerfab)
        { 
            GameObject Particular = (GameObject)Instantiate(ParticularPerfab, Vector3.zero, Quaternion.identity);

            // Add to parent
            Particular.transform.parent = PlayerConroller.Instance.transform;
            Particular.transform.position = PlayerConroller.Instance.transform.position + Offset;

            return Particular;
        }

        return null;
    }
}
