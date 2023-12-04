using UnityEngine;

public class OcclusionController : MonoBehaviour
{
    public Transform ballTransform;
    public LayerMask occlusionLayer;

    private Camera mainCamera;
    private Renderer[] occluderRenderers;

    void Start()
    {
        mainCamera = Camera.main;

        // Obtener los renderers de los objetos que pueden ocultar la pelota
        occluderRenderers = FindObjectsOfType<Renderer>();
    }

    void Update()
    {
        // revert the visibility of all objects
        foreach (Renderer renderer in occluderRenderers)
        {
            SetObjectVisibility(renderer.transform, true);
        }

        // Calculate the distance between the camera and the ball
        float distanceToBall = Vector3.Distance(mainCamera.transform.position, ballTransform.position);

        // Perform a raycast from the camera to the ball with the calculated distance
        RaycastHit[] hits = Physics.RaycastAll(mainCamera.transform.position, ballTransform.position - mainCamera.transform.position, distanceToBall, occlusionLayer);

        // Process all hits
        foreach (RaycastHit hit in hits)
        {
            // Check if the object hit is between the camera and the ball
            if (hit.transform != null && hit.transform != ballTransform)
            {
                // Handle the hit object as needed
                SetObjectVisibility(hit.transform, false);
            }
        }
    }

    void SetObjectVisibility(Transform objTransform, bool isVisible)
    {
        // Puedes modificar esto según tus necesidades específicas para cambiar la visibilidad de los objetos
        Renderer renderer = objTransform.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = isVisible;
        }
    }
}
