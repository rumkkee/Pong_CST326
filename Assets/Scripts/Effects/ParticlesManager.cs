using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _collisionParticlesPrefab;
    [SerializeField] private ParticleSystem _victoryParticlesPrefab;

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
        Instantiate(_collisionParticlesPrefab, spawnPos, Quaternion.identity, transform.parent);
    }

    public void SpawnParticles(Vector3 spawnPos, Color color)
    {
        ParticleSystem particles = Instantiate(_victoryParticlesPrefab, spawnPos, Quaternion.identity, transform.parent);
        var main = particles.main;
        main.startColor = color;
    }
}
