using Unity.VisualScripting;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [Header("Opciones de Visualización")]
    [SerializeField] private bool verbose = true;
    [SerializeField] private Color color_base = Color.green;
    private Color color_actual;

    [Header("Detección del Suelo")]
    [SerializeField] private float distancia_deteccion_suelo;
    private RaycastHit contacto_suelo;
    [Header("Detección de Vacío")]
    [SerializeField] private bool vacio_detectado;
    private RaycastHit contacto_vacio;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        color_actual = color_base;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(verbose) Debug.DrawRay(transform.position, -transform.up * distancia_deteccion_suelo, color_actual);

        Physics.Raycast(transform.position, -transform.up, out contacto_suelo, distancia_deteccion_suelo, ~(LayerMask.GetMask("Vacíos") | LayerMask.GetMask("Finales")));
        Physics.Raycast(transform.position, -transform.up, out contacto_vacio, distancia_deteccion_suelo, LayerMask.GetMask("Vacíos"));


        if (contacto_vacio.collider != null)
        {
            vacio_detectado = true;
            color_actual = Color.blue;
        }
        else
        {
            vacio_detectado = false;
        }
        if (contacto_suelo.collider != null)
        {
            color_actual = Color.red;
        }
        else if(!vacio_detectado)
        {
            color_actual = color_base;
        }


    }
    public RaycastHit DetectaSuelo()
    {
        return contacto_suelo;
    }
    public RaycastHit DetectaVacio()
    {
        return contacto_vacio;
    }
}
