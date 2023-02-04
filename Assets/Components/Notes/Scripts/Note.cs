using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Note : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textContent;
    [SerializeField] private Image _emoji;

    public void UpdateContent(Order.Note content)
    {

        _textContent.text = content.Content;
        _emoji.sprite = Emotions.Instance.GetEmoji(content.Emoji);
        SetVisible(true);
    }

    public void SetVisible(bool value) => gameObject.SetActive(value);
}
