using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    public float fuelAmount = 25f;

    private void Update() //animation
    {
        transform.RotateAround(gameObject.transform.position, Vector3.up, 50 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            FuelLevel fuelLevel = other.GetComponent<FuelLevel>();

            if (fuelLevel != null)
            {
                if (GameStats.Instance != null)
                {
                GameStats.Instance.UpdateFuel(1);
                }

                fuelLevel.AddFuel(fuelAmount);
                Debug.Log("Fuel added: " + fuelAmount);
                
                Destroy(gameObject);
            }
            
        }
    }
}