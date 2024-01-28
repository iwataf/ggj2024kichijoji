using System;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;

public class StandingPictureWoman : MonoBehaviour
{
    [Serializable]
    public class Picture {
        public Sprite[] Levels;
    }

    [SerializeField] private SpriteRenderer _sprite = default;
    [SerializeField] private Picture[] _pictures = default;
    [SerializeField] private Sprite _spriteReaction = default;

    public int NumPictures => _pictures.Length;
    public int GetMoodMax(int index) => _pictures[index].Levels.Length;
    public bool LockMood = true;

    private DG.Tweening.Sequence _agreementSequence = null;

    private float CalcWaitTime(int level)
    {
        var table =  new Vector2[] {
            new Vector2(3.0f, 4.0f),
            new Vector2(2.8f, 3.8f),
            new Vector2(2.6f, 3.6f),
            new Vector2(2.4f, 3.4f),
            new Vector2(2.2f, 3.2f),
        };

        return UnityEngine.Random.Range(table[level].x, table[level].y);
    }

    private int _level = 0;
    private int _moodIndex = 0;
    private float? _waitingSec = null;

    public void Setup()
    {
        _waitingSec = CalcWaitTime(_level);
    }

    private void Update()
    {
        if (LockMood || _waitingSec == null)
        {
            return;
        }

        _waitingSec -= Time.deltaTime;
        if (_waitingSec <= 0)
        {
            _waitingSec = null;

            var nextMood = (_moodIndex + 1) % GetMoodMax(_level);

            TogglePicture(_level, nextMood);

            _waitingSec = CalcWaitTime(_level);
        }
    }

    public void TogglePicture(int level, int moodIndex)
    {
        _sprite.sprite = _pictures[level].Levels[moodIndex];

        if (_agreementSequence == null && (_level != level || _moodIndex != moodIndex))
        {
            SetupAgreementsAnime();
        }

        _level = level;
        _moodIndex = moodIndex;
    }

    public void Reaction()
    {
        _sprite.sprite = _spriteReaction;

        if (_agreementSequence == null)
        {
            SetupAgreementsAnime();
        }
    }

    private void SetupAgreementsAnime()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(-0.5f, 0.2f).SetRelative());
        sequence.Append(transform.DOMoveY(0.5f, 0.2f).SetRelative());
        sequence.OnComplete(() => { _agreementSequence = null; });
        _agreementSequence = sequence;
    }
}
