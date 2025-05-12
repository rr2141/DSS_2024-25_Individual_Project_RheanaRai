using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrototype;  
    private GameObject spawnedPlayer;

    public void SpawnPlayerAtStartPosition()
    {
        GameObject gameboard = GameObject.FindWithTag("Gameboard");

        if (gameboard == null)
        {
            Debug.LogError("Gameboard is not found");
            return;
        }

        Transform startSpace = gameboard.transform.Find("Space1");

        if (startSpace == null)
        {
            Debug.LogError("Starting position 'Space1'");
            return;
        }

        spawnedPlayer = Instantiate(playerPrototype, startSpace.position + Vector3.up * 0.1f, Quaternion.identity);
        spawnedPlayer.name = "Player";
        Debug.Log("Player successfully spawned at space1");

        ARBoardManager boardManager = FindAnyObjectByType<ARBoardManager>();
        PlayerMovement movement = spawnedPlayer.GetComponent<PlayerMovement>();

        if (boardManager != null && movement != null)
        {
            movement.boardSpaces = boardManager.boardSpaces;
            Debug.Log("Linked successfully");
        }
        else
        {
            Debug.LogError("Linked unsuccessfully");
        }
    }
}
