using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject DartboardGO;
    public PlayerManager playerManager;
    public Scoreboard scoreboard;
    public UIManager uiManager;

    // event when the game ends
    public  event Action<PlayerType> OnWeGotAWinner;
    

    public GameMode currentMode = GameMode.Practice;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        
    }

    private void Start()
    {
        DartboardGO.GetComponent<Dartboard>().OnHit += DartHit;
        // SUBSCRIBE TO METHOD WHEN GETTING ZERO POINTS FOR HITTING A HORRIBLE THROW (Colliders on room)
        uiManager.hideAllWins();
        StartGame();
    }


    public void StartGame()
    {
        playerManager.ResetPlayers();
        scoreboard.InitializeScoreboard();
        uiManager.hideAllWins();
        // Depending on the mode, start the appropriate behavior
        
    }

    public void SwitchMode(GameMode mode)
    {
        currentMode = mode;
        StartGame(); // Restart game for new mode
    }



    public void EndGame()
    {
        // Handle the Start again and more
        Time.timeScale = 0f; // Freeze time
        Debug.Log("END GAME");
    }

    public void DartHit(int points, scoreArea areaHit)
    {
        if (currentMode == GameMode.Practice)
        {
            if(ValidateAndApplyScore(playerManager.humanPlayer,points, areaHit))
            {
                // Announce the winner
                OnWeGotAWinner?.Invoke(playerManager.currentPlayerType);
                Debug.Log($"{playerManager.currentPlayerType} wins");
            }
        }
        else if(currentMode == GameMode.AI) // Other Mode vs Computer
        {
            var currentPlayer = playerManager.currentPlayerType == PlayerType.Human 
                ? playerManager.humanPlayer 
                : playerManager.aiPlayer;

            if (ValidateAndApplyScore(currentPlayer, points, areaHit))
            {
                // Announce the winner
                OnWeGotAWinner?.Invoke(playerManager.currentPlayerType);
                Debug.Log($"{playerManager.currentPlayerType} wins");
            }
            else if (playerManager.getCurrentPlayerDartsLeft() <= 0) // No more darts left
            {
                // Switch turn if no darts are left
                playerManager.SwitchTurn();
            }

        }
    }

    private bool ValidateAndApplyScore(Player player, int points, scoreArea areaHit)
    {
        // Prevent invalid scoring: Points scored cannot exceed current score.
        if (points > player.score)
        {
            Debug.Log("BUSTED.");
            player.busted();
            playerManager.SwitchTurn();
            return false;
        }

        // Prevent the score from dropping below 2 (special case).
        if (player.score - points < 2 && player.score - points != 0)
        {
            Debug.Log("NOT BUSTED, but the score can't go lower than 2.");
            player.busted(); // NOT REALLY BUSTED, but the player's turn is over since the score cant go lower than 2.
            playerManager.SwitchTurn();
            return false;
        }

        // Check if the player wins.
        if (points == player.score)
        {
            if (areaHit == scoreArea.Double) // REMOVE THIS TRUE THIS WAS ONLY FOR TESTING
            {
                playerManager.UpdateScore(points); // Update to 0

                return true;
            }
            else
            {
                Debug.Log("Needs to be a double to win.");
                
                return false;
            }
        }

        // Update the player's score for valid throws.
        playerManager.UpdateScore(points);
        return false;
    }


   
}
