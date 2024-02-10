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
        do
        {
            timePassed += Time.deltaTime;
            text.fontSize = Mathf.Lerp(largeTextSize, normalTextSize, timePassed * speedMultiplier);
            yield return null;
        }
        while (text.fontSize != normalTextSize);        
    } 
}