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

        CheckWinCondition(player);
    }

    private void CheckWinCondition(Player player)
    {
        if(leftScore >= 11 || rightScore >= 11)
        {
            Debug.Log($"Game Over, {player} Paddle Wins!");
            ResetScores();
        }
    }

    private void ResetScores()
    {
        leftScore = 0;
        rightScore = 0;
        Debug.Log(
            "Scores Reset! \n" +
            $"Left: {leftScore}, Right: {rightScore}"
            );
    }

    private void OnDestroy()
    {
        Goal.OnBallScored -= RaiseScore;
    }
}
