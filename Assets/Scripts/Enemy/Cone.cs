using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                    // Гравець зіткнувся з перешкодою
                if (GameStats.Instance != null)
                {
                    GameStats.Instance.UpdateObstacles(1);
                }
                
                playerController.ApplySlowdown(playerController.forwardSpeed / 2f, 2.0f);
            }
        }
    }
}
