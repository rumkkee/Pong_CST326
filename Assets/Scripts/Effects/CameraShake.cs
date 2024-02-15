using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    public float shakeAmount;
    public float speed;

    private Vector3 _initialPos;
    private IEnumerator _shakeAnimation;

    private void Awake()
    {
        PongBall.OnBallCollision += Shake;
        _initialPos = transform.position;
    }

    private void Shake()
    {
        if(_shakeAnimation != null)
        {
            StopAllCoroutines();
            transform.position = _initialPos;
        }
        _shakeAnimation = PerlinShake();
        StartCoroutine(_shakeAnimation);
    }

    private IEnumerator PerlinShake()
    {
        float timePassed = 0;
        float horizontalShakeAmount = 0;
        do
        {
            timePassed += Time.deltaTime;
            float randX = Random.Range(0, 1f);
            float randY = Random.Range(0, 1f);
            horizontalShakeAmount = Mathf.PerlinNoise(randX, randY);

            float finalOffset = (horizontalShakeAmount - 0.5f) * 2f; 
            transform.position = new Vector3(finalOffset, transform.position.y, transform.position.z);
            yield return null;
        } while (timePassed < 0.15f);
        transform.position = _initialPos;
    }

    private IEnumerator ShakeHelper()
    {
        // Lerping a value that represents this object's x position.
        float timePassed = 0;
        do
        {
            timePassed += Time.deltaTime;
            transform.position = Vector3.Lerp(_initialPos + (Vector3.left * shakeAmount) , _initialPos, timePassed * speed);
            yield return null;
        } while(timePassed < 0.2f);
    }
}
