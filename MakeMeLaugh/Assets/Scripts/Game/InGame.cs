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
        public string ContainsGags = string.Empty;

        public bool IsContainsGags()
        {
            return ContainsGags != string.Empty;
        }
    }

    [Serializable]
    public class TalkData {
        public TalkSection[] Sections;
    }

    [SerializeField] private MessagePanel _messagePanel = default;
    [Header("Effect")]
    [SerializeField] private GameObject _correctPref = default;
    [SerializeField] private GameObject _incorrectPref = default;
    [Header("Talk")]
    [SerializeField] private TalkData[] _talks = default;

    private TalkData _currentTalk = default;
    private int _currentSectionIndex = 0;
    private bool _answered = false;

    public void OnClickMessageWindow()
    {
        GameObject pref = _currentTalk.Sections[_currentSectionIndex].IsContainsGags() ? _correctPref : _incorrectPref;

        var mousePos = Input.mousePosition;
        var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0f));

        var effect = Instantiate(pref);
        effect.transform.position = new Vector3(worldPos.x, worldPos.y, 0);
        effect.transform.localScale = Vector3.zero;

        var sequence = DOTween.Sequence();
        sequence.Append(effect.transform.DOScale(4.0f, 0.7f).SetEase(Ease.InSine));
        sequence.Join(effect.transform.DORotate(new Vector3(0, 0, 720), 0.7f, RotateMode.WorldAxisAdd));
        sequence.Append(effect.transform.DOScale(3.8f, 0.1f));
        sequence.Append(effect.transform.DOScale(4.0f, 0.1f));
        sequence.Append(effect.transform.DOScale(3.8f, 0.1f));
        sequence.Append(effect.transform.DOScale(4.0f, 0.1f));
        sequence.Append(effect.transform.DOScale(0.0f, 0.3f).SetEase(Ease.OutSine));

        sequence.OnComplete(() => { Destroy(effect); });

        if (!_answered && _currentTalk.Sections[_currentSectionIndex].IsContainsGags())
        {
            _answered = true;

            var correctText = _currentTalk.Sections[_currentSectionIndex].ContainsGags;
            var text = _messagePanel.TextTalk.text;
            _messagePanel.TextTalk.text = text.Replace(correctText, $"<color=\"red\">{correctText}</color>");
        }
    }

    private void Start()
    {
        // Talk
        _currentTalk = _talks.First();
        Talk(_currentTalk.Sections.First());
    }

    private void Talk(TalkSection section) {
        ApplyTalk(section);

        // 初期設定
        {
            var pos = _messagePanel.transform.localPosition;
            pos.x = -1920;
            _messagePanel.transform.localPosition = pos;

            _messagePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        // Animation
        var sequence = DOTween.Sequence();
        sequence.Append(_messagePanel.transform.DOLocalMoveX(0, 1.0f).OnComplete(OnEndEnterAnimation));
        sequence.Append(_messagePanel.transform.DOLocalMoveX(0, 2.0f).OnComplete(OnEndWaitAnimation));
        sequence.Append(_messagePanel.transform.DOLocalMoveX(1920, 1.0f).OnComplete(OnEndLeaveAnimation));
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

        _answered = false;
    }
}
