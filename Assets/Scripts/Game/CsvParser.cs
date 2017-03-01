using System.Collections.Generic;
using UnityEngine;

public class CsvParser : MonoBehaviour
{
    /****************************** test *******************************/
    static public CsvParser Instance = null;
    CsvParser()
    {
        Instance = this;
    }

    public  string getname()
    {
        return MapFileForUsing.name;
    }
    /*************************************************************/




    /****************************** CsvParser *******************************/
    public void PrepareForWork()
    {
        ParseDataFromCsvItem();
        ParseDataFromCsvMapIndex();
    }

    // Work each time, provide goods from one map, Random
    public void WorkForRandomMap()
    {
       // print("CurrentMapIndex = " + CurrentMapIndex);
       // print("List_MapIndex count = " + List_MapIndex.Count);

        MapIndex Index = List_MapIndex[CurrentMapIndex++];
        CurrentMapIndex = Mathf.Clamp(CurrentMapIndex, 0, List_MapIndex.Count - 1);
        List<TextAsset> List_MapFile = List_RandomMap.Find(it => it.Level == Index.SelfLevel).List_CsvMapWithLevel;
        MapFileForUsing = List_MapFile[Random.Range(0, List_MapFile.Count - 1)];
        ParseDataFromCsvMap();

       // print(MapFileForUsing.name);
    }


    /***************************** Table Item ****************************/
    public TextAsset CsvItem;
    private List<Item> List_Item = new List<Item>();

    void ParseDataFromCsvItem()
    {
        string[] Items = CsvItem.text.Replace("\r", "").Split('\n');

        for (int i = 0; i < Items.Length; i++)
        {
            string EachLine = Items[i];
            string[] DataArr = EachLine.Split(',');

            if (DataArr[0] == Config.TableItem)
            {
                Item NewItem = new Item(DataArr);
                List_Item.Add(NewItem);
            }
        }
    }


    /***************************** Table Map ****************************/
    public string BuildingName { get; private set; }
    private TextAsset MapFileForUsing;
    public List<Goods> List_CurrentGoods = new List<Goods>();

    [System.Serializable]
    public class RandomMap
    {
        public List<TextAsset> List_CsvMapWithLevel;
        public int Level = 0;
    }
    public List<RandomMap> List_RandomMap;

    void ParseDataFromCsvMap()
    {
        List_CurrentGoods.Clear();

        string[] CurrentGoods = MapFileForUsing.text.Replace("\r", "").Split('\n');

        for (int i = 0; i < CurrentGoods.Length; i++)
        {
            string EachLine = CurrentGoods[i];
            string[] DataArr = EachLine.Split(',');

            // BuildingName decides what kind of building will be used
            if (DataArr[0] == Config.TableBuilding)
            {
                BuildingName = DataArr[1];
            }

            if (DataArr[0] == Config.TableGoods)
            {
                Goods NewItem = new Goods(DataArr, List_Item);
                List_CurrentGoods.Add(NewItem);
            }
        }
    }


    /***************************** Table Map Index ****************************/
    private int CurrentMapIndex = 0;
    private List<MapIndex> List_MapIndex = new List<MapIndex>();
    public TextAsset CsvMapIndex;

    void ParseDataFromCsvMapIndex()
    {
        string[] Indexs = CsvMapIndex.text.Replace("\r", "").Split('\n');

        for (int i = 0; i < Indexs.Length; i++)
        {
            string EachLine = Indexs[i];
            string[] DataArr = EachLine.Split(',');

            if (DataArr[0] == Config.TableMapIndex)
            {
                MapIndex NewMapIndex = new MapIndex(DataArr);
                List_MapIndex.Add(NewMapIndex);
            }
        }
    }
}


/***** Class *************************************************************/
public class Base
{
    public int SelfId { get; set; }
}

public class Goods : Base
{
    public int SelfType { get; private set; }
    public string SelfName { get; private set; }
    public Vector3 SelfPosition { get; private set; }

    private int SelfItemId { get; set; }

    public Goods(string[] DataArr, List<Item> List_Item)
    {
        SelfId = int.Parse(DataArr[1]);
        SelfItemId = int.Parse(DataArr[2]);
        SelfPosition = ParsePostion(DataArr[3]);
        SelfName = List_Item.Find(it=>it.SelfId == SelfItemId).SelfName;
    }

    Vector3 ParsePostion(string Positon)
    {
        try { 
            string[] PostionData = Positon.Split(':');
            float X = float.Parse(PostionData[0]);
            float Y = float.Parse(PostionData[1]);
            float Z = float.Parse(PostionData[2]);
            return new Vector3(X, Y, Z);
        }
        catch(System.Exception e)
        {
            Debug.Log(CsvParser.Instance.getname() + "  this map is wrong!!!!!");
        }

        return new Vector3(100f,100f,100f); // far
    }
}

public class Item : Base
{
    public string SelfName { get; private set; }

    public Item(string[] DataArr)
    {
        SelfId = int.Parse(DataArr[1]);
        SelfName = DataArr[2];
    }
}

public class MapIndex : Base
{
    public int SelfLevel { get; private set; }

    public MapIndex(string[] DataArr)
    {
        SelfId = int.Parse(DataArr[1]);
        SelfLevel = int.Parse(DataArr[2]);
    }
}

