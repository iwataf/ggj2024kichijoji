using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePanel : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _textTitle = default;

    public TMPro.TMP_Text TextTitle => _textTitle;
}
