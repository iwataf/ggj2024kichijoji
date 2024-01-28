using System;
using System.Linq;
using UnityEngine;

public class StandingPicture : MonoBehaviour
{
    public enum Emotion
    {
        Standard,
        Smile,
        Glasses,
    }

    [Serializable]
    public class PictureData
    {
        public Emotion Emotion;
        public Sprite Image;
    }

    [SerializeField] private SpriteRenderer _sprite = default;
    [SerializeField] private PictureData[] _pictures = default;

    public void SetEmotion(Emotion emotion)
    {
        var result = _pictures.Where(p => p.Emotion == emotion).FirstOrDefault();
        if (result == null)
        {
            _sprite.sprite = _pictures.First().Image;
        }
        else
        {
            _sprite.sprite = result.Image;
        }
    }
}
