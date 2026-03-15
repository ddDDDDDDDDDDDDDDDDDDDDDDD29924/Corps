using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class DragTarget : MonoBehaviour
{
    // Put into the Camera Target
    public PlayerData playerData;
    private GameObject target;
    private bool isDragging = false;
    private Vector3 targetPoint;

    private float DragRange => playerData.dragRange;
    private float DragMinDistance => playerData.dragMinDistance;
    private float DragMaxDistance => playerData.dragMaxDistance;
    private float DragSensitivity => playerData.dragSensitivity;
    private float DragDelay => playerData.dragDelay;

    private void Update()
    {
        if (InputManager.Instance == null || GameManager.Instance.CurrentGameState != GameState.Playing)
            return;

        Vector3 direction = transform.forward;

        if (InputManager.Instance.IsDragHeld())
        {
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, DragRange))
            {
                float distance = Vector3.Distance(transform.position, hit.point);
                targetPoint = transform.position + direction * distance;

                Renderer renderer = hit.collider.GetComponent<Renderer>();
                string tag = hit.collider.gameObject.tag;

                if (renderer != null && tag == "Dragable")
                {
                    
                }
            }   
        }
    }
}
