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
    private int maxHp = 100;                              // 최대 HP    
    private int curHp;                                  // 현재 HP
    [SerializeField] private Slider hpSlider;           // HP슬라이더

    public int maxLife = 3;                             // 최대 목숨
    private int curLife;                                // 현재 목숨
    public TextMeshProUGUI lifeTxt;

    [SerializeField] private GameObject RetryPanel;     // 재시작 판넬
    [SerializeField] private Button ReStartBtn;         // 재시작 버튼
    [SerializeField] private Button GameEndBtn2;        // 게임 종료 버튼 2

    [SerializeField] private GameObject VictoryPanel;   // 승리 판넬
    [SerializeField] private Button GameEndBtn;         // 게임 종료 버튼

    private void Start()
    {
        curHp = maxHp;                                   // 게임 시작 할 때 HP 초기화
        curLife = maxLife;                               // 게임 시작 할 때 목숨 초기화
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
            // 폭탄에 부딪혔을 때 HP 감소
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
