using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrianglePickup : MonoBehaviour
{

    [SerializeField] private float speedModAmount;

    [SerializeField] private Vector3 maxBoardHeight;
    [SerializeField] private Vector3 minBoardHeight;

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
        transform.position = Vector3.Lerp(minBoardHeight, maxBoardHeight, Mathf.PingPong(timePassed * 0.04f, 1));

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SpinOnHit());
        }*/
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Interactable:" + interactable);
        PongBall ball = other.gameObject.GetComponent<PongBall>();
        if(ball != null && ball.GetAlignment() != Player.None && interactable)
        {
            if(!interactable || ball.GetAlignment() == Player.None) { return; }

            interactable = false;
            ModifyBallSpeed(ball);
            AudioManager.instance.PlayClipTriangleHit();
            StartCoroutine(SpinOnHit());
        }
    }

    private void ModifyBallSpeed(PongBall ball)
    {
        Rigidbody ballRB = ball.gameObject.GetComponent<Rigidbody>();
        if(speedModAmount > 0) // making ball faster
        {
            if(ball.speed < ball.maxSpeed - speedModAmount)
            {
                ball.speed += speedModAmount;
            }
        }
        else // slowing ball
        {
            if(ball.speed > 35f)
            {
                ball.speed += speedModAmount;
            }
            else if(ball.speed > 20f)
            {
                ball.speed += speedModAmount * 0.5f;
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
        Debug.Log("Returned from Spinhelper!");
    }
}
