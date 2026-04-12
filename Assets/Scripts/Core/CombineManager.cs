using UnityEngine;

public class CombineManager : MonoBehaviour
{
    public enum Ingredients
    {
        Red_Block,
        Blue_Block,
        Purple_Block,
    }

    public enum Combination
    {
        
    }

    [SerializeField] private Combination[] combination;
}
