using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    Animator animator;
    
    [SerializeField] private Transform hitTar;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private ModelCharactor modelCharactor;
    [SerializeField] protected Slider _slider;
    [SerializeField] public float currentHealth = 30;
    [SerializeField] protected Slider _mainHealth;
    [SerializeField] public bool hitMain = false;
    [SerializeField] public bool punch1 = true;
    [SerializeField] private GameObject hitVfx;

    private float countTime;

    public enum State
    {
        Idle, Punch1, Punch2, LastHit, Die, Hited
    }    
    [SerializeField] public State _state = State.Idle;
    
    void Start()
    {        
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        _slider.value = currentHealth / 15;
        if (playerController.isFightingBoss)
        {
            countTime += Time.deltaTime % 60;
            if (currentHealth <= 0)
            {
                ChangeState(State.Die);
            }
            else if (playerController.Lose)
            {
                ChangeState(State.LastHit);
            } else if (punch1)
            {
                ChangeState(State.Punch1);
            }
            else 
            {
                ChangeState(State.Punch2);
            }
            if (Input.GetMouseButtonDown(0))
            {
                ChangeState(State.Hited);
            }

            if (countTime % 2 >= 1.99)
            {
                if (currentHealth > 0)
                {
                    hitMain = true;
                    punch1 = !punch1;
                }
            }
        }
        switch (_state)
        {
            case State.Idle:
                break;
            case State.Punch1:
                break;
            case State.Punch2:
                break;
            case State.LastHit:
                break;
            case State.Hited:
                break;
            case State.Die:
                StartCoroutine(ExampleCoroutine());
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
            case State.Punch1:    
                hitMain = false;
                animator.ResetTrigger("Punch1");
                break;
            case State.Punch2:
                hitMain = false;
                animator.ResetTrigger("Punch2");
                break;
            case State.LastHit:
                animator.ResetTrigger("LastHit");
                break;
            case State.Hited:
                animator.ResetTrigger("Hited");
                break;
            case State.Die:
                animator.ResetTrigger("Die");
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
                hitMain = false;
                animator.SetTrigger("Idle");
                break;
            case State.Punch1:                
                animator.SetTrigger("Punch1");
                break;
            case State.Punch2:                
                animator.SetTrigger("Punch2");
                break;
            case State.LastHit:
                animator.SetTrigger("LastHit");
                break;
            case State.Hited:
                currentHealth -= modelCharactor.currentDamage;
                var hit = Instantiate(hitVfx, hitTar.position, hitTar.rotation);
                hit.SetActive(true);
                animator.SetTrigger("Hited");
                break;
            case State.Die:
                animator.SetTrigger("Die");
                break;
            //default:
            //   throw new ArgumentOutOfRangeException();
        }
    }
    
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(1.9f);
        playerController.FightingWin = true;
    }
}
