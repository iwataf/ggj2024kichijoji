using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] RectTransform[] _lifes = default;

    public void SetLifeValue(int val)
    {
        for(int i = 0; i < 3; ++i)
        {
            bool on = i < val;
            _lifes[i].Find("On").gameObject.SetActive(on);
            _lifes[i].Find("Off").gameObject.SetActive(!on);
        }
    }

}
