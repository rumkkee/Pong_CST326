using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class PongBall : MonoBehaviour
{
    private Rigidbody _rb;
    public float speed;
    public float speedBoostPerHit;
    public float maxSpeed;

    [SerializeField] TrailRenderer _trailRenderer;
    private Player playerAlignment;

    public delegate void BallCollided();
    public static event BallCollided OnBallCollision;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        playerAlignment = Player.None;
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

    public void AddSpeed()
    {
        speed = speed + speedBoostPerHit;
    }

    public void SetColor(Color color)
    {
        Material material = GetComponent<MeshRenderer>().material;
        material.color = Color.Lerp(Color.white, color, 0.8f);

        _trailRenderer.endColor = new Color(color.r, color.g, color.b, 0);
    }

    public void SetAlignment(Player player)
    {
        playerAlignment = player;
    }

    public Player GetAlignment() => playerAlignment;

    private void OnCollisionEnter(Collision other)
    {
        OnBallCollision();
        ParticlesManager.instance.SpawnParticles(transform.position);

        if (other.gameObject.CompareTag("Barrier"))
        {
            //TODO: Play audio for colliding with barrier
            AudioManager.instance.PlayClipBarrierHit();
        }
        else if (other.gameObject.CompareTag("Paddle"))
        {
            float pitch = 2;
            pitch *= speed / maxSpeed;
            
            AudioManager.instance.PlayClipPaddleHit(pitch);
        }
    }

    public Color GetColor()
    {
        return GetComponent<MeshRenderer>().material.color;
    }

}
