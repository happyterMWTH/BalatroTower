using UnityEngine;

public class Detector : MonoBehaviour
{
    RaycastHit contacto_suelo;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position,-transform.up*1000, Color.green);
        
    }
}
