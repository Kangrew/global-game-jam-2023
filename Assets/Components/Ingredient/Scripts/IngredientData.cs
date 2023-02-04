using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientData", menuName = "ScriptableObjects/IngredientData")]
public class IngredientData : ScriptableObject
{
    [SerializeField] private string _ingredientName;

    public string IngredientName => _ingredientName;
}
