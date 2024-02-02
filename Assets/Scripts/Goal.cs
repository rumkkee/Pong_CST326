using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        PongBall ball = collision.gameObject.GetComponent<PongBall>();
        if(ball != null)
        {
            // TODO: Signal that the ball has touched a goal.
        }
    }
}
