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
    private string[] capas = new string[] {"Finales", "Vacíos", "Baldíos", "Edificios"};

    [Header("Debug Variables")]
    [SerializeField] private bool verbose = false;
    [SerializeField] private bool visualizar = false;

    void Start()
    {
        Debug.Log("Iniciando creación de capas en tiempo: " + Time.time);

        Vector3 posicion_inicial = transform.position + new Vector3(-2,0,2);
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
                    string[] nivel = columnas[x].Split('.');
                    for(int n = 0; n < nivel.Length; n++)
                    {
                        string indice_capa_string = nivel[n].Trim();
                        int indice_capa = int.Parse(indice_capa_string);
                        if(indice_capa >= 0 && indice_capa < capas.Length)
                        {
                            Vector3 posicion_cuadro = new Vector3(
                                posicion_inicial.x + x * unidades_por_cuadro, 
                                posicion_inicial.y + desfase_y + n * unidades_por_cuadro, 
                                posicion_inicial.z - y * unidades_por_cuadro
                                );
                            GameObject nuevo_cuadro = Instantiate(prefab_cuadro, posicion_cuadro, Quaternion.identity, transform);
                            nuevo_cuadro.name = "Cuadro_" + x + "_" + y + "_" + n;
                            CapasDetectoras capas_detectoras = nuevo_cuadro.GetComponent<CapasDetectoras>();
                            if(capas_detectoras != null)
                            {
                                capas_detectoras.CambiarCapa(capas[indice_capa], visualizar, verbose);
                            }
                        }
                    }
                }
            }
        }
        Debug.Log("Finalizada creación de capas en tiempo: " + Time.time);
    }


    

};