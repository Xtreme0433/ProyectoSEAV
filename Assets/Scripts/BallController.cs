using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private float maxForce;
    [SerializeField]
    private GameObject directionIndicator;
    [SerializeField]
    private float velocityThreshold = 0.1f;

    private Rigidbody rb;
    private LineRenderer directionIndicatorLineRenderer;
    private MeshRenderer meshRenderer;

    private Vector3 point = Vector3.zero;
    private bool shoot = false;

    // change material
    void ChangeMaterialColor(Color color)
    {
        // Make sure the MeshRenderer and material are not null
        if (meshRenderer != null && meshRenderer.material != null)
        {
            // Change the material color
            meshRenderer.material.color = color;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // set line object to be unactive
        directionIndicator.SetActive(false);

        // get components
        rb = GetComponent<Rigidbody>();
        directionIndicatorLineRenderer = directionIndicator.GetComponent<LineRenderer>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude < velocityThreshold)
        {
            // on click (just the first frame)
            if (Input.GetMouseButtonDown(0))
            {
                directionIndicator.SetActive(true);
                directionIndicatorLineRenderer.SetPosition(0, transform.position);
            }

            // on click
            if (Input.GetMouseButton(0))
            {
                point = GetClickPoint();
                directionIndicatorLineRenderer.SetPosition(1, point);
                Debug.Log(point);
            }

            // on unclick
            if (Input.GetMouseButtonUp(0))
            {
                // deactivate the indicator
                directionIndicator.SetActive(false);

                // give it impulse

                shoot = true;
            }
        }
    }

    private void FixedUpdate()
    {
        // 1. Check angular velocity
        if (rb.velocity.magnitude < velocityThreshold)
        {
            // Object is in a stable position
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            ChangeMaterialColor(Color.green);
        }
        else
        {
            ChangeMaterialColor(Color.red);
        }

        if (shoot)
        {
            Vector3 direction = transform.position - point;
            rb.AddForce(direction, ForceMode.Impulse);
            shoot = false;
        }
    }

    // Get the point of intersection of a ray casted from the camera through the ball's xy plane
    private Vector3 GetClickPoint()
    {
        Vector3 point = Vector3.zero;

        // cast a ray through the ball's xy plane
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane m_Plane = new Plane(new Vector3(0.0f, 1.0f, 0.0f), transform.position);
        
        if (m_Plane.Raycast(ray, out float enter))
        {
            point = ray.GetPoint(enter);
        }

        return point;
    }
}
