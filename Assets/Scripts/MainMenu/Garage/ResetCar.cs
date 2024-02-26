using UnityEngine;

public class CarReset : MonoBehaviour
{
    // Add the names of all cars here
    private string[] carNames = { "Simple car", "Mini bus" };

    public void ResetCars()
    {
        foreach (string carName in carNames)
        {
            PlayerPrefs.DeleteKey(carName);
        }
    }
}
