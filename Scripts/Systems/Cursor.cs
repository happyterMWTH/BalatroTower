using System.Drawing;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cursor : MonoBehaviour
{
    [Header("Arañita")]
    [SerializeField] private Detector[] patas = new Detector[9];
    [SerializeField] private GameObject pivote;
    [SerializeField] private float perspectiva_camara = -20f;

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
            transform.position =Hacer_Par(pata.DetectaSuelo().point, pata) ;
            pivote.GetComponent<CameraSystem>().ResetExterno(transform.position);
        }
    }

    private UnityEngine.Vector3 Hacer_Par(UnityEngine.Vector3 punto_redondear, Detector pata)
    {
        float puntox= punto_redondear.x;
        float puntoy= punto_redondear.y;
        float puntoz= punto_redondear.z;
        if (pata == patas[0])
        {
            puntox-=2;
            puntoz-=2; 
        }

        if (pata == patas[1])
        {
            puntoz-=2; 
        }
        
        if (pata == patas[2])
        {
            puntox+=2;
            puntoz-=2; 
        }

        if (pata == patas[3])
        {
            puntox-=2; 
        }

        if (pata == patas[4])
        {
            puntox=puntox;
            puntoz=puntoy; 
        }

        if (pata == patas[5])
        {
            puntox+=2; 
        }

        if (pata == patas[6])
        {
            puntox-=2;
            puntoz+=2; 
        }

        if (pata == patas[7])
        {
            puntoz+=2; 
        }
        if (pata == patas[8])
        {
            puntox+=2;
            puntoz+=2; 
        }
        UnityEngine.Vector3 vertor_de_retorno= new UnityEngine.Vector3 (puntox,puntoy,puntoz);
        return vertor_de_retorno;
    }
}
