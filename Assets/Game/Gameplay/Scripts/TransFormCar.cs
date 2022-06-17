using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransFormCar : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private Animator animator;
    
    private enum State
    {
        Idle,
        ExposeCar,
        HideCar
    }
    private State _state = State.Idle;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    private void ChangeState(State newState)
    {
        if (_state == newState) return;
        ExitOldState();
        _state = newState;
        EnterNewState();
    }
    
    private void EnterNewState()
    {
        switch (_state)
        {
            case State.Idle:
                animator.SetTrigger("Idle");
                break;
            case State.ExposeCar:
                animator.SetTrigger("ExposeCar");
                break;
            case State.HideCar:
                animator.SetTrigger("HideCar");
                break;
        }
    }
    
    private void ExitOldState()
    {
        switch (_state)
        {
            case State.Idle:
                animator.ResetTrigger("Idle");
                break;
            case State.ExposeCar:
                animator.ResetTrigger("ExposeCar");
                break;
            case State.HideCar:
                animator.ResetTrigger("HideCar");
                break;
        }
    }

    private void Update()
    {
        if (playerController.transtocar)
        {
            animator.enabled = true;
            ChangeState(State.ExposeCar);
        }
        else if (playerController.transtomain)
        {
            animator.enabled = true;
            ChangeState(State.HideCar);
        } else animator.enabled = false;
    }
}
