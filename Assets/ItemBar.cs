using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBar : MonoBehaviour
{
    [SerializeField] private CarController carController;
    [SerializeField] private Transform flyTar;
    [SerializeField] private Transform moveTar;
    private Vector3 temp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (carController.isFlying)
        {
            transform.position = flyTar.position;
        } else
        {
            transform.position = moveTar.position;
        }
    }
}
