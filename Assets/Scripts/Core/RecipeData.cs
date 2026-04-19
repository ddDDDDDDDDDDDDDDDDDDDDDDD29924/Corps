using UnityEngine;

[CreateAssetMenu(fileName = "RecipeData", menuName = "ScriptableObjects/RecipeData")]
public class RecipeData : ScriptableObject
{
    public GameObject IngredientA;
    public GameObject IngredientB;

    public GameObject Result;
}
