using UnityEngine;
using UnityEngine.SceneManagement;


public class Back1 : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("Starting");
    }
}
