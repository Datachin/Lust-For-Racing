using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_L : MonoBehaviour
{
    public float moveSpeed = 4.0f;

    private void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // The player encountered an obstacle
            if (GameStats.Instance != null)
            {
                GameStats.Instance.UpdateObstacles(1);
            }

            // Access the PlayerController script
            PlayerController playerController = other.GetComponent<PlayerController>();

            // Check if the script was found
            if (playerController != null)
            {
                // Call the DamageCar() method
                playerController.DamageCar();
            }
            else
            {
                Debug.LogError("PlayerController script not found on Player object.");
            }
        }
    }
}