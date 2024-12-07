using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    public string name = "AI";
    public int score = 301;

    public int dartsLeft = 3;

    public void ResetScore()
    {
        score = 301;
    }

    public void AddScore(int points)
    {
        score -= points;
    }


    public void TakeTurn()
    {
        // Simple AI logic for dart throwing (can be improved later)
        int points = Random.Range(1, 21) * Random.Range(1, 4); // Random dart score (1-20) x multiplier (1-3)
        GameManager.Instance.playerManager.UpdateScore(points);
        
        // Optionally, add a delay to simulate AI's turn
        StartCoroutine(WaitAndSwitchTurn(2f)); // Wait 2 seconds before switching turn
    }

    private System.Collections.IEnumerator WaitAndSwitchTurn(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.Instance.playerManager.SwitchTurn();
    }
}
