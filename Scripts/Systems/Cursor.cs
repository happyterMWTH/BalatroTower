using UnityEngine;
using UnityEngine.InputSystem;

public class Cursor : MonoBehaviour
{
    [Header("Arañita")]
    [SerializeField] private Detector[] patas = new Detector[9];
    [SerializeField] private GameObject pivote;
    [SerializeField] private float perspectiva_camara = -20f;

    private Vector2 posible_movimiento = new Vector2(0, 0);

    void Awake()
    {
        
    }
    void Start()
    {
        
    }
    void Update()
    {
        transform.rotation = pivote.transform.rotation;
        transform.Rotate(0, perspectiva_camara, 0);
    }

    public void SeleccionX(InputAction.CallbackContext context)
    {
        float valor_x = context.ReadValue<float>();
        posible_movimiento.x = valor_x;
        Mover();
    }

    public void SeleccionY(InputAction.CallbackContext context)
    {
        float valor_y = context.ReadValue<float>();
        posible_movimiento.y = valor_y;
        Mover();
    }
    private void Mover()
    {
        Detector pata = patas[(int)posible_movimiento.y * 3 + (int)posible_movimiento.x + 4];

        if (pata.DetectaSuelo().collider != null)
        {
            transform.position = pata.DetectaSuelo().point;
            pivote.GetComponent<CameraSystem>().ResetExterno(transform.position);
        }
    }

}
