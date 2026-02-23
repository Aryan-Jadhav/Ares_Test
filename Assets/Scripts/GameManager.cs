using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    private void Awake()
    {
        if (_mainCamera == null)
            _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Screen Tapped");

            Ray _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out RaycastHit _hit))
            {
                Debug.Log("Hit Object: " + _hit.collider.name);

                Crate _crate = _hit.collider.GetComponent<Crate>();

                if (_crate != null)
                {
                    Debug.Log("Crate Found!");
                    _crate.OnTapped();
                }
                else
                {
                    Debug.Log("Tapped object is NOT a crate");
                }
            }
            else
            {
                Debug.Log("Raycast did NOT hit anything");
            }
        }
    }
}