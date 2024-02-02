using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int leftScore;
    public int rightScore;

    private void Awake()
    {
        Goal.OnBallScored += RaiseScore;
    }

    private void RaiseScore(Player player)
    {
        if(player == Player.Left)
        {
            ++leftScore;
        }
        else
        {
            ++rightScore;
        }

        Debug.Log(
            $"<b>{player}</b> player scored!\n" +
            $"Left: {leftScore}, Right: {rightScore}"
            );

    }

    private void OnDestroy()
    {
        Goal.OnBallScored -= RaiseScore;
    }
}
