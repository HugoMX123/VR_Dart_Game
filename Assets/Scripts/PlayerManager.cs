using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    public Player player;
    public AIPlayer aiPlayer;

    private bool isPlayerTurn = true;


    
    public void ResetPlayers()
    {
        player.ResetScore();
        aiPlayer.ResetScore();
    }

    public void SwitchTurn()
    {
        isPlayerTurn = !isPlayerTurn;

        if (GameManager.Instance.currentMode == GameMode.AI && !isPlayerTurn)
        {
            aiPlayer.TakeTurn();
            GameManager.Instance.uiManager.UpdateTurnUI("AI's Turn");
        }
        else if (isPlayerTurn)
        {
            GameManager.Instance.uiManager.UpdateTurnUI("Player's Turn");
        }
    }

    public void StartPlayerTurn()
    {
        if (GameManager.Instance.currentMode == GameMode.Practice) //Playing alone
        {
            // Just let the player keep throwing without switching turns
            GameManager.Instance.uiManager.UpdateTurnUI("Player's Turn (Practice Mode)");
        }
        else //Playing against AI
        {
            // Normal player vs AI turn logic
            SwitchTurn();
        }
    }

    public void UpdateScore(int points)
    {
        if(isPlayerTurn)
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
