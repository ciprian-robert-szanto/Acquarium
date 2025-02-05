using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class DialogPanelController : MonoBehaviour
{
    [SerializeField]
    private DialogConfiguration dialogConfiguration;
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private TextMeshProUGUI dialogText;
    [SerializeField]
    private Image dialogImage;
    [SerializeField]
    private GameObject continueText;
    [SerializeField]
    private CanvasGroup canvasGroup;

    private int currentDialogIndex = 0;

    private Action onConversationComplete = null;

    public void StartConversation(DialogConfiguration configuration, Action onComplete) {
        Show(true);

        dialogConfiguration = configuration;
        currentDialogIndex = 0;
        onConversationComplete = onComplete;

        RenderDialog();
    }
    
    private void RenderDialog() {
        Dialog currentDialogConfig = dialogConfiguration.dialogs[currentDialogIndex];
        titleText.text = currentDialogConfig.title;
        dialogText.text = currentDialogConfig.message;
        dialogImage.sprite = currentDialogConfig.image;

        continueText.SetActive(currentDialogIndex < dialogConfiguration.dialogs.Count - 1);
    }

    public void NextDialog() {
        currentDialogIndex++;

        if (currentDialogIndex >= dialogConfiguration.dialogs.Count) {
            // dialog flow completed
            Show(false);
            onConversationComplete?.Invoke();
            onConversationComplete = null;
            return;
        }

        RenderDialog();
    }

    public void Show(bool bShow) {
        canvasGroup.alpha = bShow ? 1 : 0;
        canvasGroup.interactable = bShow;
        canvasGroup.blocksRaycasts = bShow;
    }
}
