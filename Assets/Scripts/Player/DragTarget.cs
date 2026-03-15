using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;

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

        Vector3 direction = transform.forward;

        if (InputManager.Instance.IsDragHeld())
        {
            StartCoroutine(DragObject());

            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, DragRange, ~ignoreLayers))
            {
                Renderer renderer = hit.collider.GetComponent<Renderer>();
                string tag = hit.collider.gameObject.tag;

                Debug.Log($"Raycast hit: {hit.collider.gameObject.name}, Tag: {tag}, Renderer: {(renderer != null ? "Yes" : "No")}");
                Debug.DrawRay(transform.position, direction * hit.distance, Color.red);

                if (renderer != null && tag == "Dragable")
                {
                    targetObject = hit.collider.gameObject;
                    Debug.Log($"Target object set to: {targetObject.name}");
                }
            }

            if (targetObject != null)
            {
                direction = transform.forward;
                float distance = Vector3.Distance(transform.position, targetObject.transform.position);

                targetPoint = transform.position + direction * distance;

                if (targetObject.transform.position == targetPoint)
                {
                    isDragging = false;
                }
            }
            Debug.Log("Dragging: " + (targetObject != null ? targetObject.name : "None"));
        }
        else
        {
            if (targetObject != null)
                targetObject = null;
            if (isDragging)
                isDragging = false;
            StopCoroutine(DragObject());

            Debug.Log("Drag released. Target object cleared.");
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
