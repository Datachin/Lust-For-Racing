using UnityEngine;

public class ScriptableObjectsController : MonoBehaviour
{
    public static ScriptableObjectsController Instance; 

    [SerializeField] private Map[] maps; 
    [SerializeField] private MapDisplay mapDisplay;
    private int currentIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        // We check if at least one location exists
        if (maps.Length > 0)
        {
            ChangrScriptableObject(0);
        }
    }

    public void ChangrScriptableObject(int change)
    {
        currentIndex += change;
        if (currentIndex < 0) currentIndex = maps.Length - 1;
        else if (currentIndex >= maps.Length) currentIndex = 0;

        if (mapDisplay != null) mapDisplay.DisplayMap(maps[currentIndex]);
    }

    public Map GetMapByIndex(int index)
    {
        if (index >= 0 && index < maps.Length) // We check whether the index is within the bounds of the array
        {
            return maps[index]; // We return the Map object by index
        }
        else
        {
            return null; // We return null if the index exceeds the bounds of the array
        }
    }
}
