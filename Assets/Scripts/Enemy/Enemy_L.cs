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
            // Гравець зіткнувся з перешкодою
            if (GameStats.Instance != null)
            {
                GameStats.Instance.UpdateObstacles(1);
            }

            PlayerManager.gameOver = true;
        }
    }
}
