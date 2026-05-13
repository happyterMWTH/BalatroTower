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
    private bool _rotando = false;

    private float _targetZoom;
    private float _currentZoom;
    private float _offsetZoom;
    private bool _resetting;

    void Awake()
    {
        _resetting = false;
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
        

        Rotar();
        Mover();
        Zoom();
       

    }

    private void Rotar()
    {
        _currentRotation = transform.rotation;
        if (_currentRotation != _targetRotation)
        {
            _rotando = true;
            Quaternion smoothRotation = Quaternion.Slerp(
                _currentRotation,
                _targetRotation,
                rotationPerSecond * Time.deltaTime);
            transform.rotation = smoothRotation;
        }
        else
        {
            _rotando = false;
        }   
    }

    private void Mover()
    {
        _currentPosition = transform.position;
        _targetPosition = (!_resetting) ? _currentPosition + _offsetPosition : _targetPosition;
        if (_currentPosition != _targetPosition)
        {
            Vector3 smoothPosition = Vector3.Lerp(
                _currentPosition,
                _targetPosition,
                movementPerSecond * Time.deltaTime);
            transform.position = smoothPosition;
        }

        if (_resetting)
        {
            _resetting = !((_currentPosition - _targetPosition).magnitude < 0.1f);
        }
    }

    private void Zoom()
    {
        _currentZoom = _camera.orthographicSize;
        if (_currentZoom != _targetZoom)
        {
            float smoothZoom = Mathf.Lerp(
                _currentZoom,
                _targetZoom,
                zoomPerSecond * Time.deltaTime);
            _camera.orthographicSize = smoothZoom;
        }
    }

    public void Rotate_Input(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Debug.Log(context.ReadValue<float>());
            _targetRotation *= Quaternion.Euler(
                0,
                90 * -context.ReadValue<float>(),
                0);
        }
    }

    public void Mover_X_Input(InputAction.CallbackContext context)
    {
        if (!_resetting)
        {
            float input = context.ReadValue<float>();
            _offsetPosition.x = input * movementPerSecond * _currentZoom / maximumZoom * transform.right.x;
            _offsetPosition.z  = input * movementPerSecond * _currentZoom / maximumZoom * transform.right.z;
        }
    }

    public void Mover_Y_Input(InputAction.CallbackContext context)
    {
        if (!_resetting)
        {
            float input = context.ReadValue<float>();
            _offsetPosition.y = input * movementPerSecond * _currentZoom / maximumZoom;
        }
    }

    public void Zoom_Input(InputAction.CallbackContext context)
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

    public void Reset_Camara(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            Debug.Log("Reset Camera");
            _resetting = true;
            _targetPosition = _originalPosition;
        }
    }
    
    public void Reset_Externo(Vector3 nuevo_centro)
    {
        _originalPosition = nuevo_centro;
        _targetPosition = _originalPosition;
        _resetting = true;
    }
    public bool Get_Rotando()
    {
        return _rotando;
    }
}