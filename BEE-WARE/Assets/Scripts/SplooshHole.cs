using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplooshHole : MonoBehaviour
{
    [SerializeField] Rigidbody2D sploosh;
    [SerializeField] Transform splooshHole;

    void Start()
    { 
        Sploosh();
    }

    void Update()
    {
        
    }

    void Sploosh()
    {
        Instantiate(sploosh, splooshHole.position, splooshHole.rotation);
        Invoke("Sploosh", 3.5f);
    }
}
