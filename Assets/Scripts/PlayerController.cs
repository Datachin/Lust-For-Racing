using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 move;
    public float forwardSpeed;
    private float originalSpeed;
    private int desiredLane = 1;
    public float laneDistance = 2.5f; 
    float lerpSpeed = 17f;

    private FuelLevel fuelLevel; 

    private bool isSpeedReduced = false;
    private float speedReductionTimer = 0f;

    private Coroutine speedReductionCoroutine;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalSpeed = forwardSpeed;

        
        fuelLevel = FindObjectOfType<FuelLevel>();

        
        fuelLevel.OnFuelEmpty += FuelOver;
    }

    void Update()
    {
        if (isSpeedReduced)
        {
            
            speedReductionTimer -= Time.deltaTime;

           
            if (speedReductionTimer <= 0f)
            {
                
                RestoreSpeed();
            }
        }

        move.z = forwardSpeed;


        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane == 2)
                desiredLane = 1;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDistance;

    
        if (transform.position != targetPosition)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
            controller.Move(newPos - transform.position);
        }

       
        if (Mathf.Approximately(forwardSpeed, 0f) && !isSpeedReduced)
        {
            
            PlayerManager.gameOver = true;
        }

        controller.Move(move * Time.deltaTime);

        
        if (fuelLevel.CurrentFuel > 0)
        {
            PickUpFuel();
        }
    }

    private void FixedUpdate()
    {
        
    }

    
    public void ApplySlowdown(float newSpeed, float duration)
    {
        isSpeedReduced = true;
        forwardSpeed = newSpeed;
        speedReductionTimer = duration;
    }

    
    private void RestoreSpeed()
    {
        isSpeedReduced = false;
        forwardSpeed = originalSpeed;
    }

    
    IEnumerator ReduceSpeedOverTime(float targetSpeed, float duration)
    {
        float initialSpeed = forwardSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            forwardSpeed = Mathf.Lerp(initialSpeed, targetSpeed, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        
        forwardSpeed = targetSpeed;

    }

    public void FuelOver()
    {
        Debug.Log("Fuel is over!");

    
        speedReductionCoroutine = StartCoroutine(ReduceSpeedOverTime(0f, 10f));
    }

    void PickUpFuel()
    {
        if (speedReductionCoroutine != null)
        {
            
            StopCoroutine(speedReductionCoroutine);
            speedReductionCoroutine = null;

            
            RestoreSpeed();
        }

    }

}
