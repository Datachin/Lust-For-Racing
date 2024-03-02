using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public void LoadLevel(int _index)
    {
        int currentMapIndex = PlayerPrefs.GetInt("CurrentMapIndex"); //get the index of the selected location
        Map currentMap = ScriptableObjectsController.Instance.GetMapByIndex(currentMapIndex); // get a map of the object by index

        //SceneManager.LoadScene(currentMap.sceneToLoad.name);
        SceneManager.LoadScene(_index);
        
    }
}
