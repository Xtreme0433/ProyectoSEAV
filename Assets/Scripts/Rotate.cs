using UnityEngine;

public class Rotate : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown("space")) transform.Rotate(Vector3.right * 20.0f);
        transform.Rotate(Vector3.up * Time.deltaTime * 40.0f);
    }
}
