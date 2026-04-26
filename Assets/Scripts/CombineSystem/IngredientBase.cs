using UnityEngine;

public class IngredientBase : MonoBehaviour
{
    public IngredientData Data;

    private void OnCollisionEnter(Collision collision)
    {
        IngredientBase otherObject = collision.gameObject.GetComponent<IngredientBase>();

        if (otherObject != null)
        {
            TryCombine(this.gameObject, otherObject.gameObject);
        }
    }

    private void TryCombine(GameObject obj1, GameObject obj2)
    {
        if (obj1.GetInstanceID() > obj2.GetInstanceID()) return;

        foreach (var recipe in RecipeStorage.Instance.RecipeDatas)
        {
            if (CheckMatch(recipe, obj1, obj2) && CheckGameState() && CheckWorktable(obj1, obj2))
            {
                SpawnResult(recipe, obj1, obj2);
                break;
            }
        }
    }

    private bool CheckMatch(RecipeData recipe, GameObject a, GameObject b)
    {
        IngredientBase RBaseA = recipe.IngredientA.GetComponent<IngredientBase>();
        IngredientBase RBaseB = recipe.IngredientB.GetComponent<IngredientBase>();
        IngredientBase ABase = a.GetComponent<IngredientBase>();
        IngredientBase BBase = b.GetComponent<IngredientBase>();

        return (RBaseA.Data.Type == ABase.Data.Type && RBaseB.Data.Type == BBase.Data.Type) ||
               (RBaseA.Data.Type == BBase.Data.Type && RBaseB.Data.Type == ABase.Data.Type);
    }

    private bool CheckGameState()
    {
        return GameManager.Instance.CurrentGameState == GameState.Playing;
    }

    private bool CheckWorktable(GameObject a, GameObject b)
    {
        GameObject worktable = RecipeStorage.Instance.Worktable;
        float combineRange = RecipeStorage.Instance.CombineRange;
        return Vector3.Distance(a.transform.position, worktable.transform.position) <= combineRange &&
               Vector3.Distance(b.transform.position, worktable.transform.position) <= combineRange;
    }

    private void SpawnResult(RecipeData recipe, GameObject a, GameObject b)
    {
        Vector3 spawnPos = (a.transform.position + b.transform.position) / 2f;
        Instantiate(recipe.Result, spawnPos, Quaternion.identity);
        Destroy(a);
        Destroy(b);
    }
}
