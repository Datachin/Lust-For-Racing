using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text mapName;
    [SerializeField] private Image mapImage;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private Image[] maxDistanceImages;
    [SerializeField] private Sprite[] numberSprites;  
    [SerializeField] private Sprite dotSprite; 

    private Map currentMap; 
    
    [SerializeField] private Button btPrev; 
    [SerializeField] private Button btNext;

    private ScriptableObjectsController mapSO => ScriptableObjectsController.Instance;

    private void Awake()
    {
        btPrev.onClick.AddListener(() =>
        {
            mapSO.ChangrScriptableObject(-1);
            DisplayMap(mapSO.GetCurrentMap());
        });
        btNext.onClick.AddListener(() =>
        {
            mapSO.ChangrScriptableObject(1);
            DisplayMap(mapSO.GetCurrentMap());
        });
        
        
        DisplayMap(mapSO.GetCurrentMap());
    }

    private void OnDestroy()
    {
        btPrev.onClick.RemoveAllListeners();
        btNext.onClick.RemoveAllListeners();
    }

    public void DisplayMap(Map map)
    {
        currentMap = map;

        mapName.text = currentMap.mapName;
        mapImage.sprite = currentMap.mapImage;

        // Load the max distance from PlayerPrefs
        float maxDistance = PlayerPrefs.GetFloat(currentMap.mapName + "MaxDistance", currentMap.maxDistance);

        // Convert the max distance to a string with one decimal place
        string stats = maxDistance.ToString("F1"); 
        char[] digits = stats.PadLeft(7, '0').ToCharArray();

        for (int i = 0; i < maxDistanceImages.Length; i++)
        {
            if (i < digits.Length)
            {
                if (Char.IsDigit(digits[i]))
                {
                    int digit = int.Parse(digits[i].ToString());
                    maxDistanceImages[i].sprite = numberSprites[digit];  // Use sprite numbers
                }
                else if (digits[i].Equals(',') || digits[i].Equals('.'))
                {
                    maxDistanceImages[i].sprite = dotSprite;  // Use a dot sprite
                }
            }
            else
            {
                maxDistanceImages[i].sprite = null;  // If there are no more numbers, clear the sprite
            }
        }

        bool mapUnlocked = PlayerPrefs.GetFloat("TotalDistance", 0) >= currentMap.unlockDistance;

        lockImage.SetActive(!mapUnlocked);

        if (mapUnlocked)
            mapImage.color = Color.white;
        else
            mapImage.color = Color.grey;
    }

    public void UpdateMaxDistance(float distance)
    {
        // Update the max distance
        float maxDistance = Mathf.Max(distance, PlayerPrefs.GetFloat(currentMap.mapName + "MaxDistance", currentMap.maxDistance));
        PlayerPrefs.SetFloat(currentMap.mapName + "MaxDistance", maxDistance);

        // Save PlayerPrefs
        PlayerPrefs.Save();
    }

    // New method to get the current map
    public Map GetCurrentMap()
    {
        return currentMap;
    }
}
