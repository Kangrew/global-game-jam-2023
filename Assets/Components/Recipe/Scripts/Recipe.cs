using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe
{
    public enum DishType
    {
        Salad,
        Soup,
        StirFry
    }

    private DishType _type;
    private List<IngredientData> _ingredients = new List<IngredientData>();

    public DishType Type => _type;

    public Recipe(DishType type) => _type = type;
    public List<IngredientData> Ingredients => _ingredients;

    public void AddIngredient(IngredientData ingredient) 
    {
        _ingredients.Add(ingredient);
        CashController.Instance.Deduct(ingredient.Cost);
    }

}
