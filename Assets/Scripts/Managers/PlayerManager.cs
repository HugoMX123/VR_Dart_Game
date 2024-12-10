using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject DartboardGO;
    public GameObject HumanPlayerGO;
    public GameObject AIPlayerGO;
    public GameObject AICharacterGO;
    public AIDartThrower aiDartThrower;
    public Player humanPlayer;
    public Player aiPlayer;
    public PlayerType currentPlayerType = PlayerType.Human;

    void Awake()
    {
        aiDartThrower = AIPlayerGO.GetComponent<AIDartThrower>();
    }

    void Start()
    {
        DartboardGO.GetComponent<Dartboard>().OnHit += addDartScore;
        Dart.OnDartHit += addDartScore;
    }

    public void ResetPlayers()
    {
        humanPlayer.resetScore();
        aiPlayer.resetScore();
    }

    public void SwitchTurn()
    {
        DestroyAllWithName("GeneratedDart(Clone)");

        // Toggle between Human and AI
        currentPlayerType = currentPlayerType == PlayerType.Human ? PlayerType.AI : PlayerType.Human;

        //Check whos turns it is and reset darts
        if(currentPlayerType == PlayerType.Human)
        {
            // Teleport the human player to the starting position
            HumanPlayerGO.GetComponent<HumanPlayer>().teleportToPlay();
            AICharacterGO.GetComponent<TeleportAiCharacter>().TeleportToWait();
            humanPlayer.ResetDarts();
        }
        else // AI TURN
        {
            HumanPlayerGO.GetComponent<HumanPlayer>().teleportToWait();
            AICharacterGO.GetComponent<TeleportAiCharacter>().TeleportToPlay();
            aiPlayer.ResetDarts();    
            aiDartThrower.StartThrowingDarts();
        }
    }



    public void UpdateScore(int points)
    {
        if (currentPlayerType == PlayerType.Human)
        {
            humanPlayer.AddScore(points);
        }
        else
        {
            aiPlayer.AddScore(points);
        }
        
        GameManager.Instance.scoreboard.UpdateScoreboard();
    }


    // Gets and setters for the scores of each player
    public int getCurrentPlayerScore()
    {
        return currentPlayerType == PlayerType.Human ? humanPlayer.getScore() : aiPlayer.getScore();
    }

    public int getCurrentPlayerDartsLeft()
    {
        return currentPlayerType == PlayerType.Human ? humanPlayer.GetDartsLeft() : aiPlayer.GetDartsLeft();
    }

    public void addDartScore(int points, scoreArea areaHit)
    {
        if (currentPlayerType == PlayerType.Human)
        {
            humanPlayer.AddDartScore(points, areaHit);
        }
        else
        {
            aiPlayer.AddDartScore(points, areaHit);
        }
    }

    public void DestroyAllWithName(string name)
    {
        // Find all GameObjects in the scene
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        
        // Loop through all objects and destroy those that match the name
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == name)
            {
                Destroy(obj);
            }
        }
    }
 

    
}



