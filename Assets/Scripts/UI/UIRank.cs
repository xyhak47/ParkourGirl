using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRank : MonoBehaviour
{
    [System.Serializable]
    public class PlayerRank
    {
        public Text PlayerRanking;
        public Text PlayerScore;
        public Text PlayerDistance;
    }
    public PlayerRank ChildPlayerRank;

    public List<RankNode> List_RankNode;

    [SerializeField]
    private Text PlayerRankWord;

    void Start()
    {
        PlayerRankWord.text = Config.WRankPlayer;
    }

    public void SetRankData(List<RankInfo> List_RankInfo)
    {
        int RanksCount = Mathf.Clamp(List_RankInfo.Count, 0, List_RankNode.Count);
        for(int i = 0; i < RanksCount; i++)
        {
            RankInfo Rank = List_RankInfo[i];
            List_RankNode[i].gameObject.active = true;
            List_RankNode[i].SelfName.text = "player"; //Rank.Name;
            List_RankNode[i].SelfScore.text = Rank.Score + Config.CPoint;
            List_RankNode[i].SelfDistance.text = Rank.Distance + Config.CMeter;
        }

        // Hide the unuse
        for (int i = RanksCount; i < List_RankNode.Count; i++)
        {
            List_RankNode[i].gameObject.active = false;
        }
    }

    public void SetPlayerRankData(RankInfo PlayerRankInfo)
    {
        ChildPlayerRank.PlayerRanking.text = PlayerRankInfo.Ranking.ToString();
        ChildPlayerRank.PlayerScore.text = PlayerRankInfo.Score + Config.CPoint;
        ChildPlayerRank.PlayerDistance.text = PlayerRankInfo.Distance + Config.CMeter;
    }
}
