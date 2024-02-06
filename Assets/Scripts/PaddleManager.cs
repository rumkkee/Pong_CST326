using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleManager : MonoBehaviour
{
    [SerializeField] private Paddle _paddlePrefab;
    private Paddle _leftPaddle;
    private Paddle _rightPaddle;

    public static PaddleManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public IEnumerator CreatePaddles()
    {
        Vector2 spawnPos = new Vector2(23.5f, 0f);
        _leftPaddle = Instantiate(_paddlePrefab, -spawnPos, _paddlePrefab.transform.rotation);
        _leftPaddle.SetTeam(Player.Left);

        yield return new WaitForSeconds(0.2f);

        _rightPaddle = Instantiate(_paddlePrefab, spawnPos, _paddlePrefab.transform.rotation);
        _rightPaddle.SetTeam(Player.Right);
    }
}
