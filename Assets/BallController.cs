using UnityEngine;

public class LineaApuntado : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float stopVelocity = 0.1f;
    [SerializeField] private float powerMultiplier = 4f;
    [SerializeField] private float maxPower = 10f;
    private bool isAiming = false;
    private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
    private float power = 0f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer.enabled = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isAiming)  // TODO: hay veces que no lo detecta, es por temas de sincronizacion de frames
        {
            stopwatch.Start();
            power = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < stopVelocity)
        {
            StopBall();
        }
        ProcessAiming();
    }

    private void StopBall()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.rotation = Quaternion.identity; // TODO: queda feo que se resetee la rotacion pero si no hay que hacer cuentas para que la linea se dibuje bien
        isAiming = true;
    }

    private void ProcessAiming()
    {
        if (!isAiming) return;

        Vector3? point = GetMousePoint();
        if (!point.HasValue) return;

        DrawLine(point.Value);
        if (Input.GetMouseButtonUp(0))
        {
            stopwatch.Stop();
            power = Mathf.Clamp(float.Parse(stopwatch.ElapsedMilliseconds.ToString()) / 1000 * powerMultiplier, 0, maxPower);
            Debug.Log($"Power: {power}");
            stopwatch.Reset();
            ShotBall(point.Value, power);
        }
    }

    private void ShotBall(Vector3 point, float shotPower)
    {
        isAiming = false;
        lineRenderer.enabled = false;

        Vector3 horizontalPoint = new(point.x, transform.position.y, point.z);
        Vector3 direction = (horizontalPoint - transform.position).normalized;
        rb.AddForce(shotPower * direction, ForceMode.Impulse);
    }

    private void DrawLine(Vector3 point)
    {
        Vector3[] positions = {
            Vector3.zero,  // No preguntes, yo tampoco se por que esto funciona 
            point - transform.position
        };
        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;
    }

    private Vector3? GetMousePoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
            return hit.point;
        }
        return null;
    }

}
