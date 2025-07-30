using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Game.Interactable;

public class LorePaperInteraction : MonoBehaviour, IInteractable
{
    [Header("Lore UI References")]
    public GameObject lorePaperPanel; // Assign in Inspector
    public TextMeshProUGUI loreText;  // Assign in Inspector
    public Button closeButton;        // Assign in Inspector

    [Header("Lore Text")]
    [TextArea]
    public string loreContent = "Lore goes here...";

    private bool isOpen = false;

    private void Awake()
    {
        if (lorePaperPanel != null)
            lorePaperPanel.SetActive(false);

        if (closeButton != null)
            closeButton.onClick.AddListener(CloseLorePaper);
    }

    public void Interact()
    {
        ShowLorePaper();
    }

    public Transform GetTransform() => transform;

    private void ShowLorePaper()
    {
        if (loreText != null)
            loreText.text = loreContent;

        if (lorePaperPanel != null)
            lorePaperPanel.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isOpen = true;
    }

    private void CloseLorePaper()
    {
        if (lorePaperPanel != null)
            lorePaperPanel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isOpen = false;
    }

    private void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseLorePaper();
        }
    }
}
