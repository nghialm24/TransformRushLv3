using System.Collections;
using System.Collections.Generic;
using Funzilla;
using UnityEngine;

public class CarController : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Joystick joystick;
    [SerializeField] GameObject image;
    [SerializeField] private bool pauseInput = false;
    [SerializeField] public static CarController Instance;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float flySpeed = 0.1f;
    [SerializeField] public bool isFlying = false;
    
    public enum State
    {
        Idle, Play, TurnLeft, TurnRight, Quayxe
    }    
    [SerializeField] public State _state = State.Idle;

    private void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(playerController.isFightingEnemy) ChangeState(State.Quayxe);
        else if (!image.activeSelf) ChangeState(State.Idle);
        if (pauseInput || !joystick.enabled) return;
        MoveTo(joystick.Direction.x);
        switch (_state)
        {
            case State.Idle:
                break;
            case State.TurnLeft:
                break;
            case State.TurnRight:
                break;
            case State.Quayxe:
                break;
            //default:
            //   throw new ArgumentOutOfRangeException();
        }
    }

    private void ChangeState(State newState)
    {
        if (newState == _state) return;
        ExitCurrentState();
        _state = newState;
        EnterNewState();
    }

    private void ExitCurrentState()
    {
        switch (_state)
            {
                case State.Idle: 
                    animator.ResetTrigger("Idle");
                    break;
                case State.TurnLeft:                
                    animator.ResetTrigger("TurnLeft");
                    break;
                case State.TurnRight:
                    animator.ResetTrigger("TurnRight");
                    break;
                case State.Quayxe:
                    animator.ResetTrigger("Quayxe");
                    break;
                //default:
                //    throw new ArgumentOutOfRangeException();
        }
    }

    private void EnterNewState()
        {
            switch (_state)
            {
                case State.Idle: 
                    animator.SetTrigger("Idle");
                    break;
                case State.TurnLeft:                
                    animator.SetTrigger("TurnLeft");
                    break;
                case State.TurnRight:
                    animator.SetTrigger("TurnRight");
                    break;
                case State.Quayxe:
                    animator.SetTrigger("Quayxe");
                    break;
                //default:
                //   throw new ArgumentOutOfRangeException();
            }
        }
    internal void PauseInput()
    {
        joystick.enabled = false;
        pauseInput = true;
    }

    internal void ResumeInput()
    {
        joystick.gameObject.SetActive(false);
        joystick.gameObject.SetActive(true);
        joystick.enabled = true;
        pauseInput = false;
    }

    public void MoveTo(float x)
    {
        var currentPos = transform.localPosition;
        if (playerController.isFightingEnemy)
        {
            x = 0;
            currentPos.y = 0.35f;
            if (currentPos.x >= 1.31f) currentPos.x -= 5* Time.deltaTime;
            if (currentPos.x <= 1.29f) currentPos.x += 5* Time.deltaTime;
        }
        else if (playerController.isFightingBoss)
        {
            x = 0;
            currentPos.y = 0.35f;
            if (currentPos.x >= 0.01f) currentPos.x -= 5* Time.deltaTime;
            if (currentPos.x <= -0.01f) currentPos.x += 5* Time.deltaTime;
        }
        else
        {
            currentPos.x += x * moveSpeed * Time.smoothDeltaTime;
            currentPos.x = Mathf.Clamp(currentPos.x, -2.7f, 2.7f);
            if(x > 0 && currentPos.x < 2.7f) ChangeState(State.TurnRight);
            if(x < 0 && currentPos.x > -2.7f) ChangeState(State.TurnLeft);
            if (x == 0) ChangeState(State.Idle);
            if (currentPos.x == -2.7f || currentPos.x == 2.7f) ChangeState(State.Idle);
            //currentPos.y = 0f;
            if (isFlying)
            {
                 if (currentPos.y <= 5f)
                     currentPos.y += flySpeed * Time.deltaTime;
            }
             else if (currentPos.y >= 0.01f)
             {
                 currentPos.y -= flySpeed * Time.deltaTime;
             }
        }
        transform.localPosition = currentPos;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "fly")
        {
            isFlying = true;
        } else 
        if (other.tag == "down")
        {
            isFlying = false;
        }

        if (other.tag == "box")
        {
            other.attachedRigidbody.AddForce(transform.forward * 1500);
            other.attachedRigidbody.AddForce(0,0,500);
        }
    }
}
