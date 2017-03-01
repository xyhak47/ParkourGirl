using UnityEngine;

public class GainScore : MonoBehaviour
{
    public int ScoreValue = 0;

    void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag(Config.TPlayer))
        {
            PlayerConroller.Instance.GainScore(ScoreValue);
            GameController.Instance.PlayMusic(Config.MCoin);
            PlayerConroller.Instance.SpawnParticular(Config.PGainScore);
            Destroy(gameObject);
        }
    }
}
