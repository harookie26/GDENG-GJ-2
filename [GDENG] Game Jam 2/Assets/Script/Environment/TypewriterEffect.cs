using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    public float typeSpeed = 0.05f; // Seconds between each letter

    private Coroutine typingCoroutine;

    public void StartTyping(string fullText, TextMeshPro textTarget)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(fullText, textTarget));
    }

    private IEnumerator TypeText(string fullText, TextMeshPro textTarget)
    {
        textTarget.text = "";
        foreach (char letter in fullText)
        {
            textTarget.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    public void Clear(TextMeshPro textTarget)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        textTarget.text = "";
    }
}
