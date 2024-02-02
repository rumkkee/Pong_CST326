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

        StartBall(Player.Right);
    }

    /// <summary>
    /// Creates and launches a ball towards the correct player. <br></br>
    /// If this is the first round, launches towards the left. 
    /// Else, launches towards the player who was "scored" on.
    /// </summary>
    private void StartBall()
    {
        currentBall = Instantiate(ballPrefab, Vector2.zero, Quaternion.identity);
        currentBall.Kickoff(Player.Right);
    }

    private void StartBall(Player receivingPlayer)
    {
        currentBall = Instantiate(ballPrefab, Vector2.zero, Quaternion.identity);
        currentBall.Kickoff(receivingPlayer);
    }

    private void WinRoutine(Player scoringPlayer)
    {
        StartCoroutine(WinRoutineHelper(scoringPlayer));
    }

    private IEnumerator WinRoutineHelper(Player scoringPlayer)
    {
        Destroy(currentBall.gameObject);
        yield return new WaitForSeconds(1f);

        Player receivingPlayer = scoringPlayer == Player.Right ? Player.Left : Player.Right;
        StartBall(receivingPlayer);
    }

    private void OnDestroy()
    {
        Goal.OnBallScored -= WinRoutine;
    }
}

public enum Player
{
    Left = 0,
    Right = 10
}