using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fader : MonoBehaviour
{
    private Color fadeInColor = new Color(0,0,0,0);
    private Color fadeOutColor = new Color(0,0,0,1);

    public bool isFade;

    private static Fader instance;
    public static Fader Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("Fader Instance Error");
            }
            else
            {
                return instance;
            }
            return null;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {          
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        //var obj = this.transform;
    }

    public void FadeIn()
    {
        var fader = this.gameObject;
        Sequence sequence = DOTween.Sequence();
        var Image = fader.transform.GetComponent<Image>();
        sequence.Append(Image.DOColor ( fadeInColor,1.0f ))
                .OnComplete(() =>
                {
                    isFade = false;
                    fader.SetActive(false);
                });
        isFade = true;
    }

    public void FadeOut(System.Action action)
    {
        var fader = this.gameObject;
        fader.SetActive(true);
        Sequence sequence = DOTween.Sequence();
        var Image = fader.transform.GetComponent<Image>();
        sequence.Append(Image.DOColor ( fadeOutColor,1.0f ))
                .OnComplete(() =>
                {
                    isFade = false;
                    if(action != null)
                    {
                        action();
                    } 
                });
        isFade = true;
    }
}