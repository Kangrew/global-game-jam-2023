using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DialoguePacer : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;
    private string _content;

    [SerializeField] private UnityEvent _onDialogueComplete;

    private void Start()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void StartDialogue(string content, float timeBetweenText)
    {
        _content = content;
        StartCoroutine("StartDialogueCoroutine", timeBetweenText);
    }

    public void SkipPacing()
    {
        StopAllCoroutines();
        _textMesh.text = _content;
    }

    private IEnumerator StartDialogueCoroutine(float timeBetweenText)
    {
        int currentLength = 0;
        WaitForSeconds delay = new WaitForSeconds(timeBetweenText);
        while(currentLength < _content.Length)
        {
            _textMesh.text = _content.Substring(0, currentLength);
            currentLength++;
            yield return delay;
        }
        _onDialogueComplete.Invoke();
    }
}
