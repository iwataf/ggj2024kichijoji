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
        _endingImage.sprite = _endingSprites[(int)EndingData.Instance.GetEndingType()];
        Fader.Instance.FadeIn(() =>
        {
            StartCoroutine("EndSequence");
        });
    }

    private IEnumerator EndSequence()
    {
        Debug.Log("EndSequence");

        yield return new WaitForSeconds(3f);

        Fader.Instance.FadeOut(() =>
        {
            SceneManager.LoadScene("Title");
        });
    }
}
