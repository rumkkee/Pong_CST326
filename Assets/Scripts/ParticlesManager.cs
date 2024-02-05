using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    [SerializeField] ParticleSystem collisionParticlesPrefab;

    public static ParticlesManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void SpawnParticles(Vector3 spawnPos)
    {
        Instantiate(collisionParticlesPrefab, spawnPos, Quaternion.identity, transform.parent);
    }
}
