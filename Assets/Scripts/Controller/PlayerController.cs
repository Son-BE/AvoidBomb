using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private int maxHp = 100;                              // �ִ� HP    
    private int curHp;                                  // ���� HP
    [SerializeField] private Slider hpSlider;           // HP�����̴�

    public int maxLife = 3;                             // �ִ� ���
    private int curLife;                                // ���� ���
    public TextMeshProUGUI lifeTxt;

    [SerializeField] private GameObject RetryPanel;     // ����� �ǳ�
    [SerializeField] private Button ReStartBtn;         // ����� ��ư
    [SerializeField] private Button GameEndBtn2;        // ���� ���� ��ư 2

    [SerializeField] private GameObject VictoryPanel;   // �¸� �ǳ�
    [SerializeField] private Button GameEndBtn;         // ���� ���� ��ư

    private void Start()
    {
        curHp = maxHp;                                   // ���� ���� �� �� HP �ʱ�ȭ
        curLife = maxLife;                               // ���� ���� �� �� ��� �ʱ�ȭ
        UpdateHpUI();
        UpdateLife();        
        RetryPanel.SetActive(false);
        VictoryPanel.SetActive(false);
        hpSlider.value = 1.0f;
    }
       

    private void UpdateHpUI()
    {
        hpSlider.value = (curHp / maxHp) % 0.33f;
    }

    private void UpdateLife()
    {
        lifeTxt.text = curLife.ToString();
        if(curLife <=0)
        {
            RestartGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("bomb"))
        {
            // ��ź�� �ε����� �� HP ����
            TakeDamage(100);
        }

        if(collision.CompareTag("Goal"))
        {
            StageClear();
        }

        if(collision.CompareTag("Victory"))
        {
            Victory();
        }
    }

    private void Victory()
    {
        VictoryPanel.SetActive(true);
        GameEndBtn.onClick.AddListener(GameEnd);
    }

    private void GameEnd()
    {
        SceneManager.LoadScene(0);
    }

    private void StageClear()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    private void TakeDamage(int damage)
    {
        curHp -= damage;
        UpdateHpUI();
        if(curHp <= 0) 
        {
            transform.position = new Vector3(-8f, 0, 0);

            curLife -= 1;
            UpdateLife();

            if(curLife > 0)
            {
                curHp = maxHp;
                UpdateHpUI();
                Time.timeScale = 1f;
            }
            else
            {
                RestartGame();
            }
            
        }
    }

    private void RestartGame()
    {
        RetryPanel.SetActive(true);
        ReStartBtn.onClick.AddListener(GameOver);
        GameEndBtn2.onClick.AddListener(GameEnd);
    }

    private void GameOver()
    {
        string reLoadScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(reLoadScene);
        Time.timeScale = 1f;
    }
}
