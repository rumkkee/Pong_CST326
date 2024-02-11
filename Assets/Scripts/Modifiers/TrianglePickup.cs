using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrianglePickup : MonoBehaviour
{
    [SerializeField] private Vector3 maxBoardHeight;
    [SerializeField] private Vector3 minBoardHeight;

    private float timePassed;
    private void Update()
    {
        //transform.RotateAround(transform.position, transform.up, 3f * Time.fixedDeltaTime);
        timePassed += Time.fixedDeltaTime;
        transform.position = Vector3.Lerp(minBoardHeight, maxBoardHeight, timePassed * 0.04f);
    }
    private void OnTriggerEnter(Collider other)
    {
        PongBall ball = other.gameObject.GetComponent<PongBall>();
        if(ball != null)
        {
            Debug.Log("Ball collided with triangle!");
            Rigidbody ballRB = ball.gameObject.GetComponent<Rigidbody>();
            float magnitude = ballRB.velocity.magnitude;

            ballRB.velocity = Vector2.zero;

            ballRB.AddForce(-transform.forward * magnitude, ForceMode.Impulse);
            Destroy(this.gameObject);
        }
    }
}
