using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Statistic : MonoBehaviour
{
    public TextMeshProUGUI[] top5Texts;
    public TextMeshProUGUI totalDistanceText;
    public TextMeshProUGUI totalObstaclesText;
    public TextMeshProUGUI totalFuelText;
    public TextMeshProUGUI totalBucksText;

    private void Update()
    {
        if (GameStats.Instance != null)
        {
            List<float> top5Distances = GameStats.Instance.GetTopDistances();
            for (int i = 0; i < top5Distances.Count; i++)
            {
                top5Texts[i].text = " " + top5Distances[i].ToString("F1") + " km";
            }

            totalDistanceText.text = " " + GameStats.Instance.GetTotalDistance().ToString("F1") + " km";
            totalObstaclesText.text = " " + GameStats.Instance.GetTotalObstacles().ToString();
            totalFuelText.text = " " + GameStats.Instance.GetTotalFuel().ToString();
            totalBucksText.text = " " + GameStats.Instance.GetTotalBucks().ToString(); 
        }
    }

    public void UpdateDisplay()
    {
        List<float> top5Distances = GameStats.Instance.GetTopDistances();
        for (int i = 0; i < top5Texts.Length; i++)
        {
            if (i < top5Distances.Count)
            {
                top5Texts[i].text = " " + " " + top5Distances[i].ToString("F1") + " km";
            }
            else
            {
                top5Texts[i].text = " " + "0 km";
            }
        }

        totalDistanceText.text = " " + GameStats.Instance.GetTotalDistance().ToString("F1") + " km";
        totalObstaclesText.text = " " + GameStats.Instance.GetTotalObstacles().ToString();
        totalFuelText.text = " " + GameStats.Instance.GetTotalFuel().ToString();
        totalBucksText.text = " " + GameStats.Instance.GetTotalBucks().ToString(); 
    }
}
