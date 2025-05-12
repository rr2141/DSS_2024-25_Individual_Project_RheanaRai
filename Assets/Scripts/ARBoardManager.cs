using UnityEngine;
using System.Collections.Generic;

public class ARBoardManager : MonoBehaviour
{
    public GameObject gameBoard;
    public List<Transform> boardSpaces = new List<Transform>();

    void Awake()
    {
        //Tests that gameboard is assigned correctly
        if (gameBoard == null)
        {
            Debug.LogError("The gameboard is not successfully assigned!");
            return;
        }

        //If gameboard assigned, board spaces will be added
        foreach (Transform child in gameBoard.transform)
        {
            if (child.name.StartsWith("Space"))
            {
                boardSpaces.Add(child);
            }
        }

        Debug.Log($"Board spaces successfully added");
    }
}
