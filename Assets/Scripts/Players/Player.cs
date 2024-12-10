using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public string name;
    public int score = 301;
    public int dartsLeft = 3;
    public List<DartScore> dartScores = new List<DartScore>();

    public Player(string playerName, int startingScore=301)
    {
        name = playerName;
        score = startingScore;
    }

    public virtual void resetScore()
    {
        score = 301;
    }

    public virtual void AddScore(int points)
    {
        score -= points;
    }

    public void ResetDarts()
    {
        dartsLeft = 3;
    }

    public void SubtractDart()
    {
        dartsLeft--;
    }

    public int GetDartsLeft()
    {
        return dartsLeft;
    }

    public void busted()
    {
        dartsLeft = 0;
    }

    public int getScore()
    {
        return score;
    }

    public void AddDartScore(int points, scoreArea areaHit)
    {
        DartScore dartScore = new DartScore(points, areaHit);
        dartScores.Add(dartScore);
    }



}

