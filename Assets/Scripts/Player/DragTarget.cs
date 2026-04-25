using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class DragTarget : MonoBehaviour
{
    // Put into the Camera Target
    public PlayerData playerData;

    private GameObject targetObject;

    private bool isDragging = false;

    private float DragDistance = 0f;

    public LayerMask Layers;

    [SerializeField] private GameObject Floor;

    Rigidbody rb;

    private float DragRange => playerData.dragRange;
    private float DragMinDistance => playerData.dragMinDistance;
    private float DragSensitivity => playerData.dragSensitivity;
    private float DragDelay => playerData.dragDelay;
    private float DragSpeed => playerData.dragSpeed;

    private void FixedUpdate()
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
                    rb = targetObject.GetComponent<Rigidbody>();
                    rb.useGravity = false;
                }
                if (DragDistance == 0f)
                {
                    DragDistance = Vector3.Distance(Camera.main.transform.position, hit.collider.gameObject.transform.position);
                }
            }

            Vector3 targetPoint = Camera.main.transform.forward * Mathf.Clamp(DragDistance, DragMinDistance, DragRange) + Camera.main.transform.position;

            TranslateObject(targetPoint);
        }
        else
        {
            if (targetObject != null)
            {
                rb.useGravity = true;
                DragDistance = 0f;
                targetObject = null;
            }
        }
    }

    private void TranslateObject(Vector3 targetPoint)
    {
        if (targetObject != null)
        {
            if (rb == null)
            {
                Debug.LogWarning("Target object does not have a Rigidbody component.");
                return;
            }

            if (targetObject.transform.position != targetPoint)
            {
                if (!isDragging)
                {
                    isDragging = true;
                    StartCoroutine(Wait(DragDelay));
                }
                
                float yAxis = Floor.transform.position.y + targetObject.transform.localScale.y / 2f;
                Vector3 p = new Vector3(targetPoint.x, Mathf.Clamp(targetPoint.y, yAxis, 10000f), targetPoint.z);
                rb.MovePosition(p);
            }
            else
            {
                isDragging = false;
            }
        }
        else
        {
            isDragging = false;
        }
    }

    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log("Waited for " + seconds + " seconds.");
    }
}
