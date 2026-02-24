using UnityEngine;
using Dreamteck.Splines;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Transform conveyorStartPoint;
    public Transform ConveyorStartPoint => conveyorStartPoint;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private SplineComputer mainSpline;

    public SplineComputer MainSpline => mainSpline;

    private void Awake()
    {
        Instance = this;

        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

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
}