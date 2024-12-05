using UnityEngine;

public class Player : MonoBehaviour
{
    public string name;
    public int score = 301;

    // Constructor to initialize the player with a name and starting score
    public Player(string playerName, int startingScore = 301)
    {
        name = playerName;
        score = startingScore;
    }

    // Reset the score for the player
    public void ResetScore()
    {
        score = 301; // Set starting score 
    }

    // Add points to the player's score (or subtract if needed)
    public void AddScore(int points)
    {
        score -= points; // In darts, you usually subtract points from the score
    }
}
