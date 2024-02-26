using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameStats.Instance != null)
            {
                GameStats.Instance.UpdateObstacles(1);
            } 
            PlayerManager.gameOver = true;
        }
    }
}
