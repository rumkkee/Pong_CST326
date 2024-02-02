using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBall : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void MoveBall()
    {
        rb.AddForce(Vector3.left * speed, ForceMode.Impulse);
    }

    public void AddSpeed()
    {
        speed = speed + 5f;
    }
}
