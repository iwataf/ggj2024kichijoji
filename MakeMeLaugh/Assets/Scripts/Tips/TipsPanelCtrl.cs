using System;
using UnityEngine;
using DG.Tweening;

public class TipsPanelCtrl : MonoBehaviour
{

    [SerializeField] private TipsPanelView tipsPanelView;
    [SerializeField] private CanvasGroup _canvasGroup = default;

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

        _canvasGroup.interactable = false;
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, 0.25f).OnComplete(() => { _canvasGroup.interactable = true; });
    }

    private void OnClosePanel()
    {
        _canvasGroup.interactable = false;
        _canvasGroup.DOFade(0, 0.25f).OnComplete(() =>
        {
            closeAction?.Invoke();
            this.gameObject.SetActive(false);
        });
    }
}
