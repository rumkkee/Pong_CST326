using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreTextAnimator : MonoBehaviour
{
    [SerializeField] private float normalTextSize;
    [SerializeField] private float largeTextSize;
    [SerializeField] private float speedMultiplier;

    [SerializeField] private Color _glowColor;
    [SerializeField] private Color _normalColor;

    public static ScoreTextAnimator instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public IEnumerator GrowText(TextMeshProUGUI text)
    {
        // Anticipation: Normal to Shrunk text
        float timePassed = 0;
        float shrunkTextSize = normalTextSize * 0.4f;
        do
        {
            timePassed += Time.deltaTime;
            text.fontSize = Mathf.SmoothStep(normalTextSize, shrunkTextSize, timePassed * speedMultiplier);
            yield return null;
        }
        while(text.fontSize != shrunkTextSize);

        // Growing Text Size: Shrunk to Large text
        timePassed = 0;
        do
        {
            timePassed += Time.deltaTime;
            text.fontSize = Mathf.SmoothStep(shrunkTextSize, largeTextSize, timePassed * speedMultiplier * 2);
            text.color = Color.Lerp(_normalColor, _glowColor, timePassed * speedMultiplier * 2);
            yield return null;
        }
        while (text.fontSize != largeTextSize);

        // Follow-through: Large to Normal
        timePassed = 0;
        yield return null;
        do
        {
            timePassed += Time.deltaTime;
            text.fontSize = Mathf.SmoothStep(largeTextSize, normalTextSize, timePassed * speedMultiplier);
            yield return null;
        }
        while (text.fontSize != normalTextSize);

        // Fading Text Color back to normal
        timePassed = 0;
        do
        {
            timePassed += Time.deltaTime;
            text.color = Color.Lerp(_glowColor, _normalColor, timePassed * speedMultiplier * 0.1f);
            yield return null;
        }
        while (text.color != _normalColor);

    } 
}
