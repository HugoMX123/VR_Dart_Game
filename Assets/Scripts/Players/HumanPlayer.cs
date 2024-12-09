using UnityEngine;

public class HumanPlayer : Player
{
    public HumanPlayer(string playerName, int startingScore = 301) : base(playerName, startingScore) { }

    public Teleport teleport;

    void Start()
    {
        dartsLeft = 3;

        teleport = GetComponent<Teleport>();

        // Subscribe to the OnDartThrown event
        DartThrower.OnDartThrown += SubtractDart;

        
    }

    public void teleportToPlay()
    {
        teleport.TeleportToPlay();
    }

    public void teleportToWait()
    {
        teleport.TeleportToWait();
    }

    


}
