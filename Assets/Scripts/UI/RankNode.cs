using UnityEngine;
using UnityEngine.UI;

public class RankNode : MonoBehaviour
{
    public Text SelfName;
    public Text SelfScore;
    public Text SelfDistance;

    public void SetRankData(string Name, string Score, string Distance)
    {
        SelfName.text = Name;
        SelfScore.text = Score;
        SelfDistance.text = Distance;
    }
}
