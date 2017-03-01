using System.Collections.Generic;
using UnityEngine;

public class SpawnWorker : MonoBehaviour
{
    public GameObject SpawnBuilding(string BuildingName, Vector3 Position)
    {
        GameObject b = Instantiate(Resources.Load(BuildingName), Position, Quaternion.identity) as GameObject;

        if(b == null)
        {
            print("null");
        }
        return b;
    }

    public void SpawnGoods(List<Goods> List_Goods, Transform Parent)
    {
        foreach (Goods Each in List_Goods)
        {
            GameObject NewGoodsPrefab = Resources.Load(Each.SelfName) as GameObject;
            GameObject NewGoods = Instantiate(NewGoodsPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            NewGoods.transform.parent = Parent;
            NewGoods.transform.localPosition = Each.SelfPosition;
            NewGoods.transform.localRotation = NewGoodsPrefab.transform.rotation;
            NewGoods.transform.localScale = NewGoodsPrefab.transform.localScale;
        }
    }
}
