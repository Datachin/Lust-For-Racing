using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarDisplay : MonoBehaviour
{
    [Header("Description")]
    [SerializeField] private TextMeshProUGUI carName;
    [SerializeField] private TextMeshProUGUI carPrice;

    [Header("Stats")]
    [SerializeField] private Transform speedStars;
    [SerializeField] private Transform accelerationStars;
    [SerializeField] private Transform strengthStars;

    [Header("Car Model")]
    [SerializeField] private GameObject carModel;

    [Header("Buy Button")]
    [SerializeField] private Button buyButton;

    [Header("Go Button")]
    [SerializeField] private Button goButton;

    private Car currentCar;
    public static CarDisplay Instance { get; private set; }
    public Car[] cars;
    
    private void Awake()
    {
        
        if (Instance == null)
        {    
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        buyButton.onClick.AddListener(() =>
        {
            int carState = PlayerPrefs.GetInt(currentCar.carName, 0);
            if (carState == 0)
            {
                BuyCar();
            }
            else
            {
                SelectCar();
            }
        });

        // Add this line to disable the Go button at the start
        goButton.interactable = false;

        // Check if it's the first launch of the game
        if (PlayerPrefs.GetInt("IsFirstLaunch", 1) == 1)
        {
            // If it's the first launch, set the state of "My First Car" as bought
            PlayerPrefs.SetInt("My First Car", 1);
            // Set "IsFirstLaunch" to 0 to indicate that the game has been launched before
            PlayerPrefs.SetInt("IsFirstLaunch", 0);
        }

        UpdateCarDisplay();

        goButton.onClick.AddListener(GoButtonPressed);
    }


    public void UpdateCar(Car _newCar)
    {
        currentCar = _newCar;
        carName.text = _newCar.carName;
        carPrice.text = _newCar.carPrice + "$";

        for (int i = 0; i < speedStars.childCount; i++)
            speedStars.GetChild(i).gameObject.SetActive(i < _newCar.speed);

        for (int i = 0; i < accelerationStars.childCount; i++)
            accelerationStars.GetChild(i).gameObject.SetActive(i < _newCar.acceleration);

        for (int i = 0; i < strengthStars.childCount; i++)
            strengthStars.GetChild(i).gameObject.SetActive(i < _newCar.strength);

        if (carModel.transform.childCount > 0)
            Destroy(carModel.transform.GetChild(0).gameObject);
        Instantiate(_newCar.carModel, carModel.transform.position, carModel.transform.rotation, carModel.transform);

        UpdateCarDisplay();
    }

    public void BuyCar()
    {
        if (GameStats.Instance.BuyCar(currentCar))
        {
            PlayerPrefs.SetInt(currentCar.carName, 1);
            UpdateCarDisplay();
        }
        else
        {
            Debug.Log("Not enough bucks to buy this car.");
        }
    }

    public void SelectCar()
    {
        PlayerPrefs.SetString("SelectedCar", currentCar.carName);
        PlayerPrefs.Save();
    }


    public void UpdateCarDisplay()
    {
        int carState = PlayerPrefs.GetInt(currentCar.carName, 0);
        if (carState == 0)
        {
            // The car is not bought
            // Check if the player has enough money to buy the car
            if (GameStats.Instance.GetTotalBucks() >= currentCar.carPrice)
            {
                // If the player has enough money, set the button color to green
                buyButton.GetComponent<Image>().color = Color.green;
            }
            else
            {
                // If the player does not have enough money, set the button color to red
                buyButton.GetComponent<Image>().color = Color.red;
            }
            carPrice.text = currentCar.carPrice + "$";

            // Disable the Go button if the car is not bought
            goButton.interactable = false;
        }
        else
        {
            // The car is bought
            buyButton.GetComponent<Image>().color = Color.green;
            carPrice.text = "bought";

            // Enable the Go button if the car is bought
            goButton.interactable = true;
        }
    }

    public Car FindCarByName(string carName)
    {
        // Iterate over all cars
        foreach (Car car in cars)
        {
            // If the car's name matches carName, return the car
            if (car.carName == carName)
            {
                return car;
            }
        }

        // If no car is found, return null
        return null;
    }

    public void GoButtonPressed()
    {
        if (goButton.interactable == true)
        {
            PlayerPrefs.SetString("SelectedCar", currentCar.carName);
            PlayerPrefs.Save();
        }
    }

}
