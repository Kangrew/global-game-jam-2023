using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientData", menuName = "ScriptableObjects/IngredientData")]
public class IngredientData : ScriptableObject
{
    [SerializeField] private string _ingredientName;
    [SerializeField] private string _ingredientToolTip;
    [SerializeField] private int _cost;
    [SerializeField] private Color _ingredientColor,_ingredientDarkcolor;

    public string IngredientName => _ingredientName;
    public int Cost => _cost;
    public string IngredientToolTip => _ingredientToolTip;
    public Color IngredientColor => _ingredientColor;
    public Color IngredientDarkColor => _ingredientDarkcolor;

}
