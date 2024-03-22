using UnityEngine;

public class ScriptableObjectChanger : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] scriptableObjects;

    [Header ("Display Scripts")]
    
    [SerializeField] private CarDisplay carDisplay;

    private int currentCarIndex;

    //animation
    [SerializeField] private Vector3 finalPosition;
    private Vector3 initialPosition;
    private float rotatioinYSpeed = 10;

    private void Awake()
    {
        ChangeСar(0);
        initialPosition = transform.position; //animation
    }

    private void Update() //animation
    {
        transform.position = Vector3.Lerp(transform.position, finalPosition, 0.1f);
        transform.RotateAround(gameObject.transform.position, Vector3.up, 20 * Time.deltaTime);
        transform.Rotate(0,rotatioinYSpeed*Time.deltaTime,0);
    }

    private void OnDisable() //animation
    {
        transform.position = initialPosition;
    }

    public void ChangeСar(int _index)
    {
        currentCarIndex += _index;
        if (currentCarIndex < 0) currentCarIndex = scriptableObjects.Length - 1;
        if (currentCarIndex > scriptableObjects.Length - 1) currentCarIndex = 0;
        
        if(carDisplay != null) carDisplay.UpdateCar((Car)scriptableObjects[currentCarIndex]);

    }

    private void OnEnable()
    {
        ChangeСar(0);
        initialPosition = transform.position; //animation
    }
}
