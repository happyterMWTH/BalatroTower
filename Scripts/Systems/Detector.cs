using UnityEngine;

public class Detector : MonoBehaviour
{
    [Header("Opciones de Visualización")]
    [SerializeField] private bool verbose = true;
    [SerializeField] private Color color_base = Color.green;
    private Color color_actual;

    [Header("Detección del Suelo")]
    [SerializeField] private float distancia_deteccion;
    private RaycastHit contacto_suelo;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        color_actual = color_base;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, -transform.up * distancia_deteccion, color_actual);
        
        if(Physics.Raycast(transform.position, -transform.up, out contacto_suelo, distancia_deteccion))
        // TODO: cambiarle la línea de detección para que ignore ciertas capas
        {
            color_actual = Color.red;
        }
        else
        {
            color_actual = color_base;
        }

    }
    public RaycastHit DetectaSuelo()
    {
        return contacto_suelo;
    }
}
