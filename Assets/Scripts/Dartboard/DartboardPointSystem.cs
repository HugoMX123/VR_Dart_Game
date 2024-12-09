using UnityEngine;

public class DartboardPointSystem : MonoBehaviour
{
    public GameObject DartboardGO;

    // Define the radial distance thresholds for different zones
    private const float BULLSEYE_MAX_RADIUS = 0.014f;  // Radius for bullseye (fixed score 50)
    private const float BULL_MAX_RADIUS = 0.032f; // Radius for outer bullseye (fixed score 25)
    private const float INNER_NORMAL_ZONE_MAX_RADIUS = 0.173f;  // Inner normal score zone
    private const float TRIPLE_ZONE_MAX_RADIUS = 0.193f;        // Triple score zone
    private const float OUTER_NORMAL_ZONE_MAX_RADIUS = 0.286f;  // Outer normal score zone
    private const float DOUBLE_ZONE_MAX_RADIUS = 0.305f;        // Double score zone
    private const float ZERO_ZONE_MAX_RADIUS = 4f;             // Zone for no score (outside dartboard)

    // Define the sectors of the dartboard, each with a specific score
    private int[] sectorScores = new int[20] {
        6, 13, 4, 18, 1, 20, 5, 12, 9, 14, 11, 8, 16, 7, 19, 3, 17, 2, 15, 10
    };

    public (int score, scoreArea area) GetScoreFromPolar(float degrees, float r)
    {
        scoreArea areaHit = scoreArea.Single;

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
            areaHit = scoreArea.Bullseye;
            return (50, areaHit); // Bullseye score
        }
        else if (r <= BULL_MAX_RADIUS)
        {
            areaHit = scoreArea.Bull;
            return (25, areaHit); // Outer bullseye score
        }
        else
        {
            // If it's outside the bullseye or outer bullseye, calculate based on sector and radial zone
            int radialScore = GetRadialScore(r, out scoreArea radialArea);
            int sectorScore = sectorScores[sector]; // Get the sector score

            // Update areaHit to the radial area
            areaHit = radialArea;

            // Calculate the final score (sector score * radial zone score)
            return (sectorScore * radialScore, areaHit);
        }
    }

    private int GetRadialScore(float r, out scoreArea radialArea)
    {
        if (r <= INNER_NORMAL_ZONE_MAX_RADIUS)
        {
            radialArea = scoreArea.Single;
            return 1; // Inner normal score zone
        }
        else if (r <= TRIPLE_ZONE_MAX_RADIUS)
        {
            radialArea = scoreArea.Triple;
            return 3; // Triple score zone
        }
        else if (r <= OUTER_NORMAL_ZONE_MAX_RADIUS)
        {
            radialArea = scoreArea.Single;
            return 1; // Outer normal score zone
        }
        else if (r <= DOUBLE_ZONE_MAX_RADIUS)
        {
            radialArea = scoreArea.Double;
            return 2; // Double score zone
        }
        else if (r <= ZERO_ZONE_MAX_RADIUS)
        {
            radialArea = scoreArea.Zero;
            return 0; // No score (outside dartboard)
        }
        else
        {
            radialArea = scoreArea.Zero;
            return 0; // Outside the dartboard (no score)
        }
    }
}


