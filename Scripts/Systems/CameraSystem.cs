using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSystem : MonoBehaviour
{

    [Header("Camera Variables")]
    [SerializeField] private float zoomPerSecond;
    [SerializeField] private float movementPerSecond;
    [SerializeField] private float rotationPerSecond;

    [SerializeField] private float minimumZoom;
    [SerializeField] private float maximumZoom;
    [SerializeField] private GameObject player;

    private Camera _camera;

    private Quaternion _targetRotation;
    private Quaternion _currentRotation;

    private Vector3 _targetPosition;
    private Vector3 _currentPosition;
    private Vector3 _originalPosition;
    private Vector3 _offsetPosition;

    private float _targetZoom;
    private float _currentZoom;
    private float _offsetZoom;

    void Awake()
    {
        _offsetPosition = Vector3.zero;
        _offsetZoom = 0f;

        _camera = Camera.main;
    }

    void Start()
    {
        transform.position = player.transform.position;

        _currentRotation = transform.rotation;
        _targetRotation = _currentRotation;

        _currentPosition = transform.position;
        _targetPosition = _currentPosition;
        _originalPosition = _currentPosition;

        _currentZoom = _camera.orthographicSize;
        _targetZoom = _currentZoom;
    }

    void Update()
    {
        _currentRotation = transform.rotation;
        if (_currentRotation != _targetRotation)
        {
            Quaternion smoothRotation = Quaternion.Slerp(
                _currentRotation,
                _targetRotation,
                rotationPerSecond * Time.deltaTime);
            transform.rotation = smoothRotation;
        }

        _currentPosition = _camera.transform.localPosition;
        _targetPosition = _currentPosition + _offsetPosition;
        if (_currentPosition != _targetPosition)
        {
            Vector3 smoothPosition = Vector3.Lerp(
                _currentPosition,
                _targetPosition,
                movementPerSecond * Time.deltaTime);
            _camera.transform.localPosition = smoothPosition;
        }

        _currentZoom = _camera.orthographicSize;
        _targetZoom = _currentZoom + _offsetZoom * zoomPerSecond;
        if(_targetZoom < minimumZoom){ _targetZoom = minimumZoom + 0.1f; }
        if(_targetZoom > maximumZoom){ _targetZoom = maximumZoom - 0.1f; }
        if (_currentZoom != _targetZoom)
        {
            float smoothZoom = Mathf.Lerp(
                _currentZoom, 
                _targetZoom,    
                zoomPerSecond * Time.deltaTime);
            Debug.Log(smoothZoom);
            _camera.orthographicSize = smoothZoom;
        }

    }

    public void Rotate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log(context.ReadValue<float>());
            _targetRotation *= Quaternion.Euler(
                0,
                90 * -context.ReadValue<float>(),
                0);
        }
    }

    public void MoveX(InputAction.CallbackContext context)
    {
        float input = context.ReadValue<float>();
        _offsetPosition.x = input * movementPerSecond * _currentZoom / maximumZoom;
    }

    public void MoveY(InputAction.CallbackContext context)
    {
        float input = context.ReadValue<float>();
        _offsetPosition.y = input * movementPerSecond * _currentZoom / maximumZoom;
    }

    public void Zoom(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _offsetZoom = -context.ReadValue<float>();
        }
        else if (context.canceled)
        {
            _offsetZoom = 0;
        }
    }
    
}