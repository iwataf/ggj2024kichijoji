using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGame : MonoBehaviour
{
    [SerializeField] private InGameUI _ingameUI = default;
    [SerializeField] private ResultCtrl _resultCtrl = default;
    [SerializeField] private TipsPanelCtrl _tipsPanelCtrl = default;
    [SerializeField] private MessagePanel _messagePanel = default;
    [SerializeField] private StandingPicture _characterOji = default;
    [Header("Effect")]
    [SerializeField] private GameObject _correctPref = default;
    [SerializeField] private GameObject _incorrectPref = default;
    [SerializeField] private GameObject _concentrationLine = default;
    [Header("Talk")]
    [SerializeField] private TalkDataScriptable[] _talkMaster = default;

    private TalkDataScriptable[] _talks = default;
    private TalkDataScriptable _currentTalk => _talks[_currentTalkIndex];
    private int _currentTalkIndex = 0;

    private int _currentSectionIndex = 0;
    private bool _answered = false;
    private Sequence _messageSequence = null;

    private int _lifes = 3;
    private int _numCorrectAnswer = 0;

    public void OnClickMessageWindow()
    {
        // Create effect
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

            var line = Instantiate(_concentrationLine);
            line.transform.position = new Vector3(worldPos.x, worldPos.y, 0);
        }

        // 正解
        if (!_answered && _currentTalk.Sections[_currentSectionIndex].IsContainsGags())
        {
            _answered = true;

            _numCorrectAnswer += 1;

            var correctText = _currentTalk.Sections[_currentSectionIndex].ContainsGags;
            var text = _messagePanel.TextTalk.text;
            _messagePanel.TextTalk.text = text.Replace(correctText, $"<color=\"red\">{correctText}</color>");

            _characterOji.SetEmotion(StandingPicture.Emotion.Smile);

            // TODO: 正解SEを鳴らす
        }

        // 不正解
        var isGameOver = false;

        if (!_answered && !_currentTalk.Sections[_currentSectionIndex].IsContainsGags())
        {
            _answered = true;

            if (_lifes == 0)
            {
                isGameOver = true;
                Debug.Log("Game Over!");
            }

            _lifes = Math.Max(0, _lifes - 1);
            _ingameUI.SetLifeValue(_lifes);

            // TODO: 不正解SEを鳴らす
        }

        // Stop Animation
        {
            _messageSequence.Kill();
            _messageSequence = null;

            _messagePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;

            TweenCallback callback = () =>
            {
                if (isGameOver)
                {
                    OpenResult();
                }
                else
                {
                    OnEndLeaveAnimation();
                }
            };

            // Animation
            var sequence = DOTween.Sequence();
            sequence.Append(_messagePanel.transform.DOLocalMoveX(0, 1.5f + 0.3f).OnComplete(OnEndWaitAnimation));
            sequence.Append(_messagePanel.transform.DOLocalMoveX(1920, 0.8f).OnComplete(callback).SetEase(Ease.OutSine));
        }
    }

    private void Start()
    {
        SetupMessagePanelStartPotision();

        // Talk
        _currentTalkIndex = 0;
        _ingameUI.SetLifeValue(_lifes);
        _talks = _talkMaster.OrderBy(i => Guid.NewGuid()).Take(5).ToArray();

        Sound.Instance.PlayBGM(Sound.bgmValue.game);

        Fader.Instance.FadeIn(() =>
        {
            OpenTips(() =>
            {
                Talk(_currentTalk.Sections.First());
            });
        });
    }

    private void SetupMessagePanelStartPotision()
    {
        var pos = _messagePanel.transform.localPosition;
        pos.x = -1920;
        _messagePanel.transform.localPosition = pos;

        _messagePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    private void Talk(TalkDataScriptable.TalkSection section)
    {
        ApplyTalk(section);

        // 初期設定
        SetupMessagePanelStartPotision();

        var waitTime = section.Talk.Length * 0.05f + 1.0f;

        // Animation
        var sequence = DOTween.Sequence();
        sequence.Append(_messagePanel.transform.DOLocalMoveX(0, 0.8f).OnComplete(OnEndEnterAnimation).SetEase(Ease.InSine));
        sequence.Append(_messagePanel.transform.DOLocalMoveX(0, waitTime).OnComplete(OnEndWaitAnimation));
        sequence.Append(_messagePanel.transform.DOLocalMoveX(1920, 0.8f).OnComplete(OnEndLeaveAnimation).SetEase(Ease.OutSine));
        _messageSequence = sequence;

        // Character
        _characterOji.SetEmotion(StandingPicture.Emotion.Standard);
    }

    private void ApplyTalk(TalkDataScriptable.TalkSection section)
    {
        _messagePanel.TextName.text = section.Name;
        _messagePanel.TextTalk.text = section.Talk;
    }

    private void OnEndEnterAnimation()
    {
        _messagePanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    private void OnEndWaitAnimation()
    {
        _messagePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    private void OnEndLeaveAnimation()
    {
        if (!_answered && _currentTalk.Sections[_currentSectionIndex].IsContainsGags())
        {
            Debug.Log("Through");
        }

        if (_currentTalk.Sections.Length <= _currentSectionIndex + 1)
        {
            if (_talks.Length <= _currentTalkIndex + 1)
            {
                OpenResult();
            }
            else
            {
                _currentTalkIndex = (_currentTalkIndex + 1) % _talks.Length;
                _currentSectionIndex = 0;

                Talk(_currentTalk.Sections[_currentSectionIndex]);
            }
        }
        else
        {
            _currentSectionIndex = (_currentSectionIndex + 1) % _currentTalk.Sections.Length;
            Talk(_currentTalk.Sections[_currentSectionIndex]);
        }

        _answered = false;
    }

    private void OpenResult()
    {
        int sumQuestion = 0;
        foreach (var talk in _talks)
        {
            sumQuestion += talk.Sections.Count(s => s.IsContainsGags());
        }

        _resultCtrl.OpenResult(_numCorrectAnswer, sumQuestion, (val) =>
        {
            Fader.Instance.FadeOut(() => { SceneManager.LoadScene("Title"); });
        });
    }

    private void OpenTips(Action closeAction)
    {
        _tipsPanelCtrl.OpenTips(closeAction);
    }
}
