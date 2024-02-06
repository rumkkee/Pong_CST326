using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        _shakeAnimation = ShakeHelper();
        StartCoroutine(_shakeAnimation);
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
