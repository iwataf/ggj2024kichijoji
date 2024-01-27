using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void SetScoreText(string score)
    {
        _scoreText.text = score;
    }

}
