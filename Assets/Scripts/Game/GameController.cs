using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /***********   GameController Instance ************* */
    public static GameController Instance { get; private set; }
    GameController()
    {
        Instance = this;
    }

    /***********   Surrounding Spawn and Destroy  ************* */
    [System.Serializable]
    public class BuildingSetting
    {
        // Surrounding & UnderGround Prefab
        public GameObject SurroundingPrefab;
        public GameObject UnderGroundPrefab;

        // Pointers
        [System.NonSerialized]
        public GameObject PreviousBuilding = null;
        public GameObject CurrentBuilding;

        // Spawn next building position
        [System.NonSerialized]
        public Vector3 SpawnBuildingPosition = Vector3.zero;
    }
    public BuildingSetting Building;

    /***********   CsvParser  ************* */
    public CsvParser ChildCsvParser;

    /***********   Music Player  ************* */
    public MusicPlayer ChildMusicPlayer;

    /***********   Spawn Worker  ************* */
    public SpawnWorker ChildSpawnWorker;


    private void Awake()
    {
        GameBeginTime = Time.time;

        ChildCsvParser.PrepareForWork();

        UIController.Instance.ShowUIGame();

       // MyServerManager.GameConnectToServer();
    }


    /***********   Spawn and Destroy  ************* */
    public void ChangeBuilding()
    {
        // We destroy old one first, then we spawn a new one
        DestroyOldBuilding();
        SpawnNewBuilding();
    }

    private void SpawnNewBuilding()
    {
        ChildCsvParser.WorkForRandomMap();

        // Spawn new surrounding
        Building.PreviousBuilding = Building.CurrentBuilding;
        Building.CurrentBuilding = ChildSpawnWorker.SpawnBuilding(ChildCsvParser.BuildingName, Building.SpawnBuildingPosition);

        // Spawn new goods
        ChildSpawnWorker.SpawnGoods(ChildCsvParser.List_CurrentGoods, Building.CurrentBuilding.transform);
     }

    private void DestroyOldBuilding()
    {
        if (Building.PreviousBuilding)
        {
            Destroy(Building.PreviousBuilding);
        }
    }


    /***********   Music Player  ************* */
    public void PlayMusic(string MusicName)
    {
        ChildMusicPlayer.PlayMusic(MusicName);
    }

    /***********   Game Controller  ************* */
    public float EndGameDelay = 6f;
    private float GameBeginTime = 0;
    private float GameEndTime = 0;

    public void GameOver()
    {
        print("GameOver");
        //UIController.Instance.ShowUIKeyBoard();
        UIController.Instance.AddNewRank("");
        UIController.Instance.ShowUIRank();
        ZombieFollowerController.Instance.Stop();
        StartCoroutine(EndGameWithDelay());

        GameEndTime = Time.time;
        PlayerLog.Instance.WriteLog((GameEndTime - GameBeginTime).ToString());
        PlayerLog.Instance.WriteLog(PlayerConroller.Instance.CurrentScore.ToString());
    }

    private IEnumerator EndGameWithDelay()
    {
        yield return new WaitForSeconds(EndGameDelay);
      //  MyServerManager.EndGame();
    }
}
