using UnityEngine;

public class CapasDetectoras : MonoBehaviour
{

    [Header("Variables Debug")]
    private MeshRenderer _renderer;
    [SerializeField] private Material[] materiales_debug;
    private int indice_material_actual = 0;

    void Awake()
    {
        _renderer = GetComponentInChildren<MeshRenderer>();
    }

    public void Visualizar(bool visualizar)
    {
        _renderer.enabled = visualizar;
        
        _renderer.material = materiales_debug[indice_material_actual];
    }
    

    public void CambiarCapa(string nombre_capa, bool visualizar, bool verbose)
    {
        int nueva_capa = LayerMask.NameToLayer(nombre_capa);
        gameObject.layer = nueva_capa;
        foreach (Transform child in transform)
        {
            child.gameObject.layer = nueva_capa;
        }

        switch (nombre_capa)
        {
            case "Vacíos":
                indice_material_actual = 1;
                break;
            case "Baldíos":
                indice_material_actual = 2;
                break;
            case "Edificios":
                indice_material_actual = 3;
                break;
            default:
                indice_material_actual = 0;
                break;
        }
        if (verbose)
        {
            Debug.Log("Cambiando capa a: " + nombre_capa + " con indice: " + nueva_capa);
        }
        Visualizar(visualizar);
    }
}
