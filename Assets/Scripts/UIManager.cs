using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject scoreboard;
    public GameObject playerWinsTxt;
    public GameObject aiWinsTxt;

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

    

    public void UpdateTurnUI(string turn)
    {
        // Display the current turn on the screen
    }

    public void UpdateModeUI(string mode)
    {
        // Update UI to show the current mode (Practice or Game)
    }
}
