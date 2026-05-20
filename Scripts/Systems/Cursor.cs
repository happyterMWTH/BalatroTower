using UnityEngine;
using UnityEngine.InputSystem;

public class Cursor : MonoBehaviour
{
    [Header("Arañita")]
    [SerializeField] private Detector[] patas = new Detector[9];
    [SerializeField] private GameObject pivote;
    [SerializeField] private float perspectiva_camara = -20f;
    [SerializeField] private int maximos_vacios = 4;

    private UnityEngine.Vector2 posible_movimiento = new UnityEngine.Vector2(0, 0);

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
        if(context.started && !pivote.GetComponent<CameraSystem>().Get_Rotando()) Mover();
    }

    public void SeleccionY(InputAction.CallbackContext context)
    {
        float valor_y = context.ReadValue<float>();
        posible_movimiento.y = valor_y;
        if(context.started && !pivote.GetComponent<CameraSystem>().Get_Rotando()) Mover();
    }
    // Movimiento del cursor
    private bool Mover()
    {
        Detector pata = patas[(int)posible_movimiento.y * 3 + (int)posible_movimiento.x + 4];

        // Todavía no podemos hacer que salten los vacíos porque el raycast no funciona sin el FixedUpdate
        if (pata.DetectaSuelo().collider != null)
        {
            var vector_diferencia = pata.DetectaSuelo().point - transform.position;
            
            transform.position += Hacer_Par(vector_diferencia, pata);
            transform.position = new Vector3(
                Mathf.Round(transform.position.x), 
                Mathf.Round(transform.position.y), 
                Mathf.Round(transform.position.z)
            );
            pivote.GetComponent<CameraSystem>().Reset_Externo(transform.position);
        }
        return pata.DetectaSuelo().collider != null;
    }
    // Actualización del visual del cursor
    private void Actualizar_Cursor()
    {

    }


    // Ajuste de coordenadas
    private Vector3 Hacer_Par(Vector3 punto_redondear, Detector pata)
    {
        float punto_x = 0;
        float punto_y = punto_redondear.y;
        float punto_z = 0;

        if (pata == patas[0])
        {
            punto_x = -2;
            punto_z = -2; 
        }

        else if (pata == patas[1])
        {
            punto_z = -2; 
        }
        else if (pata == patas[2])
        {
            punto_x = 2;
            punto_z = -2; 
        }
        else if (pata == patas[3])
        {
            punto_x = -2; 
        }
        else if (pata == patas[4])
        {
            punto_x = 0;
            punto_z = 0; 
        }
        else if (pata == patas[5])
        {
            punto_x = 2; 
        }
        else if (pata == patas[6])
        {
            punto_x = -2;
            punto_z = 2; 
        }
        else if (pata == patas[7])
        {
            punto_z = 2; 
        }
        else if (pata == patas[8])
        {
            punto_x = 2;
            punto_z = 2; 
        }

        Vector3 frente = transform.forward * punto_z;
        Vector3 derecha = transform.right * punto_x;
        Vector3 vector_de_retorno = new Vector3 (frente.x + derecha.x, punto_y, frente.z + derecha.z);

        return vector_de_retorno;
    }
    


}
