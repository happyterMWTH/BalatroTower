using UnityEngine;

public class Cursor : MonoBehaviour
{
    [Header("Arañita")]
    [SerializeField] private Detector[] patas = new Detector[9];
    [SerializeField] private GameObject pivote;
    [SerializeField] private float camera_perspective = -20f;

    void Awake()
    {
        
    }
    void Start()
    {
        
    }
    void Update()
    {
        transform.rotation = pivote.transform.rotation;
        transform.Rotate(0, camera_perspective, 0);
    }

}
