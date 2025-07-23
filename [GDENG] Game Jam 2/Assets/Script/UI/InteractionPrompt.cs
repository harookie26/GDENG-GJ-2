using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InteractionPrompt : MonoBehaviour
{
    public static InteractionPrompt Instance { get; private set; }
    public TextMeshProUGUI promptText;

    void Awake()
    {
        Instance = this;
        if (promptText == null)
            promptText = GetComponent<TextMeshProUGUI>();
        HidePrompt();
    }

    public void ShowPrompt(string message)
    {
        if (promptText != null)
        {
            promptText.text = message;
            promptText.enabled = true;
        }
    }

    public void HidePrompt()
    {
        if (promptText != null)
            promptText.enabled = false;
    }
}
