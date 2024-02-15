using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrianglePickup : MonoBehaviour
{
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
        //transform.RotateAround(transform.position, transform.up, 3f * Time.fixedDeltaTime);
        timePassed += Time.fixedDeltaTime;
        transform.position = Vector3.Lerp(minBoardHeight, maxBoardHeight, Mathf.PingPong(timePassed * 0.04f, 1));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SpinOnHit());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Interactable:" + interactable);
        PongBall ball = other.gameObject.GetComponent<PongBall>();
        if(ball != null && interactable)
        {
            interactable = false;
            SlowDownBall(ball);
            StartCoroutine(SpinOnHit());
        }
    }

    private void SlowDownBall(PongBall ball)
    {
        Rigidbody ballRB = ball.gameObject.GetComponent<Rigidbody>();

        if(ball.speed > 35f)
        {
            ball.speed -= 5f;
        }
        else if(ball.speed > 20f)
        {
            ball.speed -= 3f;
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
