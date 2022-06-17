using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransFormMain : MonoBehaviour
{
    [SerializeField] private Transform hitTar; 
    [SerializeField] private PlayerController playerController;
    [SerializeField] private CarController carController;
    [SerializeField] private ModelCharactor modelCharactor;
    [SerializeField] private EnemyController enemyController;
    [SerializeField] public Slider enemyHealth;
    [SerializeField] public Slider _mainHealth;
    [SerializeField] private BossController bossController;
    [SerializeField] private GameObject flyVfx;
    [SerializeField] private GameObject hitVfx;

    private int i = 0;
    float count = 0;
    private Animator animator;
    private Collider m_Collider;

    private enum State
    {
        Idle,
        ToMain,
        ToCar,
        Punch1,
        Punch2,
        LastHit,
        Die,
        Fly,
        Hited
    }
    private State _state = State.ToMain;
    
    void Start()
    {
        m_Collider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        flyVfx.SetActive(false);
        hitVfx.SetActive(false);
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
            case State.ToMain:
                animator.SetTrigger("TransToMain");
                break;
            case State.ToCar:
                animator.SetTrigger("TransToCar");
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
            case State.Die:
                m_Collider.enabled = false;
                animator.SetTrigger("Die");
                break;
            case State.Fly:
                animator.SetTrigger("Fly");
                break;
            case State.Hited:
                animator.SetTrigger("Hited");
                break;
        }
    }
    
    private void ExitOldState()
    {
        switch (_state)
        {
            case State.Idle:
                animator.SetTrigger("Idle");
                break;
            case State.ToMain:
                animator.ResetTrigger("TransToMain");
                break;
            case State.ToCar:
                animator.ResetTrigger("TransToCar");
                break;
            case State.Punch1:
                animator.ResetTrigger("Punch1");
                break;
            case State.Punch2:
                animator.ResetTrigger("Punch2");
                break;
            case State.LastHit:
                animator.ResetTrigger("LastHit");
                break;
            case State.Die:
                animator.ResetTrigger("Die");
                break;            
            case State.Fly:
                animator.ResetTrigger("Fly");
                break;
            case State.Hited:
                animator.ResetTrigger("Hited");
                break;
        }
    }

    void Update()
    {
        if (playerController.Lose)
        {
            playerController.transtomain = false;
            ChangeState(State.Die);
        } else
        if (carController.isFlying)
        {
            if (count < 1f) count += Time.deltaTime % 60;
            else
            {
                playerController.transtomain = false;
                flyVfx.SetActive(true);
                ChangeState(State.Fly);
            }
        } else flyVfx.SetActive(false);
        
        if (playerController.transtocar)
        {   
            ChangeState(State.ToCar);
        }
        if (playerController.transtomain)
        {   
            ChangeState(State.ToMain);
        }      
        if (!playerController.Lose)
        {
            if (enemyController.hitMain)
            {
                hitVfx.SetActive(true);
                ChangeState(State.Hited);
            }
        }
        if (playerController.isFightingEnemy)
        {
            if (!playerController.Lose)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    i++;
                    if(enemyHealth.value <= 0.09)
                    {
                        ChangeState(State.LastHit);
                    }
                    else if(i % 2 != 0) ChangeState(State.Punch1);
                    else if(i % 2 == 0) ChangeState(State.Punch2);
                }
            }

        }else if (playerController.isFightingBoss)
        {
            if (!playerController.Lose)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    i++;
                    if(bossController.currentHealth <= 0.09)
                    {
                        ChangeState(State.LastHit);
                    }
                    else if(i % 2 != 0) ChangeState(State.Punch1);
                    else if(i % 2 == 0) ChangeState(State.Punch2);
                }
            }
        } else _mainHealth.value = 1;
    }
    
    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "hit")
        {
            if (playerController.isFightingBoss) _mainHealth.value -= 15;
            var hit = Instantiate(hitVfx, hitTar.position, hitTar.rotation);
            hit.SetActive(true);
            if (modelCharactor.currentItem <= 0)
            {
                _mainHealth.value = 0;
            }
            else if (modelCharactor.formList[0])
            {
                _mainHealth.value -= 3.9f;
            } else if (modelCharactor.formList[1])
            {
                if(!enemyController.die) _mainHealth.value -= 5f / (10 * modelCharactor.currentItem);
                else _mainHealth.value -= 8.9f;
            } 
            else if (modelCharactor.formList[2])
                _mainHealth.value -= 5f / (10 * modelCharactor.currentItem);
        }
    }
}