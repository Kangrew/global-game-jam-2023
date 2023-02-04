using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialManager : Singleton<TutorialManager>
{
    private const float _dialoguePace = 0.05f;

    [Serializable] public struct Slide
    {
        [SerializeField] private string _dialogue;
        [SerializeField] Emotions.State _emoji;

        public string Dialogue => _dialogue;
        public Emotions.State Emoji => _emoji;
    }

    [SerializeField] private Slide[] _slides;
    [SerializeField] private Image _emojiImage;
    [SerializeField] private DialoguePacer _dialoguePacer;

    [SerializeField] private UnityEvent _onTutorialCompleted;
    
    private int currentSlide = 0;

    // Returns True if Last Slide
    public void NextSlide()
    {
        _emojiImage.sprite = Emotions.Instance.GetEmoji(_slides[currentSlide].Emoji);
        _dialoguePacer.StartDialogue(_slides[currentSlide].Dialogue, _dialoguePace);
        currentSlide++;
        if(currentSlide >= _slides.Length) _onTutorialCompleted.Invoke();
    }
}
