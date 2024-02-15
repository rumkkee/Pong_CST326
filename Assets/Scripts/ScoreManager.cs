using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _leftScore;
    private int _rightScore;

    

    private void Awake()
    {
        Goal.OnBallScored += RaiseScore;
    }

    private void RaiseScore(Player player)
    {
        if(player == Player.Left)
        {
            ++_leftScore;
            ScoreDisplayManager.instance.UpdateLeftScore(_leftScore);
        }
        else
        {
            ++_rightScore;
            ScoreDisplayManager.instance.UpdateRightScore(_rightScore);
        }

        Debug.Log(
            $"<b>{player}</b> player scored!\n" +
            $"Left: {_leftScore}, Right: {_rightScore}"
            );

        CheckWinCondition(player);
    }

    private void CheckWinCondition(Player player)
    {
        if(_leftScore >= 11 || _rightScore >= 11)
        {
            Debug.Log($"Game Over, {player} Paddle Wins!");
            ResetScores();
        }
    }

    private void ResetScores()
    {
        _leftScore = 0;
        _rightScore = 0;
        ScoreDisplayManager.instance.ResetScores();
        Debug.Log(
            "Scores Reset! \n" +
            $"Left: {_leftScore}, Right: {_rightScore}"
            );
    }

    private void OnDestroy()
    {
        Goal.OnBallScored -= RaiseScore;
    }
}
