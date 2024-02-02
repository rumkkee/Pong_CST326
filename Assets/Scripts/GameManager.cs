using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PongBall ballPrefab;
    private PongBall currentBall;


    private void Awake()
    {
        Goal.OnBallScored += WinRoutine;

        StartBall();
    }

    private void StartBall()
    {
        currentBall = Instantiate(ballPrefab, Vector2.zero, Quaternion.identity);
        currentBall.MoveBall();
    }

    private void WinRoutine(Player scoringPlayer)
    {
        // TODO: Destroy Ball
        Destroy(currentBall);

        // TODO Incremement winning Player's Score;
        

        // TODO: Create and Launch new ball in the direction of the losing player;
        StartBall();
    }

    private void OnDestroy()
    {
        Goal.OnBallScored -= WinRoutine;
    }
}

public enum Player
{
    left = 0,
    right = 10
}