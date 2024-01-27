using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerPanel : MonoBehaviour
{
    [SerializeField] Button[] _buttons = default;

    private Action<int> _clickCallback = default;

    private void Awake()
    {
        for(int i = 0; i < _buttons.Length; ++i)
        {
            int tmp = i;
            _buttons[i].onClick.AddListener(() => { _clickCallback(tmp); });
        }
    }

    public void Setup(string[] choices, Action<int> callback)
    {
        for(int i = 0; i < _buttons.Length; ++i)
        {
            _buttons[i].gameObject.GetComponentInChildren<TMPro.TMP_Text>().text = choices[i];
        }
        _clickCallback = callback;
    }
}
