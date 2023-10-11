using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button gameStart;
    void Start()
    {
        gameStart.onClick.AddListener(GameStart);
    }

    private void GameStart()
    {
        SceneManager.LoadScene(2);
    }
}
