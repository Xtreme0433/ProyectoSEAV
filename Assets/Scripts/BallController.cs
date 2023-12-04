using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private float maxForce;
    [SerializeField]
    private float velocityThreshold = 0.5f;
    [SerializeField]
    private GameObject directionIndicator;
    [SerializeField]
    private Vector3 startingPosition;

    private Rigidbody rb;
    private LineRenderer directionIndicatorLineRenderer;
    private MeshRenderer meshRenderer;

    private Vector3 point = Vector3.zero;
    private bool shoot = false;
    private bool clicked = false;

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
        // if the ball is not "moving"
        if (rb.velocity.magnitude < velocityThreshold)
        {
            // on click (just the first frame)
            if (Input.GetMouseButtonDown(0))
            {
                clicked = true;
                directionIndicator.SetActive(true);
                directionIndicatorLineRenderer.SetPosition(0, transform.position);
            }

            // on hold click
            if (Input.GetMouseButton(0))
            {
                point = GetClickPoint();
                directionIndicatorLineRenderer.SetPosition(0, transform.position);
                directionIndicatorLineRenderer.SetPosition(1, point);
            }

            // on unclick
            if (Input.GetMouseButtonUp(0) && clicked)
            {
                // deactivate the indicator
                directionIndicator.SetActive(false);

                // change back clicked to false
                clicked = false;

                // give it impulse

                shoot = true;
            }
        }
    }

    private void FixedUpdate()
    {
        // Check if the velocity magnitude is less than a threshold (we count that as not moving)
        if (rb.velocity.magnitude < velocityThreshold)
        {
            // Object is in a stable position
            ChangeMaterialColor(Color.green);
        }
        else
        {
            ChangeMaterialColor(Color.red);
        }

        // if you can shoot then apply an impulse to the ball
        if (shoot)
        {
            const float magnitudeScale = 0.25f;

            Vector3 direction = transform.position - point;
            float magnitude = Vector3.Magnitude(direction) * magnitudeScale;
            magnitude = Mathf.Clamp(magnitude, 0.0f, maxForce);

            rb.AddForce(Vector3.Normalize(direction) * magnitude, ForceMode.Impulse);
            shoot = false;
        }

        // reset the ball
        if (rb.position.y < -5.0f)
        {
            rb.position = new Vector3(7.0f, 0.5f, 3.0f);
            // Reset the velocity to zero
            rb.velocity = Vector3.zero;

            // Reset the angular velocity to zero
            rb.angularVelocity = Vector3.zero;
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