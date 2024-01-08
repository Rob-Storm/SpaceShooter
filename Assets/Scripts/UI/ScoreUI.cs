using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    TMP_Text scoreText;

    private void Awake()
    {
        scoreText = GetComponentInChildren<TMP_Text>();

        Player.OnScoreChanged += OnScoreChanged;

        scoreText.text = $"Score: {Player.Score}";
    }

    private void OnScoreChanged(object sender, System.EventArgs e)
    {
        scoreText.text = $"Score: {Player.Score}";
    }
}
