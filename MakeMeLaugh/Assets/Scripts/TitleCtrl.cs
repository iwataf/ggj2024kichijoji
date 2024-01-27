using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCtrl : MonoBehaviour
{
    public void OnClickSampleScene()
    {
        Fader.Instance.FadeOut(SampleSceneLoad);
    }

    // Start is called before the first frame update
    void Start()
    {
        Fader.Instance.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SampleSceneLoad()
    {
        SceneManager.LoadScene("GameScene");
    }
}
