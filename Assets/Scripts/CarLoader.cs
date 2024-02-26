using UnityEngine;

public class CarLoader : MonoBehaviour
{
    private void Start()
    {
        // load the name of the selected car from PlayerPrefs
        string selectedCarName = PlayerPrefs.GetString("SelectedCar", "My First Car");

        // download the corresponding Scriptable Object of the car
        Car selectedCar = Resources.Load<Car>(selectedCarName);

        // Install the car model
        Instantiate(selectedCar.carModel, transform.position, transform.rotation, transform);
    }
}
