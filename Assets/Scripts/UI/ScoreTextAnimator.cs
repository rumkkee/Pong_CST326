using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreTextAnimator : MonoBehaviour
{
    [SerializeField] private float normalTextSize;
    [SerializeField] private float largeTextSize;
    [SerializeField] private float speedMultiplier;

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
        float timePassed = 0;

        float shrunkTextSize = normalTextSize * 0.4f;
        do
        {
            timePassed += Time.deltaTime;
            text.fontSize = Mathf.SmoothStep(normalTextSize, shrunkTextSize, timePassed * speedMultiplier);
            yield return null;
        }
        while(text.fontSize != shrunkTextSize);

        timePassed = 0;

        do
        {
            timePassed += Time.deltaTime;
            text.fontSize = Mathf.SmoothStep(shrunkTextSize, largeTextSize, timePassed * speedMultiplier * 2);
            yield return null;
        }
        while (text.fontSize != largeTextSize);

        timePassed = 0;

        yield return null;
        do
        {
            timePassed += Time.deltaTime;
            text.fontSize = Mathf.SmoothStep(largeTextSize, normalTextSize, timePassed * speedMultiplier * 1.5f);
            yield return null;
        }
        while (text.fontSize != normalTextSize);        
    } 
}
