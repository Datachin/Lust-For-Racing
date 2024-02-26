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

        // Update the maximum distance for the current map
        int currentMapIndex = PlayerPrefs.GetInt("CurrentMapIndex"); //get the index of the selected location
        Map currentMap = ScriptableObjectsController.Instance.GetMapByIndex(currentMapIndex); // get a map of the object by index
        if (distance > currentMap.maxDistance)
        {
            currentMap.maxDistance = distance;
            
            PlayerPrefs.SetFloat(currentMap.mapName + "MaxDistance", currentMap.maxDistance);
            PlayerPrefs.Save(); // Save the changes
        }

        gameOverPanel.SetActive(true);
        Time.timeScale = 0; 
    }


}