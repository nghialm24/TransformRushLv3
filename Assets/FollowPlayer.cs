using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    void Start()
    {
        player = GameObject.Find("updownvfxTar");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }
}
