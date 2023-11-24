using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float xOffSet, yOffSet, zOffSet;
    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + new Vector3(xOffSet, yOffSet, zOffSet);
        transform.LookAt(target.transform.position);
    }
}
