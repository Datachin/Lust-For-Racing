using UnityEngine;

[CreateAssetMenu(fileName = "New Car", menuName = "Scriptable Objects/Car")]
public class Car : ScriptableObject
{
    public string carName;
    public int carPrice;
    public float speed;
    public float strength;
    public float acceleration;
    public GameObject carModel;
}
