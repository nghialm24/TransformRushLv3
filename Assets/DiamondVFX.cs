using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondVFX : MonoBehaviour
{
    [SerializeField] private GameObject vfx;
    [SerializeField] private GameObject diamond;
    void Start()
    {
        vfx.SetActive(false);
    }

    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            vfx.SetActive(true);
            Destroy(diamond);
        }
    }
}
