using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    private Rigidbody rb;
    public float speed;
    private Vector3 _facingDirection;
    public Player owner;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _facingDirection = owner == Player.Left ? Vector3.right : Vector3.left; 
    }

    void FixedUpdate()
    {
        float verticalValue = owner == Player.Left ?  Input.GetAxis("LeftPaddle") : Input.GetAxis("RightPaddle");

        Vector3 force = new Vector3(transform.position.x, verticalValue, transform.position.y) * speed;

        rb.AddForce(force, ForceMode.Force);
    }

    public void SetTeam(Player player)
    {
        owner = player;
        _facingDirection = owner == Player.Left ? Vector3.right : Vector3.left;
    }

    private void OnCollisionEnter(Collision collision)
    {
        PongBall ball = collision.gameObject.GetComponent<PongBall>();
        if(ball != null)
        {
            Quaternion rotation = Quaternion.Euler(0f, 0f, 60f);
            Vector3 bounceDirection = rotation * _facingDirection;

            Rigidbody ballRB = collision.gameObject.GetComponent<Rigidbody>();

            rotation = Quaternion.Euler(0f, 0f, ballRB.rotation.z + 180f);
            //ballRB.MoveRotation(rotation);
            

            // TODO: Set a Force on the ball determined by the position it hit on the paddle

            // The Ball will be set to move either +/- 60 degrees

            float currentMagnitude = ballRB.velocity.magnitude;
            float updatedSpeed = currentMagnitude * 2f;

            ballRB.velocity = Vector3.zero;
            ball.AddSpeed();
            ballRB.AddForce(bounceDirection * ball.speed, ForceMode.Impulse);
        }
    }

}
