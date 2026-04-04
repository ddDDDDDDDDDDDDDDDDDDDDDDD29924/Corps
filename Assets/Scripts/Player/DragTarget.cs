using UnityEngine;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine.SocialPlatforms;
using Unity.VisualScripting;

public class DragTarget : MonoBehaviour
{
    // Put into the Camera Target
    public PlayerData playerData;

    private GameObject targetObject;

    private bool isDragging = false;

    private float DragDistance;

    private Vector3 targetPoint;

    public LayerMask Layers;

    private float DragRange => playerData.dragRange;
    private float DragMinDistance => playerData.dragMinDistance;
    private float DragSensitivity => playerData.dragSensitivity;
    private float DragDelay => playerData.dragDelay;
    private float DragSpeed => playerData.dragSpeed;


    bool hasLogged = false;

    private void Awake()
    {
        DragDistance = DragRange;
    }

    private void Update()
    {
        if (InputManager.Instance == null || GameManager.Instance.CurrentGameState != GameState.Playing)
            return;

        if (InputManager.Instance.IsDragHeld())
        {
            StartCoroutine(DragObject());

            float rayDistance = DragRange;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, Layers))
            {
                if (targetObject == null)
                {
                    targetObject = hit.collider.gameObject;
                    DragDistance = Vector3.Distance(Camera.main.transform.position, hit.collider.gameObject.transform.position);
                }
            }

            targetPoint = Camera.main.transform.forward * DragDistance + Camera.main.transform.position;
        }
        else
        {
            StopCoroutine(DragObject());

            DragDistance = DragRange;
            targetObject = null;
            targetPoint = Vector3.zero;
        }
    }

    private void AdvancedDebugLog(string message)
    {
        if (!hasLogged)
        {
            Debug.Log(message);
            hasLogged = true;
        }
    }

    private IEnumerator DragObject()
    {
        while (true)
        {
            if (targetObject == null || targetPoint == null || !InputManager.Instance.IsDragHeld())
                yield break;

            if (targetObject.transform.position != targetPoint)
            {
                Vector3 direction = (targetPoint - targetObject.transform.position).normalized;

                if (!isDragging)
                {
                    isDragging = true;
                    yield return new WaitForSeconds(DragDelay);
                }

                targetObject.transform.Translate(direction * DragSpeed * Time.deltaTime);
            }
            else
            {
                isDragging = false;
            }
        }
    }
}
