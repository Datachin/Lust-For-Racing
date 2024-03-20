using UnityEngine;

public class CarLoader : MonoBehaviour
{
    public Car selectedCar { get; private set; }

    private void Awake()
    {
        // Download the name of the selected car from PlayerPrefs
        string selectedCarName = PlayerPrefs.GetString("SelectedCar", "My First Car");

        // We download the corresponding ScriptableObject of the car
        selectedCar = Resources.Load<Car>(selectedCarName);

        // We install the car model
        Instantiate(selectedCar.carModel, transform.position, transform.rotation, transform);
    }
}
