using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Player defendingPlayer;

    public delegate void PlayerWon(Player winningPlayer);
    public static event PlayerWon OnBallScored;

    private void OnTriggerEnter(Collider collider)
    {
        PongBall ball = collider.gameObject.GetComponent<PongBall>();
        if(ball != null)
        {
            Player winningPlayer = defendingPlayer == Player.Left ? Player.Right : Player.Left;
            OnBallScored(winningPlayer);
            AudioManager.instance.PlayClipOnScore();
        }
    }
}
