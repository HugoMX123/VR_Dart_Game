using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    public Player player;
    public AIPlayer aiPlayer;

    public PlayerType currentPlayerType = PlayerType.Human;

    public void ResetPlayers()
    {
        player.ResetScore();
        aiPlayer.ResetScore();
    }

    public void SwitchTurn()
    {
        // Toggle between Human and AI
        currentPlayerType = currentPlayerType == PlayerType.Human ? PlayerType.AI : PlayerType.Human;

        if (GameManager.Instance.currentMode == GameMode.AI && currentPlayerType == PlayerType.AI)
        {
            aiPlayer.TakeTurn();
            GameManager.Instance.uiManager.UpdateTurnUI("AI's Turn");
        }
        else if (currentPlayerType == PlayerType.Human)
        {
            GameManager.Instance.uiManager.UpdateTurnUI("Player's Turn");
        }
    }

    public void StartPlayerTurn()
    {
        if (GameManager.Instance.currentMode == GameMode.Practice) // Playing alone
        {
            // Just let the player keep throwing without switching turns
            GameManager.Instance.uiManager.UpdateTurnUI("Player's Turn (Practice Mode)");
        }
        else // Playing against AI
        {
            // Normal player vs AI turn logic
            SwitchTurn();
        }
    }

    public void UpdateScore(int points)
    {
        if (currentPlayerType == PlayerType.Human)
        {
            player.AddScore(points);
        }
        else
        {
            aiPlayer.AddScore(points);
        }
        
        GameManager.Instance.scoreboard.UpdateScoreboard();
    }
}



