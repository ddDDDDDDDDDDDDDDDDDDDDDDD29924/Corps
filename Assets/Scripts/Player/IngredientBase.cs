using UnityEngine;

public class IngredientBase : MonoBehaviour
{
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
        return (recipe.IngredientA.name == a.name && recipe.IngredientB.name == b.name) ||
               (recipe.IngredientA.name == b.name && recipe.IngredientB.name == a.name);
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
