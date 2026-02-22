using UnityEngine;
using System;

public class PlayerProgression : MonoBehaviour
{
    [Header("Links")]
    [Tooltip("A link to PlayerStats for possible stats increasement during the level.")]
    [SerializeField] private PlayerStats playerStats;

    [Header("Level")]
    [Tooltip("Current level of the player.")]
    [SerializeField] private int currentLevel = 1;

    [Header("Experience")]
    [Tooltip("Current experience points of the player.")]
    [SerializeField] private float currentExperience = 0f;

    public int CurrentLevel => currentLevel;

    public float CurrentExperience => currentExperience;

    [Tooltip("Basic experience quality for start level increasement.")]
    [SerializeField] private float baseExperienceToNextLevel = 100f;

    [Tooltip("Experience growth factor for each level.")]
    [SerializeField] private float experienceGrowthFactor = 1.5f;

    public event Action<int> OnLevelUp;
    public event Action<float, float> OnExperienceChanged;

    private void Awake()
    {
        if (playerStats == null)
            Debug.LogWarning("PlayerStats reference is missing on PlayerProgression", this);

        float required = GetRequiredExperienceForNextLevel();
        OnExperienceChanged?.Invoke(currentExperience, required);
    }

    private float GetRequiredExperienceForNextLevel()
    {
        float required = baseExperienceToNextLevel;

        int power = Mathf.Max(0, currentLevel - 1);
        required *= Mathf.Pow(experienceGrowthFactor, power);

        return required;
    }

    public void AddExperience(float amount)
    {
        if (amount <= 0f) return;

        currentExperience += amount;

        bool leveledUpAtLeastOnce = false;

        while (true)
        {
            float required = GetRequiredExperienceForNextLevel();

            if (currentExperience < required) break;

            currentExperience -= required;
            LevelUpInternal();
            leveledUpAtLeastOnce = true;
        }

        float nextRequired = GetRequiredExperienceForNextLevel();
        OnExperienceChanged?.Invoke(currentExperience, nextRequired);

        if (leveledUpAtLeastOnce)
        {
            Debug.Log($"Player leveled up to level {currentLevel}");
        }
    }

    private void LevelUpInternal()
    {
        currentLevel++;

        OnLevelUp?.Invoke(currentLevel);
    }
}
