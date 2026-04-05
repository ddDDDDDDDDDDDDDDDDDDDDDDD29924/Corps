using UnityEngine;
using System.Collections;

public class DragTarget : MonoBehaviour
{
    // Put into the Camera Target
    public PlayerData playerData;

    private GameObject targetObject;

    private bool isDragging = false;

    private float DragDistance = 0f;

    public LayerMask Layers;

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
            float rayDistance = DragRange;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, Layers))
            {
                if (targetObject == null)
                {
                    targetObject = hit.collider.gameObject;
                }
                if (DragDistance == 0f)
                {
                    DragDistance += Vector3.Distance(Camera.main.transform.position, hit.collider.gameObject.transform.position);
                }
            }

            Vector3 targetPoint = ray.direction * Mathf.Clamp(DragDistance, DragMinDistance, DragRange) + Camera.main.transform.position;

            TranslateObject(targetPoint);
        }
        else
        {
            DragDistance = DragRange;
            targetObject = null;
        }
    }

    private void TranslateObject(Vector3 targetPoint)
    {
        if (targetObject != null)
        {
            if (targetObject.transform.position != targetPoint)
            {
                Vector3 direction = (targetObject.transform.position - targetPoint).normalized;

                if (!isDragging)
                {
                    isDragging = true;
                    StartCoroutine(Wait(DragDelay));
                }

                targetObject.transform.Translate(direction * DragSpeed * Time.deltaTime);
            }
            else
            {
                isDragging = false;
            }
        }
        else
        {
            Debug.Log("Target object is null");
            isDragging = false;
        }
    }

    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log("Waited for " + seconds + " seconds.");
    }
}
