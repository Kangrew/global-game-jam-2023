using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : Singleton<GameOver>
{
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private Image _emoji;

    public void Trigger(int score)
    {
        _textMesh.text = "Your Reputation Score after 4 days is " + score.ToString() + "/100";
        if(score >= 70) _emoji.sprite = Emotions.Instance.GetEmoji(Emotions.State.Happy);
        else if(score >= 40) _emoji.sprite = Emotions.Instance.GetEmoji(Emotions.State.Unimpressed);
        else _emoji.sprite = Emotions.Instance.GetEmoji(Emotions.State.Disappointed);
    }
    public void ReloadScene() => SceneManager.LoadScene(0);
}
