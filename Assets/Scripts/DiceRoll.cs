using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceRoll : MonoBehaviour
{
    public Button endTurnButton;
    public TMP_Text resultText;
    public PlayerMovement player;

    private GameObject dice1, dice2;
    private bool canRoll = true;

    private DiceFaceReader faceReader1, faceReader2;

    public void AssignDice(GameObject d1, GameObject d2)
    {
        dice1 = d1;
        dice2 = d2;

        faceReader1 = d1.GetComponent<DiceFaceReader>();
        faceReader2 = d2.GetComponent<DiceFaceReader>();

        RollDice();
    }

    // How the dices rolls with the physics
    void RollDice()
    {
        if (!canRoll) return;

        Rigidbody rb1 = dice1.GetComponent<Rigidbody>();
        Rigidbody rb2 = dice2.GetComponent<Rigidbody>();

        rb1.AddForce(Vector3.up * 5f + Random.insideUnitSphere * 2f, ForceMode.Impulse);
        rb2.AddForce(Vector3.up * 5f + Random.insideUnitSphere * 2f, ForceMode.Impulse);

        rb1.AddTorque(Random.insideUnitSphere * 10f, ForceMode.Impulse);
        rb2.AddTorque(Random.insideUnitSphere * 10f, ForceMode.Impulse);

        Invoke(nameof(ReadDiceAfterRoll), 2f);
    }

    //Records the result of the dice after calculating the sum and moves the player the number of spaces calculated.
    void ReadDiceAfterRoll()
    {
        int dice1Value = faceReader1.GetTopFaceValue();
        int dice2Value = faceReader2.GetTopFaceValue();

        int total = dice1Value + dice2Value;
        resultText.text = $"You rolled {dice1Value} + {dice2Value} = {total}";

        if (player != null)
            player.MovePlayer(total);

        canRoll = false;
        endTurnButton.gameObject.SetActive(true);
    }

    //endturn button pressed.
    public void EndTurn()
    {
        canRoll = true;
        endTurnButton.gameObject.SetActive(false);
        resultText.text = "Tap Generate Dice to roll!";
    }
}
