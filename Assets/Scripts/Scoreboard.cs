using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    public GameObject playerScoreTxtGO;
    public GameObject aiScoreTxtGO;
    public TMP_Text playerScoreTxt;
    public TMP_Text  aiScoreTxt;
    public Player currentPlayer;
    public AIPlayer aiPlayer;



    private void Start()
    {
        playerScoreTxt = playerScoreTxtGO.GetComponent<TMP_Text>();
        aiScoreTxt = aiScoreTxtGO.GetComponent<TMP_Text>();
        InitializeScoreboard();
    }


    public void InitializeScoreboard()
    {
       // UpdateScoreboard();
    }

    public void UpdateScoreboard()
    {
        updatePlayerScore();
        updateAIScore();
    }

    public void updatePlayerScore()
    {
        currentPlayer = GameManager.Instance.playerManager.player;
        playerScoreTxt.text = "Score: " + currentPlayer.score.ToString();
    }

    public void updateAIScore()
    {
        aiPlayer = GameManager.Instance.playerManager.aiPlayer;
        aiScoreTxt.text = "Score: " + aiPlayer.score.ToString();
    }


    public void UpdateModeUI(string mode)
    {
        // Change the UI Where it sday the gamemode or something 
    }

    public void UpdateTurnUI(string turn)
    {
        // Update the UI text to reflect whose turn it is (Player or AI)
    }
}

