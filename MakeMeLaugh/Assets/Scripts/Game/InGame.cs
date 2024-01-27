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

    [SerializeField] private MessagePanel _messagePanel = default;
    [SerializeField] private AnswerPanel _answerPanel = default;
    [Header("Talk")]
    [SerializeField] private TalkSection[] _sections = default;

    private int _currentSectionIndex = 0;

    public void OnClickMessageWindow()
    {
        Debug.Log("Click");
        if (_currentSectionIndex + 1 < _sections.Length)
        {
            _currentSectionIndex += 1;
            ApplyTalk(_sections[_currentSectionIndex]);
        } else if (_answerPanel.gameObject.activeInHierarchy == false) {
            _messagePanel.TextName.text = "";
            _messagePanel.TextTalk.text = "回答の時間です";

            var choices = new string[] {
                "回答1", "回答2", "回答3",
            };
            _answerPanel.Setup(choices, (index) => {
                Debug.Log($"回答は{index}です。");
                _answerPanel.gameObject.SetActive(false);

                // Reset
                _currentSectionIndex = 0;
                ApplyTalk(_sections[_currentSectionIndex]);
            });
            _answerPanel.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        _answerPanel.gameObject.SetActive(false);
        Talk(_sections);
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
