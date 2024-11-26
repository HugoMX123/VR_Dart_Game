using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerManager playerManager;
    public Scoreboard scoreboard;
    public UIManager uiManager;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartGame()
    {
        playerManager.ResetPlayers();
        scoreboard.InitializeScoreboard();
        uiManager.HideGameOver();
        NextTurn();
    }

    public void EndGame()
    {
        uiManager.ShowGameOver();
    }

    public void NextTurn()
    {
        if (playerManager.IsPlayerTurn())
        {
            // Player's turn
            uiManager.UpdateTurnUI("Player");
        }
        else
        {
            // AI's turn
            uiManager.UpdateTurnUI("AI");
            playerManager.AIPlayerTurn();
        }
    }
}
