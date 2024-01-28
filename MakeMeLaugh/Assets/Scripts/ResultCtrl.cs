using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ResultCtrl : MonoBehaviour
{
    [SerializeField] private ResultView _resultView;

    private System.Action<bool> m_isClickCloseButton;

    public void OnClickResultCloseButton()
    {
        m_isClickCloseButton.Invoke(true);
    }

    void Start()
    {
    }

    public void OpenResult(int score, int maxValue, System.Action<bool> action)
    {
        var tabel = new string[] { "<sprite=9>", "<sprite=0>", "<sprite=1>", "<sprite=2>", "<sprite=3>", "<sprite=4>", "<sprite=5>", "<sprite=6>", "<sprite=7>", "<sprite=8>"};

        string scoreString = "";
        foreach(var s in score.ToString())
        {
            scoreString += tabel[int.Parse($"{s}")];
        }

        string maxValueString = "";
        foreach(var s in maxValue.ToString())
        {
            maxValueString += tabel[int.Parse($"{s}")];
        }

        _resultView.SetScoreText(scoreString + "<sprite=10>" + maxValueString);
        gameObject.SetActive(true);
        m_isClickCloseButton = action;

        // 遷移アニメーション
        {
            var pos = transform.position;
            pos.x = -1920;
            transform.position = pos;
        }
        transform.DOMoveX(0, 0.8f);
    }
}
