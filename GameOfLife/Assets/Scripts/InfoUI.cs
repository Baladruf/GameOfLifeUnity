using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoUI : MonoBehaviour
{
    private const string TURN = "Turn : ";
    private const string PLAY = "PLAY";
    private const string PAUSE = "PAUSE";

    [SerializeField]
    private TextMeshProUGUI turnText;

    public int nbTurn { get; private set; }

    [SerializeField]
    private TextMeshProUGUI stateGameText;

    public static InfoUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        nbTurn = 0;
    }

    private void UpdateTurnText()
    {
        turnText.text = TURN + nbTurn;
    }

    public void IncrementTurn()
    {
        nbTurn++;
        UpdateTurnText();
    }

    public void ResetTurn()
    {
        nbTurn = 0;
        UpdateTurnText();
    }


    public void SetStateGame(bool isPlaying)
    {
        if(isPlaying)
        {
            stateGameText.text = PLAY;
            stateGameText.color = Color.green;
        }
        else
        {
            stateGameText.text = PAUSE;
            stateGameText.color = Color.red;
        }
    }
}
