using UnityEngine;

public class AIPlayer : Player
{
    public AIPlayer(string playerName = "AI", int startingScore=301) : base(playerName, startingScore) { }
    public AIDartThrower aiDartThrower;

    void Start()
    {
        // Subscribe to the OnDartThrown event
        aiDartThrower = GetComponent<AIDartThrower>();
        dartsLeft = 3;
        AIDartThrower.OnAIDartThrown += SubtractDart;
    }


    private System.Collections.IEnumerator WaitAndSwitchTurn(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.Instance.playerManager.SwitchTurn();
    }

}

