using UnityEngine;




public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject DartboardGO;
    public PlayerManager playerManager;
    public Player player;
    public AIPlayer aiPlayer;
    public Scoreboard scoreboard;
    public UIManager uiManager;
    

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
        uiManager.hideAllWins();
        StartGame();
    }


    public void StartGame()
    {
        playerManager.ResetPlayers();
        scoreboard.InitializeScoreboard();
        //uiManager.HideGameOver();
        //uiManager.UpdateModeUI(currentMode.ToString());
        
        // Depending on the mode, start the appropriate behavior
        if (currentMode == GameMode.Practice)
        {
            PracticeMode();
        }
        else if (currentMode == GameMode.AI)
        {
            AIStartGame();
        }
    }

    public void SwitchMode(GameMode mode)
    {
        currentMode = mode;
        StartGame(); // Restart game for new mode
    }


    private void PracticeMode()
    {
        // In Practice Mode, the player just throws darts with no opponent
        uiManager.UpdateTurnUI("Player (Practice Mode)");
        playerManager.StartPlayerTurn();

    }

    private void AIStartGame()
    {
        // In AI Mode, it's player vs. AI
        uiManager.UpdateTurnUI("Player's Turn");
        playerManager.StartPlayerTurn();
    }

    public void EndGame()
    {
        // Handle the Start again and more
    }

    public void DartHit(int points, scoreArea areaHit)
    {
        if (currentMode == GameMode.Practice)
        {
            // Prevent invalid scoring: Points scored cannot exceed current score.
            if (points > playerManager.player.score)
            {
                Debug.Log("Invalid points: Cannot score more than current score.");
                return;
            }

            // Prevent the score from dropping below 2 (special case).
            if (playerManager.player.score - points < 2 && playerManager.player.score - points != 0)
            {
                Debug.Log("Invalid throw: Remaining score cannot be less than 2 unless it's exactly 0 with a double.");
                return;
            }

            // Check if the player wins.
            if (points == playerManager.player.score)
            {
                if (areaHit == scoreArea.Double)
                {
                    Debug.Log("Player Wins!");
                    playerManager.UpdateScore(points); // Update to 0
                    uiManager.showPlayerWins();
                    playerManager.player.dartThrower.SetCanThrow(false);
                    EndGame();
                    return;
                }
                else
                {
                    Debug.Log("Needs to be a double to win.");
                    return;
                }
            }

            // Update the player's score for valid throws.
            playerManager.UpdateScore(points);
            Debug.Log($"Score updated. Remaining score: {playerManager.player.score}");
        }
        else if(currentMode == GameMode.AI)
        {
            // Other Mode vs Computer
        }
    }
}
