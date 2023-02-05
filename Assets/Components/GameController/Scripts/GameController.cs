using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    private const int _totalDays = 4;
    private const int _goodIngredientScore = 1;
    private const int _badIngredientScore = -2;
    private const int _goodScore = 4;
    private const int _neutralScore = 2;
    private const int _saladPrice = 8;
    private const int _soupPrice = 8;
    private const int _stirFryPrice = 8;
    private const float _timeReductionPerDay = 5f;
    private const float _startingTime = 30f;
    private const int _disappointedScoreDeduction = 14;
    private const int _unimpressedScoreDeduction = 8;

    private int _score = 100;

    private float _timeLeft = _startingTime;

    private int _day = 0;
    private int _completedOrders = 0;
    private int _totalOrdersToday = 3;
    private int _ordersCompletedToday = 0;
    private int _dailyRating = 0;
    

    [SerializeField] private List<Order> _orderData;
    [SerializeField] private Note _dayNote;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private TMPro.TextMeshProUGUI _timer;

    private bool _orderOngoing = false;

    [SerializeField] private string[] TimeOutComments;

    private void Start()
    {
        Shuffle<Order>(_orderData);
        InitiateTutorial();
    }

    private void Update()
    {
        if(_orderOngoing)
        {
            _timeLeft -= Time.deltaTime;
            _timer.text = "Timer - " + _timeLeft.ToString("00.##");
            if(_timeLeft <= 0)
            {
                CompleteOrder(new Recipe(Recipe.DishType.Incomplete));
            }
        }
    }

    public void InitiateTutorial()
    {
        TutorialManager.Instance.NextSlide();
    }

    public void NewOrder()
    {
        if(_ordersCompletedToday >= _totalOrdersToday) EndDay();
        else 
        {
            _orderOngoing = true;
            _timer.gameObject.SetActive(true);
            NotesController.Instance.UpdateOrderNote(_orderData[_completedOrders].Preference);
            _timeLeft = _startingTime - (_day * _timeReductionPerDay);
        }
    }

    public void CompleteOrder() => CompleteOrder(new Recipe(Recipe.DishType.Salad));

    public void CompleteOrder(Recipe recipe)
    {
        _orderOngoing = false;
        _timer.gameObject.SetActive(false);

        if(recipe.Type == Recipe.DishType.Incomplete)
        {
            string str = TimeOutComments[Random.Range(0,TimeOutComments.Length-1)];
            NotesController.Instance.UpdateFeedbackNote(new Order.Note(str, Emotions.State.Disappointed));
            return;
        }

        int score = recipe.Ingredients.Count;
        //Debug.Log("start of the score " + score);
        foreach(IngredientData ingredient in _orderData[_completedOrders].GoodIngredients)
        {
            if(recipe.Ingredients.Contains(ingredient)) score += _goodIngredientScore;
        }
        //Debug.Log("after good ingredients " + score);


        foreach(IngredientData ingredient in _orderData[_completedOrders].BadIngredients)
        {
            if(recipe.Ingredients.Contains(ingredient)) score += _badIngredientScore;
        }
        //Debug.Log("after bad ingredients " + score);


        if(recipe.Type != _orderData[_completedOrders].Dishtype) score = _neutralScore - 1;
        //Debug.Log("after _neutralScore " + score);


        Order.Feedback feedback;

        if(score >= _goodScore) feedback = Order.Feedback.Good;
        else if (score >= _neutralScore) feedback = Order.Feedback.Neutral;
        else feedback = Order.Feedback.Bad;
        
        if(feedback == Order.Feedback.Good) 
            NotesController.Instance.UpdateFeedbackNote(_orderData[_completedOrders].GoodFeedback);
        else if(feedback == Order.Feedback.Neutral) 
        {
            NotesController.Instance.UpdateFeedbackNote(_orderData[_completedOrders].NeutralFeedback);
            _score -= _unimpressedScoreDeduction;
        }
        else 
        {
            NotesController.Instance.UpdateFeedbackNote(_orderData[_completedOrders].BadFeedback);
            _score -= _disappointedScoreDeduction;
        }

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
            GameOver.Instance.Trigger(_score);
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
