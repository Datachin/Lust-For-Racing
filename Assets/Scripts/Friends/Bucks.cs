using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucks : MonoBehaviour
{
    public float bucksAmount = 25f;

    private void Update() //animation
    {
        transform.RotateAround(gameObject.transform.position, Vector3.up, 50 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // pick up Bucks
            if (GameStats.Instance != null)
            {
                GameStats.Instance.UpdateBucks(bucksAmount);
            }
            Debug.Log("Bucks added: " + bucksAmount);
                
            Destroy(gameObject);
                      
        }
    }
}
