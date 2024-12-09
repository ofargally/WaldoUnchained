using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private void Start()
    {
        Debug.Log("ClickToMove Script is attached to " + gameObject.name);
        navAgent = GetComponent<NavMeshAgent>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("ClickToMove : Mouse Clicked");
            // Create Ray from Camera to Mouse Position
            Camera cam = Camera.main;
            if (cam == null)
            {
                Debug.LogError("Main Camera not found. Ensure your camera is tagged as 'MainCamera'.");
                return;
            }
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            int groundLayerMask = LayerMask.GetMask("Ground");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayerMask))
            {
                Debug.Log("ClickToMove : Raycast hit " + hit.collider.gameObject.name);
                navAgent.SetDestination(hit.point);
            }
            else
            {
                Debug.Log("ClickToMove : Raycast did not hit anything.");
            }
        }
    }
}