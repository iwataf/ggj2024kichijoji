using System;
using UnityEngine;

public class StandingPictureWoman : MonoBehaviour
{
    [Serializable]
    public class Picture {
        public Sprite[] Levels;
    }

    [SerializeField] private SpriteRenderer _sprite = default;
    [SerializeField] private Picture[] _pictures = default;

    public int NumPictures => _pictures.Length;
    public int GetMoodMax(int index) => _pictures[index].Levels.Length;
    public bool LockMood = true;

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
        if (_waitingSec == null)
        {
            return;
        }

        _waitingSec -= Time.deltaTime;
        if (_waitingSec <= 0)
        {
            _waitingSec = null;

            if (!LockMood)
            {
                _moodIndex = (_moodIndex + 1) % GetMoodMax(_level);
                _sprite.sprite = _pictures[_level].Levels[_moodIndex];

                _waitingSec = CalcWaitTime(_level);
            }
        }
    }

    public void TogglePicture(int index, int levelIndex)
    {
        _level = index;
        _sprite.sprite = _pictures[index].Levels[levelIndex];
    }
}
