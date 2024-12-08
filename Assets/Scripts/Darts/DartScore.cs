[System.Serializable]
public struct DartScore
{
    public int score;
    public scoreArea scoreArea;

    public DartScore(int score, scoreArea _scoreArea)
    {
        this.score = score;
        this.scoreArea = _scoreArea;
    }
}
