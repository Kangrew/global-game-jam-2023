using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Order", menuName = "ScriptableObjects/Order")]
public class Order : ScriptableObject
{
    public enum Feedback
    {
        Good = 1,
        Neutral = 0, 
        Bad = -1
    }

    [Serializable] public struct Note
    {
        public Note(string content, Emotions.State emoji)
        {
            _content = content;
            _emoji = emoji;
        }

        [SerializeField][TextArea(4,8)] private string _content;
        [SerializeField] private Emotions.State _emoji;

        public string Content => _content;
        public Emotions.State Emoji => _emoji;
    }

    [SerializeField] private Note _preference;
    [SerializeField] private Note _goodFeedback;
    [SerializeField] private Note _neutralFeedback;
    [SerializeField] private Note _badFeedback;

    public Note Preference => _preference;
    public Note GoodFeedback => _goodFeedback;
    public Note NeutralFeedback => _neutralFeedback;
    public Note BadFeedback => _badFeedback;
}
