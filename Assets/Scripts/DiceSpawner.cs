using UnityEngine;
using UnityEngine.UI;

public class DiceSpawner : MonoBehaviour
{
    public GameObject dicePrefab;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public DiceRoll diceRoll;
    public Button generateDiceButton;

    private GameObject dice1, dice2;
    private GameObject gameBoardInstance;

    void Start()
    {
        generateDiceButton.onClick.AddListener(() =>
        {
            //Finds the gameboard on the virtual environment
            if (gameBoardInstance == null)
            {
                gameBoardInstance = GameObject.FindGameObjectWithTag("Gameboard");
            }

            if (gameBoardInstance != null)
            {
                SpawnDice();
            }
            else
            {
                Debug.LogWarning("Game board not placed yet!");
            }
        });
    }

    void SpawnDice()
    {
        //Prevents more than two dices being spawned at a time
        if (dice1 != null) Destroy(dice1);
        if (dice2 != null) Destroy(dice2);

        dice1 = Instantiate(dicePrefab, spawnPoint1.position, Random.rotation);
        dice2 = Instantiate(dicePrefab, spawnPoint2.position, Random.rotation);

        dice1.transform.localScale = Vector3.one * 0.1f;
        dice2.transform.localScale = Vector3.one * 0.1f;

        //Sends dices to DiceRoll script
        if (diceRoll != null)
            diceRoll.AssignDice(dice1, dice2);
    }
}
