using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HintUI : MonoBehaviour
{
    public static HintUI Instance;

    public GameObject hintPanel;
    public TextMeshProUGUI hintText;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        HideHint();
    }

    public void ShowHint(string message)
    {
        hintText.text = message;
        hintPanel.SetActive(true);
    }

    public void HideHint()
    {
        hintPanel.SetActive(false);
        hintText.text = "";
    }
}
