using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessagePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _textName = default;
    [SerializeField] private TMP_Text _textTalk = default;

    public TMP_Text TextName => _textName;
    public TMP_Text TextTalk => _textTalk;
}
