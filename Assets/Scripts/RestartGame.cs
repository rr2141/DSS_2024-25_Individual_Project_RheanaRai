using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGameController : MonoBehaviour
{
    public void RestartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

