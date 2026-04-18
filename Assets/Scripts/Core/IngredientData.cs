using UnityEngine;

public enum IngredientType
{
    Solid,
    Liquid,
    Gas
}

public enum Ingredient
{
    None,
    RedBlock,
    BlueBlock,
    PurpleBlock,
}

[CreateAssetMenu(fileName = "IngredientData", menuName = "Ingredients Data/Ingredients", order = 1)]

public class IngredientData : ScriptableObject
{
    [Header("Description")]
    public string ingredientName = "Ingredient Name";
    public string description = "Ingredient Description";

    [Header("Visuals")]
    public Vector3 ingredientScale = Vector3.one;
    public GameObject ingredientPrefab;
    public float visualEffectIntensity = 1f;

    [Header("Properties")]
    public float mass = 1f;
    public IngredientType ingredientType = IngredientType.Solid;
    public Ingredient ingredient = Ingredient.None;
}
