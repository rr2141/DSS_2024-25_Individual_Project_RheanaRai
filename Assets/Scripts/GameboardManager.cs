using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GameboardManager : MonoBehaviour
{
    [SerializeField] private GameObject gameBoardPrefab;
    [SerializeField] private ARRaycastManager raycastManager;
    public bool gameStarted = false;

    private List<ARRaycastHit> hitList = new List<ARRaycastHit>();
    private GameObject lastPlacedObject;
    private bool boardPlaced = false;

    void Update()
    {
        if (gameStarted && !boardPlaced)
        {
            Vector2 centerScreen = new Vector2(Screen.width / 2, Screen.height / 2);
            PlaceObject(centerScreen);
        }
    }

    public void PlaceObject(Vector2 screenPosition)
    {
        if (lastPlacedObject != null || boardPlaced) return;

        if (raycastManager.Raycast(screenPosition, hitList, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hitList[0].pose;

            lastPlacedObject = Instantiate(gameBoardPrefab, hitPose.position, Quaternion.identity);
            lastPlacedObject.tag = "Gameboard";
            boardPlaced = true;

            Debug.Log("Gameboard is placed at: " + lastPlacedObject.transform.position);

            // Spawns the player
            PlayerSpawner spawner = FindFirstObjectByType<PlayerSpawner>();
            if (spawner != null)
            {
                spawner.SpawnPlayerAtStartPosition();
            }
        }
        else
        {
            Debug.LogWarning("No plane has been detected!");
        }
    }
}
