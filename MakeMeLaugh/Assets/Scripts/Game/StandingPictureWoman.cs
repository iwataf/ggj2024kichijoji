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

    public void TogglePicture(int index, int levelIndex)
    {
        _sprite.sprite = _pictures[index].Levels[levelIndex];
    }
}
