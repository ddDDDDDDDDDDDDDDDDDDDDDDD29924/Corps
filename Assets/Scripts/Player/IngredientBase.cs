using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

public class IngredientBase
{
    [SerializeField] protected IngredientData ingredientData;

    private GameObject ingredientPrefab => ingredientData.ingredientPrefab;
    private Vector3 ingredientScale => ingredientData.ingredientScale;

    private string ingredientName => ingredientData.ingredientName;
    private string description => ingredientData.description;

    private float mass => ingredientData.mass;
    private IngredientType ingredientType => ingredientData.ingredientType;
    private Ingredient ingredient => ingredientData.ingredient;

    public bool allowedToCombine = false;

}
