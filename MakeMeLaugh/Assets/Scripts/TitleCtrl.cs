using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCtrl : MonoBehaviour
{
    public void OnClickGameStart()
    {
        Fader.Instance.FadeOut(GameSceneLoad);
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
}
