using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{

    private Rigidbody rb;
    public float speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float verticalValue = Input.GetAxis("Vertical");

        Vector3 force = new Vector3(transform.position.x, verticalValue, transform.position.y) * speed;
        //transform.position += new Vector3(transform.position.x, verticalValue, transform.position.z) * speed * Time.deltaTime;

        rb.AddForce(force, ForceMode.Force);

        /*if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = Vector3.right;
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.velocity = Vector3.left;
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        PongBall ball = collision.gameObject.GetComponent<PongBall>();
        if(ball != null)
        {
            Quaternion rotation = Quaternion.Euler(0f, 0f, 60f);
            Vector3 bounceDirection = rotation * Vector3.right;

            Rigidbody ballRB = collision.gameObject.GetComponent<Rigidbody>();
            
            

            // TODO: Set a Force on the ball determined by the position it hit on the paddle

            // The Ball will be set to move either +/- 60 degrees

            float currentMagnitude = ballRB.velocity.magnitude;
            float updatedSpeed = currentMagnitude * 1.2f;

            ballRB.velocity = Vector3.zero;
            ballRB.AddForce(bounceDirection * currentMagnitude, ForceMode.Impulse);
        }
    }

}
