using UnityEngine;
using Dreamteck.Splines;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Transform conveyorStartPoint;
    public Transform ConveyorStartPoint => conveyorStartPoint;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private SplineComputer mainSpline;

    private float tapCooldown = 0.3f;
    private float lastTapTime = -1f;

    public SplineComputer MainSpline => mainSpline;

    private void Awake()
    {
        Instance = this;

        if (mainCamera == null)
            mainCamera = Camera.main;
    }



    private void Update()
    {
        if (Time.time - lastTapTime < tapCooldown)
            return;

        // Touch input (mobile)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            HandleTap(Input.GetTouch(0).position);
        }
        // Mouse input (PC / Editor)
        else if (Input.GetMouseButtonDown(0))
        {
            HandleTap(Input.mousePosition);
        }
    }

    private void HandleTap(Vector2 screenPosition)
    {
        lastTapTime = Time.time;

        Ray ray = mainCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Crate crate = hit.collider.GetComponent<Crate>();
            if (crate != null)
            {
                crate.OnTapped();
            }
        }
    }
}