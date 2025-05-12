using UnityEngine;

public class StartMenuController : MonoBehaviour
{
    public GameObject startMenuCanvas;
    public GameObject gameUICanvas;
    public GameboardManager gameboardManager;

    public void StartGame()
    {
        //Hides 
        startMenuCanvas.SetActive(false);

        //Shows 
        gameUICanvas.SetActive(true);

        //Game Starts
        if (gameboardManager != null)
        {
            gameboardManager.gameStarted = true;
            StartCoroutine(PlaceGameboardWithDelay());
        }

        Debug.Log("Game Started Successfully");
    }

    private System.Collections.IEnumerator PlaceGameboardWithDelay()
    {
        yield return new WaitForSeconds(0.5f); 

        Vector2 centerScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);
        gameboardManager.PlaceObject(centerScreen);
    }
}
