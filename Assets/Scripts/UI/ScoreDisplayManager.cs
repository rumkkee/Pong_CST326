using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplayManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _leftScoreText;
    [SerializeField] private TextMeshProUGUI _rightScoreText;

    public static ScoreDisplayManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void UpdateLeftScore(int newScore)
    {
        _leftScoreText.text = newScore.ToString();
    }

    public void UpdateRightScore(int newScore)
    {
        _rightScoreText.text = newScore.ToString();
    }
}
