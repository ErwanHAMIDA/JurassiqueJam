using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "S_LoadManager", menuName = "Scriptable Objects/S_LoadManager")]
public class S_LoadManager : ScriptableObject
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
