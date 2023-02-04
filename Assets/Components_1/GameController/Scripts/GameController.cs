using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    private int _day = 0;
    private int _completedOrders = 0;
    private int _totalOrdersToday = 3;
    private int _ordersCompletedToday = 0;
    private int _dailyRating = 0;
    

    [SerializeField] private List<Order> _orderData;
    [SerializeField] private Note _dayNote;
    [SerializeField] private GameObject _gameOverScreen;

    private void Start()
    {
        InitiateTutorial();
    }

    [ContextMenu("Initiate")]
    public void InitiateTutorial()
    {
        TutorialManager.Instance.NextSlide();
    }

    public void NewOrder()
    {
        if(_ordersCompletedToday >= _totalOrdersToday) EndDay();
        else NotesController.Instance.UpdateOrderNote(_orderData[_completedOrders].Preference);
    }

    public void CompleteOrder() => CompleteOrder(Order.Feedback.Bad);

    public void CompleteOrder(Order.Feedback feedback)
    {
        _ordersCompletedToday++;
        _completedOrders++;
        _dailyRating += (int)feedback;
        
        if(feedback == Order.Feedback.Good) 
            NotesController.Instance.UpdateFeedbackNote(_orderData[_completedOrders].GoodFeedback);
        else if(feedback == Order.Feedback.Neutral) 
            NotesController.Instance.UpdateFeedbackNote(_orderData[_completedOrders].NeutralFeedback);
        else 
            NotesController.Instance.UpdateFeedbackNote(_orderData[_completedOrders].BadFeedback);
    }

    private void EndDay()
    {
        if(_dailyRating > 0) _totalOrdersToday++;
        else if(_dailyRating < 0) _totalOrdersToday--;

        if(_totalOrdersToday == 0) _totalOrdersToday = 1;

        _day++;
        _dailyRating = 0;
        _ordersCompletedToday = 0;

        if(_day >= 5) 
        {
            _gameOverScreen.SetActive(true);
            return;
        }

        var note = new Order.Note("Day " + (_day + 1), Emotions.State.Default);
        _dayNote.UpdateContent(note);
    }
}
