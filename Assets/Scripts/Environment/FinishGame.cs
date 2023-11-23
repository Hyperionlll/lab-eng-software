using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    private bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isTriggered)
        {
            Time.timeScale = 0;
            AudioManager.instance.PlayFinishGameSound();
            MainGameUIManager mainGameUIManager = GameObject.Find("MainGameUIManager").GetComponent<MainGameUIManager>();
            mainGameUIManager.ShowGameWonScreen();
            isTriggered = true;
        }
    }
}
