using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EventNames;

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

        EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_CAMERA_MOVEMENT_DISABLED);
        EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_PLAYER_MOVEMENT_DISABLED);
        EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_CONTROLS_DISABLED);
        InteractionPrompt.Instance.HidePrompt();

        //if ()
        //HideHint();
    }

    public void HideHint()
    {
        hintPanel.SetActive(false);
        hintText.text = "";

        EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_CAMERA_MOVEMENT_ENABLED);
        EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_PLAYER_MOVEMENT_ENABLED);
        EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_CONTROLS_ENABLED);
    }
}
