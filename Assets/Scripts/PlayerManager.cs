using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    private bool gameOverTriggered = false;
    public GameObject gameOverPanel;
    public Speedometr speedometr;

    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (gameOver && !gameOverTriggered)
        {
            gameOverTriggered = true;
            ShowGameOverPanel(); 
        }
    }

    public void ShowGameOverPanel()
    {
        float distance = speedometr.GetDistance();

        // Save the last game result
        GameStats.Instance.UpdateDistance(distance);
        speedometr.SaveDistance();

        // Update the maximum distance for the selected map
        int selectedMapIndex = PlayerPrefs.GetInt("CurrentMapIndex");
        Map selectedMap = ScriptableObjectsController.Instance.GetMapByIndex(selectedMapIndex); // get a map of the object by index
        if (distance > selectedMap.maxDistance)
        {
            selectedMap.maxDistance = distance;
            
            PlayerPrefs.SetFloat(selectedMap.mapName + "MaxDistance", selectedMap.maxDistance);
            PlayerPrefs.Save(); // Save the changes
        }

        gameOverPanel.SetActive(true);
        Time.timeScale = 0; 
    }
}