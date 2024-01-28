using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingScene : MonoBehaviour
{
    [SerializeField] private Image _endingImage;
    [SerializeField] private Sprite[] _endingSprites;

    private void Start()
    {
        var endingType = EndingData.Instance.GetEndingType();
        _endingImage.sprite = _endingSprites[(int)EndingData.Instance.GetEndingType()];
        Fader.Instance.FadeIn(() =>
        {
            Sound.Instance.StopBGM();
            switch (endingType)
            {
                case EndingData.EndingType.BAD:
                    {
                        Sound.Instance.PlaySE(Sound.seValue.badEnd);
                        StartCoroutine("BadEndSequence");
                        break;
                    }
                case EndingData.EndingType.NORMAL:
                    {
                        Sound.Instance.PlaySE(Sound.seValue.goodEnd);
                        StartCoroutine("NormalEndSequence");
                        break;
                    }
                case EndingData.EndingType.GOOD:
                    {
                        Sound.Instance.PlaySE(Sound.seValue.goodEnd);
                        StartCoroutine("GoodEndSequence");
                        break;
                    }
            }
        });
    }

    private IEnumerator BadEndSequence()
    {
        Debug.Log("BadEndSequence");

        yield return new WaitForSeconds(5f);

        Fader.Instance.FadeOut(() =>
        {
            SceneManager.LoadScene("Title");
        });
    }

    private IEnumerator NormalEndSequence()
    {
        Debug.Log("NormalEndSequence");

        yield return new WaitForSeconds(5f);

        Fader.Instance.FadeOut(() =>
        {
            SceneManager.LoadScene("Title");
        });
    }

    private IEnumerator GoodEndSequence()
    {
        Debug.Log("GoodEndSequence");

        // いい感じに待つ
        yield return new WaitForSeconds(3f);

        Sound.Instance.PlaySE(Sound.seValue.goodEndVoice);

        yield return new WaitForSeconds(2f);

        Fader.Instance.FadeOut(() =>
        {
            SceneManager.LoadScene("Title");
        });
    }
}
