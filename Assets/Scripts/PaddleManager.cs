using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleManager : MonoBehaviour
{
    [SerializeField] private Paddle _paddlePrefab;
    [SerializeField] private Color _leftPaddleColor;
    [SerializeField] private Color _rightPaddleColor;

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
        StartCoroutine(PaddleSpawnAnimation(_leftPaddle));
        yield return StartCoroutine(_leftPaddle.SetColor(_leftPaddleColor));

        yield return new WaitForSeconds(0.2f);

        _rightPaddle = Instantiate(_paddlePrefab, spawnPos, _paddlePrefab.transform.rotation);
        _rightPaddle.SetTeam(Player.Right);
        StartCoroutine(PaddleSpawnAnimation(_rightPaddle));
        yield return StartCoroutine(_rightPaddle.SetColor(_rightPaddleColor));
    }

    private IEnumerator PaddleSpawnAnimation(Paddle paddle)
    {
        float timePassed = 0f;
        Vector3 startScale = new Vector3(paddle.transform.localScale.x * 4, 0, paddle.transform.localScale.z);
        Vector3 stretchedScale = new Vector3(paddle.transform.localScale.x * 0.5f, paddle.transform.localScale.y * 1.5f, paddle.transform.localScale.z);
        Vector3 finalScale = paddle.transform.localScale;
        do
        {
            timePassed += Time.deltaTime;
            paddle.transform.localScale = Vector3.Lerp(startScale, stretchedScale, timePassed * 6f);
            yield return null;
        } while (paddle.transform.localScale != stretchedScale);

        timePassed = 0f;
        do
        {
            timePassed += Time.deltaTime;
            paddle.transform.localScale = Vector3.Lerp(stretchedScale, finalScale, timePassed * 10f);
            yield return null;
        } while (paddle.transform.localScale != finalScale);


    }
}
