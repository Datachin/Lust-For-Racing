using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoButton : MonoBehaviour
{
    [SerializeField] private Button goButton;
    [SerializeField] private MapDisplay mapDisplay;

    private void Start()
    {
        goButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        Map currentMap = mapDisplay.GetCurrentMap();
        PlayerPrefs.SetInt("CurrentMapIndex", currentMap.mapIndex);
        //SceneManager.LoadScene(currentMap.sceneToLoad.name);
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        bool mapUnlocked = PlayerPrefs.GetFloat("TotalDistance", 0) >= mapDisplay.GetCurrentMap().unlockDistance;
        goButton.interactable = mapUnlocked;

        if (mapUnlocked)
            goButton.image.color = Color.white;
        else
            goButton.image.color = Color.grey;
    }
}
