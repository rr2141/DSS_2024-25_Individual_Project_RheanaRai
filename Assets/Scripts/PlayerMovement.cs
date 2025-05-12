using UnityEngine;
using System.Collections;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public ARRaycastManager arRaycastManager;
    public GameObject gameboard;  
    public GameObject xrOrigin;   
    public float moveSpeed = 1.5f;

    public List<Transform> boardSpaces = new List<Transform>();

    private int currentIndex = 0;
    private bool isBoardSpacesInitialized = false;

    void Start()
    {
        if (arRaycastManager == null)
        {
            arRaycastManager = Object.FindFirstObjectByType<ARRaycastManager>();
        }

 
        if (boardSpaces.Count == 0 && gameboard != null)
        {
            AutoFillBoardSpacesFromGameboard();
        }

        StartCoroutine(WaitForBoardSpacesInitialization());
    }

    void AutoFillBoardSpacesFromGameboard()
    {
        boardSpaces.Clear();
        for (int i = 1; i <= 36; i++)
        {
            Transform space = gameboard.transform.Find("Space" + i);
            if (space != null)
            {
                boardSpaces.Add(space);
            }
            else
            {
                Debug.LogWarning("Board Space" + i + " not found on in Gameboard.");
            }
        }

        Debug.Log($" ARBoardManager: Found {boardSpaces.Count} board spaces.");
        isBoardSpacesInitialized = true;
    }

    IEnumerator WaitForBoardSpacesInitialization()
    {
        while (!isBoardSpacesInitialized)
        {
            yield return null;
        }

        Debug.Log("Linked successfully");
    }

    public void MovePlayer(int steps)
    {
        if (boardSpaces.Count == 0)
        {
            Debug.LogError("Player cannot move");
            return;
        }

        int targetIndex = Mathf.Clamp(currentIndex + steps, 0, boardSpaces.Count - 1);
        Debug.Log($"Attempting to move player {steps} steps (currentIndex: {currentIndex}, targetIndex: {targetIndex})");

        StartCoroutine(MoveStepByStep(targetIndex));
    }

    IEnumerator MoveStepByStep(int targetIndex)
    {
        if (boardSpaces.Count == 0)
        {
            Debug.LogError("Player cannot move.");
            yield break;
        }

        Vector3 localTargetPosition = gameboard.transform.InverseTransformPoint(boardSpaces[targetIndex].position);
        Vector3 worldTargetPosition = xrOrigin.transform.TransformPoint(localTargetPosition);

        while (Vector3.Distance(transform.position, worldTargetPosition) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, worldTargetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = worldTargetPosition;
        currentIndex = targetIndex;

        //Shows WinCanvas if player reaches end of virtual gameboard
        if (currentIndex == boardSpaces.Count - 1)
        {
   
            ShowWinUI();
        }

        Debug.Log($" Player has reached the final space: {currentIndex}");
    }

    private bool Raycast(Vector2 screenPosition, out ARRaycastHit hitInfo)
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (arRaycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
        {
            hitInfo = hits[0];
            return true;
        }

        hitInfo = default;
        return false;
    }

    public GameObject winUICanvas;  

    private void ShowWinUI()
    {
        if (winUICanvas != null)
        {
            winUICanvas.SetActive(true);  
            Debug.Log("Player wins!.");
        }
        else
        {
            Debug.LogError(" Win UI Canvas is not assigned");
        }
    }

}
