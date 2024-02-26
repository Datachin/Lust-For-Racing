using UnityEngine;
using UnityEngine.UI;

public class ResetStatsButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ResetStats);
    }

    void ResetStats()
    {
        GameStats.Instance.ResetAllStats();
        FindObjectOfType<Statistic>().UpdateDisplay();
    }
}
