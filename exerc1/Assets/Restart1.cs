using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart1 : MonoBehaviour
{
    public void Recomeca1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
}