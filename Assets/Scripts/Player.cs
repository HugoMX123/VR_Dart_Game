using UnityEngine;

public class Player : MonoBehaviour
{
    public string name;
    public int score = 301;
    public DartThrower dartThrower; // Reference to the DartThrower script

    [SerializeField]
    private int dartsLeft; // Number of darts the player has left

    void Start()
    {
        dartsLeft =3; // Set the number of darts the player has left to 3
        // Get the DartThrower component attached to the player
        dartThrower = GetComponent<DartThrower>();

        // Subscribe to the OnDartThrown event
        DartThrower.OnDartThrown += substractDart;
    }

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

    public void resetDarts()
    {
        dartsLeft = 3; // Reset the number of darts the player has left
    }

    public void substractDart()
    {
        dartsLeft--; // Subtract one dart from the player's darts left
    }
    
}
