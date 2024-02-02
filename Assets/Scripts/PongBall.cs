using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBall : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.left * speed, ForceMode.Impulse);
    }

    public void AddSpeed()
    {
        speed = speed + 5f;
    }
}
