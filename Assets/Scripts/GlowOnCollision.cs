using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowOnCollision : MonoBehaviour
{
    private Material _material;
    private IEnumerator _glowRoutine;

    public float speed;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _material.EnableKeyword("_EMISSION");
    }

    public void AddGlow()
    {
        if(_glowRoutine != null)
        {
            StopCoroutine(_glowRoutine);
        }
        _glowRoutine = GlowRoutine();
        StartCoroutine(_glowRoutine);
    }

    public IEnumerator GlowRoutine()
    {
        float timePassed = 0;
        do
        {
            timePassed += Time.deltaTime;
            _material.SetColor("_EmissionColor", Color.Lerp(Color.white, Color.clear, timePassed * speed));
            yield return null;
        }
        while (_material.GetColor("_EmissionColor") != Color.clear);
    }

    private void OnCollisionEnter(Collision collision)
    {
        AddGlow();
    }
}
