using UnityEngine;

public class NotesController : Singleton<NotesController>
{
    [SerializeField] private Note _orderNoteContent;
    [SerializeField] private Note _feedbackNoteContent;

    public void UpdateOrderNote(Order.Note content) =>_orderNoteContent.UpdateContent(content);

    public void UpdateFeedbackNote(Order.Note content) => _feedbackNoteContent.UpdateContent(content);
}
