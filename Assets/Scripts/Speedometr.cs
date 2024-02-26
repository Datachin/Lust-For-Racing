using TMPro;
using UnityEngine;

public class Speedometr : MonoBehaviour
{
    public TextMeshProUGUI speedText;    
    public TextMeshProUGUI distanceText; 
    public Transform player;              

    private Vector3 lastPosition;
    private float distanceTraveled;
    private float currentSpeedKilometersPerHour;  
    private float targetSpeedKilometersPerHour;   
    private float speedChangeSpeed = 1.0f;        

    private void Start()
    {
        lastPosition = player.position;
        currentSpeedKilometersPerHour = 0f;  
        distanceTraveled = PlayerPrefs.GetFloat("AllDistance", 0);
    }

    private void Update()
    {
        float distanceDelta = player.position.z - lastPosition.z;
        float timeDelta = Time.deltaTime;

        if (timeDelta > 0f)
        {
            float speedMetersPerSecond = distanceDelta / timeDelta;
            float speedKilometersPerHour = speedMetersPerSecond * 5f; 
            currentSpeedKilometersPerHour = Mathf.Lerp(currentSpeedKilometersPerHour, speedKilometersPerHour, speedChangeSpeed * timeDelta);

            distanceTraveled += distanceDelta;
            float distanceKilometers = distanceTraveled / 200f;
            
            speedText.text = " " + currentSpeedKilometersPerHour.ToString("F1") + " km/h";
            distanceText.text = " " + distanceKilometers.ToString("F1") + " km";
        }
        else
        {
            currentSpeedKilometersPerHour = 0f;
            speedText.text = " 0.0 km/h"; 
        }

        if (player.GetComponent<CharacterController>().isGrounded && player.GetComponent<CharacterController>().velocity.magnitude <= 0.01f)
        {
            currentSpeedKilometersPerHour = 0f;
        }

        lastPosition = player.position;
    }

    public float GetDistance()
    {
        return distanceTraveled / 200f;
    }

    public void SaveDistance()
    {
        PlayerPrefs.SetFloat("AllDistance", GetDistance());
        PlayerPrefs.Save();
    }
}
