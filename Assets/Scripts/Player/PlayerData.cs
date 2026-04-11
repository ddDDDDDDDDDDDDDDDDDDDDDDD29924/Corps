using UnityEngine;

[CreateAssetMenu(
    fileName = "PlayerData",
    menuName = "Game Data/Player Data",
    order = 0)]

public class PlayerData : ScriptableObject
{
    [Header("Основные характеристики")]
    [Min(1f)] public float maxHealth = 100f;

    [Header("Движение")]
    [Min(0f)] public float moveSpeed = 5f;
    [Min(0f)] public float jumpForce = 5f;

    [Header("Additional Movement Parameters")]
    [Min(0f)] public float acceleration = 10f;
    [Min(0f)] public float rotationSpeed = 720f; // Degrees per second

    [Header("Dragging")]
    [Min(1f)] public float dragRange = 3f;
    [Min(1f)] public float dragMinDistance = 1f;
    [Min(1f)] public float dragSensitivity = 1f;
    [Min(0f)] public float dragDelay = 2f;
    [Min(1f)] public float dragSpeed = 5f;
}