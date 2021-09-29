using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject _objPauseMenu = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_objPauseMenu.activeInHierarchy)
                OpenPause();
            else
                ClosePause();
        }
    }

    private void ClosePause()
    {
        Time.timeScale = 1f;
        _objPauseMenu.SetActive(false);
    }

    void OpenPause()
    {
        _objPauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        ClosePause();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
