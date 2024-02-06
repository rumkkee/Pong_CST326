using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    private Rigidbody _rb;
    [SerializeField] private float _speed;

    private Vector3 _facingDirection;
    private float _length;

    public Player owner;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _length = transform.localScale.y;
    }

    void FixedUpdate()
    {
        float verticalValue = owner == Player.Left ?  Input.GetAxis("LeftPaddle") : Input.GetAxis("RightPaddle");

        Vector3 force = new Vector3(transform.position.x, verticalValue, transform.position.y) * _speed;

        _rb.AddForce(force, ForceMode.Force);
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
            // Determine where the ball hit the paddle on a scale of 1 to -1.
            float collisionPos = ball.transform.position.y - transform.position.y;
            float collisionOffset = collisionPos;

            // Ensuring collisionOffset is within -2.5 to 2.5;
            if(collisionPos > _length / 2)
            {
                collisionOffset = _length / 2;
            }
            else if(collisionPos < -_length / 2)
            {
                collisionOffset = -_length / 2;
            }

            // Converting collisionOffset (-2.5 to 2.5) to a range from -1 to 1.
            float offsetNormalized = collisionOffset / (_length * 0.5f);

            // Flipping the rotation degrees based on whether the paddle is facing left or right.
            float rotationDegrees = offsetNormalized * 60f;  
            rotationDegrees *= owner == Player.Left ? 1 : -1;

            // Use the difference to determine the ball's new direction.
            Quaternion rotation = Quaternion.AngleAxis(rotationDegrees, Vector3.forward);
            Vector3 newDirection = rotation * _facingDirection;

            newDirection.Normalize();

            Rigidbody ballRB = collision.gameObject.GetComponent<Rigidbody>();

            ballRB.velocity = Vector3.zero;
            ball.AddSpeed();
            ballRB.AddForce(newDirection * ball.speed, ForceMode.Impulse);
        }
    }

}
