using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    private const int _totalDays = 4;
    private const int _goodIngredientScore = 1;
    private const int _badIngredientScore = -2;
    private const int _goodScore = 4;
    private const int _neutralScore = 2;
    private const int _saladPrice = 12;
    private const int _soupPrice = 14;
    private const int _stirFryPrice = 16;

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
        Shuffle<Order>(_orderData);
        InitiateTutorial();
    }

    public void InitiateTutorial()
    {
        TutorialManager.Instance.NextSlide();
    }

    public void NewOrder()
    {
        if(_ordersCompletedToday >= _totalOrdersToday) EndDay();
        else NotesController.Instance.UpdateOrderNote(_orderData[_completedOrders].Preference);
    }

    public void CompleteOrder() => CompleteOrder(new Recipe(Recipe.DishType.Salad));

    public void CompleteOrder(Recipe recipe)
    {
        int score = recipe.Ingredients.Count;

        foreach(IngredientData ingredient in _orderData[_completedOrders].GoodIngredients)
        {
            if(recipe.Ingredients.Contains(ingredient)) score += _goodIngredientScore;
        }

        foreach(IngredientData ingredient in _orderData[_completedOrders].GoodIngredients)
        {
            if(recipe.Ingredients.Contains(ingredient)) score += _badIngredientScore;
        }

        if(recipe.Type != _orderData[_completedOrders].Dishtype) score = _neutralScore - 1;

        Order.Feedback feedback;

        if(score >= _goodScore) feedback = Order.Feedback.Good;
        else if (score >= _neutralScore) feedback = Order.Feedback.Neutral;
        else feedback = Order.Feedback.Bad;
        
        if(feedback == Order.Feedback.Good) 
            NotesController.Instance.UpdateFeedbackNote(_orderData[_completedOrders].GoodFeedback);
        else if(feedback == Order.Feedback.Neutral) 
            NotesController.Instance.UpdateFeedbackNote(_orderData[_completedOrders].NeutralFeedback);
        else 
            NotesController.Instance.UpdateFeedbackNote(_orderData[_completedOrders].BadFeedback);

        _ordersCompletedToday++;
        _completedOrders++;
        _dailyRating += (int)feedback;

        if(_orderData[_completedOrders].Dishtype == Recipe.DishType.Salad) CashController.Instance.Credit(_saladPrice);
        else if(_orderData[_completedOrders].Dishtype == Recipe.DishType.Soup) CashController.Instance.Credit(_soupPrice);
        else if(_orderData[_completedOrders].Dishtype == Recipe.DishType.StirFry) CashController.Instance.Credit(_stirFryPrice);
    }

    private void EndDay()
    {
        if(_dailyRating > 0) _totalOrdersToday++;
        else if(_dailyRating < 0) _totalOrdersToday--;

        if(_totalOrdersToday == 0) _totalOrdersToday = 1;

        _day++;
        _dailyRating = 0;
        _ordersCompletedToday = 0;

        if(_day >= _totalDays) 
        {
            _gameOverScreen.SetActive(true);
            return;
        }

        var note = new Order.Note("Day " + (_day + 1), Emotions.State.Default);
        _dayNote.UpdateContent(note);
    }

    public void Shuffle<T>(IList<T> list)  
    {
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = Random.Range(0 , n);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }
}
