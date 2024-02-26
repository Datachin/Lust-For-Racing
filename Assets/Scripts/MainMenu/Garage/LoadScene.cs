using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public void LoadLevel(int _index)
    {
        SceneManager.LoadScene(_index);
    }
}
