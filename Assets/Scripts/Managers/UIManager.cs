using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject scoreboard;
    public GameObject playerWinsTxt;
    public GameObject aiWinsTxt;

    void Start()
    {
        // Hide the player wins and AI wins text
        hideAllWins();

        // Subscribe to the OnWeGotAWinner event
        GameManager.Instance.OnWeGotAWinner += showWinningPlayer;

    }

    public void showWinningPlayer(PlayerType playerType)
    {
        // Show the winning player
        if (playerType == PlayerType.Human)
        {
            showPlayerWins();
        }
        else
        {
            showAIWins();
        }
    }

    public void showPlayerWins()
    {
        playerWinsTxt.SetActive(true);
    }

    public void showAIWins()
    {
        aiWinsTxt.SetActive(true);
    }

    public void hidePlayerWins()
    {
        playerWinsTxt.SetActive(false);
    }

    public void hideAIWins()
    {
        aiWinsTxt.SetActive(false);
    }

    public void hideAllWins()
    {
        hidePlayerWins();
        hideAIWins();
    }

    
}
