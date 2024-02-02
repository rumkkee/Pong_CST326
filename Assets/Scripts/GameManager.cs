using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PongBall ballPrefab;
    private PongBall currentBall;
    private void Start()
    {
        StartBall();
    }

    private void StartBall()
    {
        currentBall = Instantiate(ballPrefab, Vector2.zero, Quaternion.identity);
        currentBall.MoveBall();
    }
}
