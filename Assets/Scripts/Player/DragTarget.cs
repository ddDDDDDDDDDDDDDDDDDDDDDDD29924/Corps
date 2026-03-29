using UnityEngine;
using System.Collections;

public class DragTarget : MonoBehaviour
{
    // Put into the Camera Target
    public PlayerData playerData;
    private GameObject targetObject;
    private bool isDragging = false;
    private Vector3 targetPoint;

    public LayerMask ignoreLayers;

    private float DragRange => playerData.dragRange;
    private float DragMinDistance => playerData.dragMinDistance;
    private float DragSensitivity => playerData.dragSensitivity;
    private float DragDelay => playerData.dragDelay;
    private float DragSpeed => playerData.dragSpeed;

    private void Update()
    {
        if (InputManager.Instance == null || GameManager.Instance.CurrentGameState != GameState.Playing)
            return;

        if (InputManager.Instance.IsDragHeld())
        {
            Debug.Log("Drag is held.");
        }
        else
        {
            Debug.Log("Drag is not held.");
        }
    }

    private IEnumerator DragObject()
    {
        if (targetObject == null || targetPoint == null)
            yield break;

        Vector3 direction = (targetPoint - targetObject.transform.position).normalized;

        while (InputManager.Instance.IsDragHeld() && targetObject.transform.position != targetPoint)
        {
            if (!isDragging)
            {
                isDragging = true;
                yield return new WaitForSeconds(DragDelay);
            }

            targetObject.transform.Translate(direction * DragSpeed * Time.deltaTime);
        }
    }
}
