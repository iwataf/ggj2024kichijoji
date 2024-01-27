using System;
using UnityEngine;

public class TipsPanelCtrl : MonoBehaviour
{

    [SerializeField] private TipsPanelView tipsPanelView;

    private Action closeAction;

    private void Start()
    {
        tipsPanelView.CloseButton.onClick.AddListener(OnClosePanel);
        this.gameObject.SetActive(false);
    }

    public void OpenTips(Action closeAction)
    {
        this.closeAction = closeAction;
        this.gameObject.SetActive(true);
    }

    private void OnClosePanel()
    {
        closeAction?.Invoke();
        this.gameObject.SetActive(false);
    }
}
