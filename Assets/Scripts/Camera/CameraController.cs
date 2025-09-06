using UnityEngine;
using Unity.Cinemachine;

[RequireComponent(typeof(CinemachineCamera))]
public class CameraController : MonoBehaviour
{
    [Header("Zoom Settings")]
    public float zoomSpeed = 5f;
    public float minZoom = 2f;
    public float maxZoom = 15f;
    public float zoomDamping = 0.5f;

    [Header("Rotation Settings")]
    public float rotationSpeed = 2f;
    public float rotationDamping = 0.1f;

    public Transform target; // Alvo da câmera
    private float currentZoom;
    private Vector2 rotation = new Vector2(20, 0); // x = pitch, y = yaw

    private CinemachineCamera virtualCamera;
    private CinemachineFollow follow;

    void Awake()
    {
        currentZoom = (minZoom + maxZoom) / 2f; // Zoom inicial
    }

    void Update()
    {
        HandleZoom();
        HandleRotation();
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            currentZoom = Mathf.Clamp(currentZoom - scroll * zoomSpeed, minZoom, maxZoom);
        }
    }

    void HandleRotation()
    {
        if (Input.GetMouseButton(1) || Input.GetMouseButton(0)) // Botão direito ou esquerdo
        {
            rotation.y += Input.GetAxis("Mouse X") * rotationSpeed;
            rotation.x -= Input.GetAxis("Mouse Y") * rotationSpeed;
            rotation.x = Mathf.Clamp(rotation.x, 5f, 70f);
        }

        if (target != null)
        {
            Quaternion rot = Quaternion.Euler(rotation.x, rotation.y, 0);
            Vector3 offset = rot * new Vector3(0, 0, -currentZoom);
            Vector3 desiredPosition = target.position + offset;

            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime / zoomDamping);
            transform.LookAt(target);
        }
    }
}
