using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleManager : MonoBehaviour
{
    [SerializeField] Paddle paddlePrefab;
    private Paddle leftPaddle;
    private Paddle rightPaddle;

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
        leftPaddle = Instantiate(paddlePrefab, -spawnPos, paddlePrefab.transform.rotation);
        leftPaddle.SetTeam(Player.Left);

        yield return new WaitForSeconds(0.2f);

        rightPaddle = Instantiate(paddlePrefab, spawnPos, paddlePrefab.transform.rotation);
        rightPaddle.SetTeam(Player.Right);
    }
}
