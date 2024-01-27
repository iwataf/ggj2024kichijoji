using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class InGame : MonoBehaviour
{
    [Serializable]
    public class TalkSection
    {
        public string Name;
        [TextArea]
        public string Talk;
    }

    [Serializable]
    public class TalkData {
        public TalkSection[] Sections;
        public string[] Choices;
        public int CorrectAnswerIndex;
    }

    [SerializeField] private MessagePanel _messagePanel = default;
    [SerializeField] private AnswerPanel _answerPanel = default;
    [Header("Talk")]
    [SerializeField] private TalkData[] _talks = default;

    private TalkData _currentTalk = default;
    private int _currentSectionIndex = 0;

    public void OnClickMessageWindow()
    {
        Debug.Log("Click");
    }

    private void Start()
    {
        _answerPanel.gameObject.SetActive(false);

        // Talk
        _currentTalk = _talks.First();
        Talk(_currentTalk.Sections.First());
    }

    private void Talk(TalkSection section) {
        ApplyTalk(section);

        // 初期設定
        {
            var pos = _messagePanel.transform.position;
            pos.x = -1920;
            _messagePanel.transform.position = pos;

            _messagePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        // Animation
        var sequence = DOTween.Sequence();
        sequence.Append(_messagePanel.transform.DOLocalMoveX(0, 1.0f).OnComplete(OnEndEnterAnimation));
        sequence.Append(_messagePanel.transform.DOLocalMoveX(0, 2.0f).OnComplete(OnEndWaitAnimation));
        sequence.Append(_messagePanel.transform.DOLocalMoveX(1920, 1.0f).OnComplete(OnEndLeaveAnimation));

        _talkSequence = sequence;
    }

    private void ApplyTalk(TalkSection section)
    {
        _messagePanel.TextName.text = section.Name;
        _messagePanel.TextTalk.text = section.Talk;
    }

    private void OnEndEnterAnimation()
    {
        Debug.Log("移動完了");

        _messagePanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    private void OnEndWaitAnimation()
    {
        Debug.Log("待機終了");

        _messagePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    private void OnEndLeaveAnimation()
    {
        Debug.Log("移動完了");

        _currentSectionIndex = (_currentSectionIndex + 1) % _currentTalk.Sections.Length;
        Talk(_currentTalk.Sections[_currentSectionIndex]);
    }
}
