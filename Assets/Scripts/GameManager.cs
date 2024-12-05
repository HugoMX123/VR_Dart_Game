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

        if (playerManager.player.score == 0)
        {
            
        }

    }

    private void AIStartGame()
    {
        // In AI Mode, it's player vs. AI
        uiManager.UpdateTurnUI("Player's Turn");
        playerManager.StartPlayerTurn();
    }

    public void EndGame()
    {
        uiManager.ShowGameOver();
    }

    public void DartHit(int points,scoreArea areaHit)
    {
        playerManager.UpdateScore(points); // Only If its allowed on the gamemode 
    }

}
