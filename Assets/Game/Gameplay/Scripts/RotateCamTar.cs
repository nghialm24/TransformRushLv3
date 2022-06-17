using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamTar : MonoBehaviour
{
    private PlayerController playerController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        if (currentRotation.y < 350)
        {
            transform.Rotate(0, 45 * Time.deltaTime, 0);
        }
    }
}
