using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PongBall _ballPrefab;
    private PongBall _currentBall;

    private void Awake()
    {
        Goal.OnBallScored += WinRoutine;

        
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(PaddleManager.instance.CreatePaddles());
        yield return new WaitForSeconds(0.5f);
        LaunchBall(Player.Right);
    }

    /// <summary>
    /// Creates and launches a ball towards the given player. <br></br>
    /// If this is the first round, launches towards the left. 
    /// Else, launches towards the player who was "scored" on.
    /// </summary>
    private void LaunchBall(Player receivingPlayer)
    {
        _currentBall = Instantiate(_ballPrefab, Vector2.zero, Quaternion.identity);
        _currentBall.Kickoff(receivingPlayer);
    }

    private void WinRoutine(Player scoringPlayer)
    {
        StartCoroutine(WinRoutineHelper(scoringPlayer));
    }

    private IEnumerator WinRoutineHelper(Player scoringPlayer)
    {
        ParticlesManager.instance.SpawnParticles(_currentBall.transform.position, _currentBall.GetColor());
        Destroy(_currentBall.gameObject);
        yield return new WaitForSeconds(1f);

        Player receivingPlayer = scoringPlayer == Player.Right ? Player.Left : Player.Right;
        LaunchBall(receivingPlayer);
    }

    private void OnDestroy()
    {
        Goal.OnBallScored -= WinRoutine;
    }
}

public enum Player
{
    Left = 0,
    Right = 10,
    None = 100
}