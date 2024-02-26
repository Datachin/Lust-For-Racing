using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance;

    private List<float> topDistances = new List<float>();
    private float totalDistance;
    private int totalObstacles;
    private int totalFuel;
    private float totalBucks; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadStats();
    }

    public bool BuyCar(Car car)
    {
        if (totalBucks >= car.carPrice)
        {
            totalBucks -= car.carPrice;
            SaveStats();
            return true;
        }
        return false;
    }

    public void UpdateDistance(float distance)
    {
        totalDistance += distance;

        // Check if the new distance is a record
        if (topDistances.Count < 5 || distance > topDistances[4])
        {
            // Add the new distance to the list
            topDistances.Add(distance);

            // Sort the list in descending order
            topDistances.Sort();
            topDistances.Reverse();

            // Keep only the top 5 distances
            while (topDistances.Count > 5)
            {
                topDistances.RemoveAt(topDistances.Count - 1);
            }

            SaveStats();
        }
    }

    public void UpdateObstacles(int count)
    {
        totalObstacles += count;
        SaveStats();
    }

    public void UpdateFuel(int count)
    {
        totalFuel += count;
        SaveStats();
    }

    public void UpdateBucks(float amount) 
    {
        totalBucks += amount;
        SaveStats();
    }

    private void SaveStats()
    {
        PlayerPrefs.SetFloat("TotalDistance", totalDistance);
        PlayerPrefs.SetInt("TotalObstacles", totalObstacles);
        PlayerPrefs.SetInt("TotalFuel", totalFuel);
        PlayerPrefs.SetFloat("TotalBucks", totalBucks); 
        for (int i = 0; i < topDistances.Count; i++)
        {
            PlayerPrefs.SetFloat("TopDistance" + i, topDistances[i]);
        }
    }

    private void LoadStats()
    {
        totalDistance = PlayerPrefs.GetFloat("TotalDistance", 0);
        totalObstacles = PlayerPrefs.GetInt("TotalObstacles", 0);
        totalFuel = PlayerPrefs.GetInt("TotalFuel", 0);
        totalBucks = PlayerPrefs.GetFloat("TotalBucks", 0);

        for (int i = 0; i < 5; i++)
        {
            float topDistance = PlayerPrefs.GetFloat("TopDistance" + i, 0);
            if (topDistance > 0)
            {
                topDistances.Add(topDistance);
            }
        }
    }

    public void ResetAllStats()
    {
        totalDistance = 0;
        totalObstacles = 0;
        totalFuel = 0;
        totalBucks = 0; // later delete
        topDistances.Clear();

        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.DeleteKey("TopDistance" + i);
        }

        SaveStats();
        LoadStats();

    }

    public List<float> GetTopDistances()
    {
        return topDistances;
    }

    public float GetTotalDistance()
    {
        return totalDistance;
    }

    public int GetTotalObstacles()
    {
        return totalObstacles;
    }

    public int GetTotalFuel()
    {
        return totalFuel;
    }

    public float GetTotalBucks() 
    {
        return totalBucks;
    }
}