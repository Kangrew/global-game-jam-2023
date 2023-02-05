using UnityEngine;
using UnityEngine.Events;

public class NotesController : Singleton<NotesController>
{
    [SerializeField] private Note _orderNoteContent;
    [SerializeField] private Note _feedbackNoteContent;

    [SerializeField] private UnityEvent onPageShow;

    public void UpdateOrderNote(Order.Note content) 
    {
        _orderNoteContent.UpdateContent(content);
        onPageShow.Invoke();
    }

    public void UpdateFeedbackNote(Order.Note content) 
    {
        _feedbackNoteContent.UpdateContent(content);
        onPageShow.Invoke();
    }
}
