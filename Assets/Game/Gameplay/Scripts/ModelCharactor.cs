using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModelCharactor : MonoBehaviour
{
    [SerializeField] private Transform vfxTar;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private CarController carController;
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private BossController bossController;
    [SerializeField] public List<GameObject> carsList = new List<GameObject>();
    [SerializeField] public List<GameObject> mainList = new List<GameObject>();
    [SerializeField] public float currentItem = 0;
    [SerializeField] public float currentHealth = 0;
    [SerializeField] public float currentDamage = 0;
    [SerializeField] public float bomb = 2;
    [SerializeField] public float pin = 1;
    [SerializeField] public Slider _slider;
    [SerializeField] private GameObject barItem;
    [SerializeField] private GameObject mainHealth;
    [SerializeField] private GameObject bossHealth;
    [SerializeField] private GameObject enemyHealth;
    [SerializeField] private GameObject img;
    [SerializeField] private GameObject upFormVfx;
    [SerializeField] private GameObject downFormVfx;
    [SerializeField] private GameObject textAddItem;
    [SerializeField] private GameObject textDelItem;
    [SerializeField] public List<bool> formList = new List<bool>();
    void Start()
    {
        formList[0] = true;
        formList[1] = false;
        formList[2] = false;
        barItem.SetActive(true);
        mainHealth.SetActive(false);
        bossHealth.SetActive(false);
        textAddItem.gameObject.SetActive(false);
        textDelItem.gameObject.SetActive(false);
    }
    void Update()
    {
        _slider.value = currentHealth / 5;
        if (!playerController.Win && !playerController.Lose)
        {
            if (playerController.isFightingEnemy)
            {
                barItem.SetActive(false);
                mainHealth.SetActive(true);
            } else if (playerController.isFightingBoss)
            {
                enemyHealth.SetActive(false);
                bossHealth.SetActive(true);
                barItem.SetActive(false);
                mainHealth.SetActive(true);
                Vector3 newPosition = new Vector3(260, 1018, 0);
                mainHealth.transform.position = newPosition;
                Vector3 newRotation = new Vector3(180, 0, 0);
                Vector3 newRotation2 = new Vector3(0, 0, 0);
                mainHealth.transform.eulerAngles = newRotation;
                img.transform.eulerAngles = newRotation2;
                Vector3 newPosition2 = new Vector3(260, 1010, 0);
                mainHealth.transform.position = newPosition2;
            }
            else
            {
                barItem.SetActive(true);
                mainHealth.SetActive(false);
            }
        }
                
        if (playerController.transtomain)
        {
            for (int i = 0; i < 3; i++)
            {
                if (carsList[i].activeSelf)
                {
                    formList[i] = true;
                    mainList[i].SetActive(true);
                }
                else
                {                    
                    formList[i] = false;
                    mainList[i].SetActive(false);
                }
            }

            if (formList[0])
            {
                currentDamage = 0.5f;
            } else if (formList[1])
            {
                currentDamage = 2.3f;
            } else if (formList[2])
            { 
                currentDamage = 4.8f;
            } 
        }
        ChangeClothes();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //other.GetComponent<BoxCollider>().enabled = false;
        if (other.tag == "bomb")
        {
            playerController.transtocar = false;
            if (currentHealth == 0 || currentHealth == 1)
            {
                if(currentItem != 0)
                {
                    Instantiate(downFormVfx, vfxTar.position, vfxTar.rotation);
                }
            }
            if (currentItem > 0)
            {
                var aMoney = Instantiate(textDelItem, textDelItem.transform.parent);
                aMoney.gameObject.SetActive(true);
                currentItem -= bomb;
                currentHealth -= bomb;
            }
        }
        if (other.tag == "battery")
        {
            if (currentHealth == 4)
            {
                if(!carsList[2].activeSelf)
                {
                    Instantiate(upFormVfx, vfxTar.position, vfxTar.rotation);
                }
            }
            playerController.transtocar = false;
            if (currentItem < 15)
            {
                var aMoney = Instantiate(textAddItem, textAddItem.transform.parent);
                aMoney.gameObject.SetActive(true);
                currentItem += pin;
                currentHealth += pin;
            }
        }
    }
    
    private void ChangeClothes()
    {
        if (currentItem == 0 || currentItem < 5)
        {
            if (currentItem == 4) currentHealth = 4;
            if (currentItem == 3) currentHealth = 3;
            carsList[0].SetActive(true);
            carsList[1].SetActive(false);
            carsList[2].SetActive(false);

        }
        else if (currentItem == 5 || currentItem < 10)
        {
            if (currentItem == 5) currentHealth = 0;
            if (currentItem == 9) currentHealth = 4;
            if (currentItem == 8) currentHealth = 3;
            carsList[1].SetActive(true);
            carsList[0].SetActive(false);
            carsList[2].SetActive(false);
                    
        }
        else if (currentItem == 10 || currentItem < 15)
        {
            if (currentItem == 10) currentHealth = 0;
            if (currentItem == 14) currentHealth = 4;
            if (currentItem == 13) currentHealth = 3;
            carsList[2].SetActive(true);
            carsList[1].SetActive(false);
            carsList[0].SetActive(false);
        }
    }
}
