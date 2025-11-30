using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cube : MonoBehaviour
{
    Rigidbody rb;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.AddForce(new Vector3(0f, 10f, 0), ForceMode.Impulse);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray;
            RaycastHit hit;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("I'm hit!" + hit.collider.gameObject.name);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("On collision Enter");
    }

    void OnCollisionStay(Collision collision)
    {
        Debug.Log("On collision Stay");
    }

    void OnCollisionExit(Collision collision)
    {
        Debug.Log("On collision Exit");
    }

    void OnDrawGizmosSeleted()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(2,3,4));
    }

}
