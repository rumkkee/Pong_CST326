using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeAmount;
    public float speed;

    private Vector3 initialPos;

    private IEnumerator shakeAnimation;

    private void Awake()
    {
        PongBall.OnBallCollision += Shake;
        initialPos = transform.position;
    }

    private void Shake()
    {
        if(shakeAnimation != null)
        {
            StopAllCoroutines();
            transform.position = initialPos;
        }
        shakeAnimation = ShakeHelper();
        StartCoroutine(shakeAnimation);
    }

    private IEnumerator ShakeHelper()
    {
        // Lerping a value that represents this object's x position.
        float timePassed = 0;
        do
        {
            timePassed += Time.deltaTime;
            transform.position = Vector3.Lerp(initialPos + (Vector3.left * shakeAmount) , initialPos, timePassed * speed);
            yield return null;
        } while(timePassed < 0.2f);
    }
}
