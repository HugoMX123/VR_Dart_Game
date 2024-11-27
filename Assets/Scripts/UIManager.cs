using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    //public Text gameOverText;

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        //gameOverText.text = "Game Over!";
    }

    public void HideGameOver()
    {
        gameOverPanel.SetActive(false);
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
