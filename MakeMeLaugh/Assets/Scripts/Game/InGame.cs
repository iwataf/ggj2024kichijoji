using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        if (_currentSectionIndex + 1 < _currentTalk.Sections.Length)
        {
            _currentSectionIndex += 1;
            ApplyTalk(_currentTalk.Sections[_currentSectionIndex]);
        } else if (_answerPanel.gameObject.activeInHierarchy == false) {
            _messagePanel.TextName.text = "";
            _messagePanel.TextTalk.text = "回答の時間です";

            var choices = _currentTalk.Choices;
            _answerPanel.Setup(choices, (index) => {
                Debug.Log($"「{choices[index]}」を回答として選択。");

                if (_currentTalk.CorrectAnswerIndex == index)
                {
                    Debug.Log("正解です。");
                } else {
                    Debug.Log("不正解です。");
                }

                _answerPanel.gameObject.SetActive(false);

                // Reset
                _currentSectionIndex = 0;
                ApplyTalk(_currentTalk.Sections[_currentSectionIndex]);
            });
            _answerPanel.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        _answerPanel.gameObject.SetActive(false);

        // Talk
        _currentTalk = _talks.First();
        Talk(_currentTalk.Sections);

        Fader.Instance.FadeIn();
    }

    private void Talk(TalkSection[] sections) {
        var first = sections.First();
        ApplyTalk(first);
    }

    private void ApplyTalk(TalkSection section)
    {
        _messagePanel.TextName.text = section.Name;
        _messagePanel.TextTalk.text = section.Talk;
    }
}
