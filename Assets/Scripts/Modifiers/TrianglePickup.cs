using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrianglePickup : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _ballSpeedChange;

    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    private float timePassed;

    private Quaternion initRotation;
    private IEnumerator spinAnimation;

    private bool interactable;

    private void Awake()
    {
        initRotation = transform.rotation;
        interactable = true;
    }

    private void Update()
    {
        timePassed += Time.fixedDeltaTime;
        transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.PingPong(timePassed * 0.04f * _moveSpeed, 1));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!interactable) { return; }

        PongBall ball = other.gameObject.GetComponent<PongBall>();
        if(ball != null)
        {
            if(ball.GetPlayerAlignment() == Player.None) { return; }

            interactable = false;
            ModifyBallSpeed(ball);
            AudioManager.instance.PlayClipTriangleHit();
            StartCoroutine(SpinOnHit());
        }
    }

    private void ModifyBallSpeed(PongBall ball)
    {
        Rigidbody ballRB = ball.gameObject.GetComponent<Rigidbody>();
        if(_ballSpeedChange > 0) // making ball faster
        {
            if(ball.speed < ball.maxSpeed - _ballSpeedChange)
            {
                ball.speed += _ballSpeedChange;
            }
        }
        else // slowing ball
        {
            if(ball.speed > 35f)
            {
                ball.speed += _ballSpeedChange;
            }
            else if(ball.speed > 20f)
            {
                ball.speed += _ballSpeedChange * 0.5f;
            }
        }
        
        Vector2 currentDirection = ballRB.velocity.normalized;
        ballRB.velocity = Vector2.zero;
        ballRB.AddForce( currentDirection * ball.speed, ForceMode.Impulse);

    }

    private IEnumerator SpinOnHit()
    {
        if(spinAnimation != null)
        {
            StopCoroutine(spinAnimation);
        }
        spinAnimation = SpinOnHitHelper();
        yield return StartCoroutine(SpinOnHitHelper());
    }

    private IEnumerator SpinOnHitHelper()
    {
        transform.rotation = initRotation;
        float rotationPerSec = 20f;
        do 
        {
            transform.RotateAround(transform.position, Vector3.up, rotationPerSec);
            rotationPerSec -= Time.deltaTime * rotationPerSec * 1.5f;
            yield return new WaitForFixedUpdate();
        }while(rotationPerSec > 2f);
        Quaternion currentRotation = transform.rotation;
        float timePassed = 0f;
        do
        {
            timePassed += Time.deltaTime * rotationPerSec * 1.5f;
            transform.rotation = Quaternion.Lerp(currentRotation, initRotation, timePassed);
            yield return new WaitForFixedUpdate();
        } while (timePassed < 1f);
        transform.rotation = initRotation;
        interactable = true;
    }
}
