using UnityEngine;

public class DartboardPointSystem : MonoBehaviour
{
    public GameObject DartboardGO;
    

    // Define the radial distance thresholds for different zones
    private const float BULLSEYE_MAX_RADIUS = 0.014f;  // Radius for bullseye (fixed score 50)
    private const float BULL_MAX_RADIUS = 0.032f; // Radius for outer bullseye (fixed score 25)
    
    // Redefine other zones based on dartboard layout:
    private const float INNER_NORMAL_ZONE_MAX_RADIUS = 0.173f;  // Inner normal score zone
    private const float TRIPLE_ZONE_MAX_RADIUS = 0.193f;        // Triple score zone
    private const float OUTER_NORMAL_ZONE_MAX_RADIUS = 0.286f;  // Outer normal score zone
    private const float DOUBLE_ZONE_MAX_RADIUS = 0.305f;        // Double score zone
    private const float ZERO_ZONE_MAX_RADIUS = 4f;             // Zone for no score (outside dartboard)


    // Define the sectors of the dartboard, each with a specific score
    private int[] sectorScores = new int[20] {
        6, 13, 4, 18, 1, 20, 5, 12, 9, 14, 11, 8, 16, 7, 19, 3, 17, 2, 15, 10
    };



    // Function to convert polar coordinates (degrees and radius) to Cartesian and determine the score
    public int GetScoreFromPolar(float degrees, float r)
    {
        // Normalize the degrees to be between 0 and 360
        degrees = (degrees + 360f) % 360f;

        degrees = (degrees + 9f) % 360f; // Shift by 9 degrees to center sectors around 0, 18, 36, etc.

        // Calculate the angular sector based on the degrees (divide by 18 to get sector number)
        int sector = (int)(degrees / 18);  // 0-19 sectors

        // Ensure the sector is within the range [0, 19]
        sector = sector % 20;

        // Check if the dart is in the bullseye or outer bullseye (fixed scores)
        if (r <= BULLSEYE_MAX_RADIUS)
        {   
            return 50; // Bullseye score
        }
        else if (r <= BULL_MAX_RADIUS)
        {
            return 25; // Outer bullseye score
        }
        else
        {
            // If it's outside the bullseye or outer bullseye, calculate based on sector and radial zone
            int radialScore = GetRadialScore(r); // Get the radial zone score
            int sectorScore = sectorScores[sector]; // Get the sector score

            // Calculate the final score (sector score * radial zone score)
            return sectorScore * radialScore;
        }
    }

    // Function to determine the radial zone score based on the distance from the center (r)
    private int GetRadialScore(float r)
    {
        if (r <= INNER_NORMAL_ZONE_MAX_RADIUS)
        {
            return 1; // Inner normal score zone
        }
        else if (r <= TRIPLE_ZONE_MAX_RADIUS)
        {
            return 3; // Triple score zone
        }
        else if (r <= OUTER_NORMAL_ZONE_MAX_RADIUS)
        {
            return 1; // Outer normal score zone
        }
        else if (r <= DOUBLE_ZONE_MAX_RADIUS)
        {
            return 2; // Double score zone
        }
        else if (r <= ZERO_ZONE_MAX_RADIUS)
        {
            return 0; // No score (outside dartboard)
        }
        else
        {
            return 0; // Outside the dartboard (no score)
        }
    }
} 


