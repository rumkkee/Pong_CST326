using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class PongBall : MonoBehaviour
{
    private Rigidbody _rb;
    public float speed;
    public float speedBoostPerHit;

    public delegate void BallCollided();
    public static event BallCollided OnBallCollision;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Moves ball towards the given player. 
    /// For more dynamic gameplay, the ball's kickoff direction is then rotated randomly by -60 to 60 degrees.
    /// </summary>
    /// <param name="towardsPlayer"> the player the ball will sent towards </param>
    public void Kickoff(Player towardsPlayer)
    {
        Vector3 startingDirection = towardsPlayer == Player.Right ? Vector3.right : Vector3.left;
        float degreesToRotate = Random.Range(-60f, 60f);

        Vector3 rotatedDirection = Quaternion.AngleAxis(degreesToRotate, Vector3.forward) * startingDirection;
        rotatedDirection.Normalize();

        _rb.AddRelativeForce(rotatedDirection * speed, ForceMode.Impulse);
    }

    public void MoveBall()
    {
        _rb.AddForce(Vector3.left * speed, ForceMode.Impulse);
    }

    public void AddSpeed()
    {
        speed = speed + speedBoostPerHit;
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnBallCollision();
        ParticlesManager.instance.SpawnParticles(transform.position);
    }

}
