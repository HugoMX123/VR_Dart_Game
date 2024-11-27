using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    public GameObject playerScoreTxtGO;
    public GameObject aiScoreTxtGO;
    private TMP_Text playerScoreTxt;
    private TMP_Text  aiScoreTxt;



    private void Start()
    {
        playerScoreTxt = playerScoreTxtGO.GetComponent<TMP_Text>();
        aiScoreTxt = aiScoreTxtGO.GetComponent<TMP_Text>();
        InitializeScoreboard();
    }


    public void InitializeScoreboard()
    {
        UpdateScoreboard();
    }

    public void UpdateScoreboard()
    {
        Player currentPlayer = GameManager.Instance.playerManager.player;
        AIPlayer aiPlayer = GameManager.Instance.playerManager.aiPlayer;

        playerScoreTxt.text = "Score: " + currentPlayer.score.ToString();
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

