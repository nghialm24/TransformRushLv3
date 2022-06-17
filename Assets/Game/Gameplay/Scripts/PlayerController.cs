using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject HomeUI;
    [SerializeField] CinemachineVirtualCamera move;
    [SerializeField] CinemachineVirtualCamera enemy;
    [SerializeField] CinemachineVirtualCamera boss;
    [SerializeField] CinemachineVirtualCamera boss2;
    [SerializeField] CinemachineVirtualCamera fly;
    [SerializeField] protected Slider _mainHealth;
    [SerializeField] public bool FightingWin = false;
    [SerializeField] public bool Win = false;
    [SerializeField] public bool Lose = false;
    [SerializeField] public bool isFightingEnemy = false;
    [SerializeField] public bool isFightingBoss = false;
    [SerializeField] public bool transtomain = false;
    [SerializeField] public bool transtocar = false;
    [SerializeField] private GameObject camera;
    private SplineFollower spline;

    private void OnEnable()
    {
        CameraSwitcher.Register(move);
        CameraSwitcher.Register(enemy);
        CameraSwitcher.Register(boss);
        CameraSwitcher.Register(boss2);
        CameraSwitcher.Register(fly);
        CameraSwitcher.SwitchCamera(move);
    }
    public void OnDisable()
    {
        CameraSwitcher.UnRegister(move);
        CameraSwitcher.UnRegister(enemy);
        CameraSwitcher.UnRegister(boss);
        CameraSwitcher.UnRegister(boss2);
        CameraSwitcher.UnRegister(fly);
    }
    void Start()
    {            
        spline = GetComponent<SplineFollower>();
        spline.enabled = false;
    }

    void Update()
    {
        if (HomeUI.activeSelf)
        {
            if (Input.GetMouseButtonDown(0)) spline.enabled = true;
        }
        if (isFightingEnemy || isFightingBoss)
        {
            if(spline.followSpeed >= 0) spline.followSpeed -= 14 * Time.deltaTime;
            FighttingEnemy();
        } else if(spline.followSpeed <= 15) spline.followSpeed += 14 * Time.deltaTime;
    }
    
    private void OnTriggerEnter(Collider other)
    {        
        FightingWin = false;
        if (other.tag == "sandau")
        {
            transtocar = false;
            transtomain = true;
            isFightingEnemy = true;
            CameraSwitcher.SwitchCamera(enemy);
        }
        if (other.tag == "sandauBoss")
        {
            transtocar = false;
            transtomain = true;
            isFightingBoss = true;
            CameraSwitcher.SwitchCamera(boss);
        }
        if (other.tag == "fly")
        {
            transtocar = false;
            transtomain = true;
            CameraSwitcher.SwitchCamera(fly);
        }
        if (other.tag == "down")
        {
            transtocar = true;
            transtomain = false;
            CameraSwitcher.SwitchCamera(move);
        }
    }
    
    private void FighttingEnemy()
    {
        if (FightingWin)
        {
            transtomain = false;
            transtocar = true;
            isFightingEnemy = false;
            CameraSwitcher.SwitchCamera(move);
        }
        
        if (_mainHealth.value <= 0)
        {
            Lose = true;
            if(isFightingBoss) CameraSwitcher.SwitchCamera(boss2);
        }
    }
}
