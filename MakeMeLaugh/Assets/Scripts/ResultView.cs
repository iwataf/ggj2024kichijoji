using TMPro;
using UnityEngine;

public class ResultView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void SetScoreText(string score)
    {
        _scoreText.text = score;
    }
}
