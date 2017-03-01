using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class UIController : MonoBehaviour
{
    /***********   UIController  ************* */
    public static UIController Instance;
    UIController()
    {
        Instance = this;
    }

    private void Update()
    {
        //transform.rotation = Camera.main.transform.rotation;
    }

    public void ShowUIRank()
    {
        UIInGame.gameObject.active = false;
        UIInRank.gameObject.active = true;
        UIKeyBoard.gameObject.active = false;
    }

    public void ShowUIGame()
    {
        UIInGame.gameObject.active = true;
        UIInRank.gameObject.active = false;
        UIKeyBoard.gameObject.active = false;
    }

    public void ShowUIKeyBoard()
    {
        UIInGame.gameObject.active = false;
        UIInRank.gameObject.active = false;
        UIKeyBoard.gameObject.active = true;
    }

    /***********   UI  ************* */
    public UIGame UIInGame;
    private int CurrentScore = 0;
    private int CurrentDistance = 0;

    public void CalculateScoreToUI(int TotalScore)
    {
        CurrentScore = TotalScore;
        UIInGame.Score.text = Config.CScore + TotalScore.ToString();
    }

    public void CalculateDistanceToUI(int TotalDistance)
    {
        CurrentDistance = TotalDistance;
        UIInGame.Distance.text = Config.CDistance + TotalDistance.ToString();
    }

    public void CalculateBloodToUI(float Percent)
    {
        UIInGame.Blood.fillAmount = Percent;
    }

    public void CalculatePowerToUI(float Percent)
    {
        UIInGame.Power.fillAmount = Percent;
    }

    public void Invincible(bool Begin)
    {
        UIInGame.Invincible(Begin);
    }



    /***********   UI Rank  ************* */
    public UIRank UIInRank;
    private List<RankInfo> List_RankInfo; 
    private string RankDataKey = "RankData";
    public int RankStoreLimit = 100;

    private void Start()
    {
        RecoverRankInfoFromLocalRankData();
        CalculateScoreToUI(0);
        CalculateDistanceToUI(0);
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.C))
    //    {
    //        PlayerPrefs.SetString(RankDataKey, "");
    //        PlayerPrefs.Save();
    //    }
    //}

    public void AddNewRank(string PlayerName)
    {
        RankInfo NewRank = new RankInfo();
        NewRank.SetRankData(0, PlayerName, CurrentScore, CurrentDistance);
        List_RankInfo.Add(NewRank);
        ResetRankData();

        UIInRank.SetRankData(List_RankInfo);
        UIInRank.SetPlayerRankData(NewRank);
    }

    private void ResetRankData()
    {
        // Keep count in RankStoreLimit
        while (List_RankInfo.Count > RankStoreLimit)
        {
            List_RankInfo.RemoveAt(List_RankInfo.Count - 1);
        }

        // Sort: first compare score, then compare distance
        List_RankInfo.Sort(
            (Left, Right) =>
            {
                // Inverted order
                int ScoreCompare = Right.Score - Left.Score;
                int DistanceCompare = Right.Distance - Left.Distance;
                return ScoreCompare == 0 ? DistanceCompare : ScoreCompare;
            }
        );

        // Set ranking value
        for (int i = 0; i < List_RankInfo.Count; i++)
        {
            List_RankInfo[i].Ranking = i + 1;
        }

        // Save
        SaveRankData();
    }

    private void SaveRankData()
    {
        PlayerPrefs.SetString(RankDataKey, GetRankData());
        PlayerPrefs.Save();
    }

    private string GetRankData()
    {
        string RankData = "";

        foreach (RankInfo Each in List_RankInfo)
        {
            RankData += Each.GetRankData();
        }

        return RankData;
    }

    private void RecoverRankInfoFromLocalRankData()
    {
        string LocalRankData = PlayerPrefs.GetString(RankDataKey);
        string[] RankDatas = LocalRankData.Split(';');
        List_RankInfo = new List<RankInfo>(RankDatas.Length);

        for (int i = 0; i < RankDatas.Length; i++)
        {
            string OneRank = RankDatas[i];
            RankInfo Rank = RankInfo.ParseToRankInfo(OneRank);

            if(Rank != null)
            {
                List_RankInfo.Add(Rank);
            }
        }
    }


    /*  ********* UI KeyBoard********/
    public GameObject UIKeyBoard;
}

public class RankInfo
{
    public int Ranking;
    public string Name;
    public int Score;
    public int Distance;

    public static RankInfo ParseToRankInfo(string RankData)
    {
        if(RankData == "")
        {
            return null;
        }

        RankInfo NewRankInfo = new RankInfo();
        string[] Rank = RankData.Split(',');
        NewRankInfo.SetRankData(int.Parse(Rank[0]), Rank[1], int.Parse(Rank[2]), int.Parse(Rank[3]));
        return NewRankInfo;
    }

    public void SetRankData(int TempRanking, string TempName, int TempScore, int TempDistance)
    {
        Ranking = TempRanking;
        Name = TempName;
        Score = TempScore;
        Distance = TempDistance;
    }

    public string GetRankData()
    {
        return Ranking.ToString() + "," + Name + "," + Score.ToString() + "," + Distance.ToString() + ";";
    }
}