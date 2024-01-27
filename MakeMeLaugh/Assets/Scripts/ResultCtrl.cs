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
        _resultView.SetScoreText(score + "/" + maxValue);
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
