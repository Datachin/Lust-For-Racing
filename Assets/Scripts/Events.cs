using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    private ScriptableObjectsController mapSO => ScriptableObjectsController.Instance;

    public void ReplayGame()
    {
        int currentMapIndex = PlayerPrefs.GetInt("CurrentMapIndex");
        Map currentMap = mapSO.GetMapByIndex(currentMapIndex);
        SceneManager.LoadScene(currentMap.nameSceneToLoad);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
