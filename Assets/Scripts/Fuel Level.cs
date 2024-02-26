using UnityEngine;
using UnityEngine.UI;

public class FuelLevel : MonoBehaviour
{
    public Slider fuelSlider;
    public Image fillImage;
    public float maxFuel = 100f;
    public float fuelConsumptionRate = 5f;

    private float currentFuel;

    public float CurrentFuel
    {
        get { return currentFuel; }
    }

    // ������� ���� ��� ��������� ��� ��������� ������
    public delegate void FuelEmpty();
    public event FuelEmpty OnFuelEmpty;

    private void Start()
    {
        currentFuel = maxFuel;
        UpdateFuelSlider();
        UpdateFuelColor();
    }

    private void Update()
    {
        if (currentFuel > 0)
        {
            currentFuel -= fuelConsumptionRate * Time.deltaTime;
            UpdateFuelSlider();
            UpdateFuelColor();

            // ��������, �� ������ ����������
            if (currentFuel <= 0f)
            {
                // ��������� ����, �� ������ ����������
                if (OnFuelEmpty != null)
                {
                    OnFuelEmpty();
                }
            }
        }
    }

    private void UpdateFuelSlider()
    {
        fuelSlider.value = currentFuel / maxFuel;
    }

    private void UpdateFuelColor()
    {
        if (currentFuel <= 0.3f * maxFuel)
        {
            fillImage.color = Color.red;
        }
        else if (currentFuel <= 0.6f * maxFuel)
        {
            fillImage.color = Color.yellow;
        }
        else
        {
            fillImage.color = Color.green;
        }
    }

    // ����� ��� ��������� ��������
    public void AddFuel(float amount)
    {
        currentFuel = Mathf.Clamp(currentFuel + amount, 0f, maxFuel);
        UpdateFuelSlider();
    }

}
