using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreadorCapas : MonoBehaviour
{
    [Header("Capa Variables")]
    [SerializeField] private TextAsset cuadricula_csv;
    [SerializeField] private char divisor = ';';
    [SerializeField] private int unidades_por_cuadro = 2;
    [SerializeField] private GameObject prefab_cuadro;
    [SerializeField] private float desfase_y;

    void Start()
    {
        Vector3 posicion_inicial = transform.position;
        if(cuadricula_csv != null)
        {
            string cuadricula_text = cuadricula_csv.text.ToString();
            string[] filas = cuadricula_text.Split('\n');
            for (int y = 0; y < filas.Length; y++)
            {
                string[] columnas = filas[y].Split(divisor);
                //Debug.Log("columnas: " + columnas.Length);
                for (int x = 0; x < columnas.Length; x++)
                {
                    if(columnas[x] == "1")
                    {
                        Vector3 posicion_cuadro = new Vector3(
                            posicion_inicial.x + x * unidades_por_cuadro, 
                            posicion_inicial.y + desfase_y, 
                            posicion_inicial.z - y * unidades_por_cuadro);
                        var nuevo_cuadro = Instantiate(prefab_cuadro, posicion_cuadro, Quaternion.identity, transform);
                        nuevo_cuadro.name = $"Cuadro_{x}_{y}";
                        nuevo_cuadro.layer = LayerMask.NameToLayer("Edificios");
                        foreach (Transform child in nuevo_cuadro.transform)
                        {
                            child.gameObject.layer = LayerMask.NameToLayer("Edificios");
                        }
                    }
                }
            }
        }
    }

    

};