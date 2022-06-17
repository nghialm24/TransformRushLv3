using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeAnim : MonoBehaviour
{
    [SerializeField]float scrollSpeed = 0.5f;
    [SerializeField]Renderer rend;

    void Start()
    {
        
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}
