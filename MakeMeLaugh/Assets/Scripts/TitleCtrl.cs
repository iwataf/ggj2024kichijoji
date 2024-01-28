using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCtrl : MonoBehaviour
{
    public void OnClickGameStart()
    {
        Sound.Instance.PlaySE(Sound.seValue.wadaiko);
        Fader.Instance.FadeOut(GameSceneLoad);
    }

    public void OnClickEnding()
    {
        EndingData.Instance.SetEndingType(EndingData.EndingType.BAD);
        Fader.Instance.FadeOut(EndingSceneLoad);
    }

    // Start is called before the first frame update
    void Start()
    {
        Sound.Instance.PlayBGM(Sound.bgmValue.title);
        Fader.Instance.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GameSceneLoad()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void EndingSceneLoad()
    {
        SceneManager.LoadScene("EndingScene");
    }
}
