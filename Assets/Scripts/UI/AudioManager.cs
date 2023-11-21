using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip pauseSound, unpauseSound, jumpSound, hitSound, healSound, playerDeathSound, enemyDeathSound, playerAttackSound, finishGameSound;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPauseSound()
    {
        audioSource.PlayOneShot(pauseSound);
    }

    public void PlayUnpauseSound()
    {
        audioSource.PlayOneShot(unpauseSound);
    }

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }

    public void PlayHealSound()
    {
        audioSource.PlayOneShot(healSound);
    }

    public void PlayPlayerDeathSound()
    {
        audioSource.PlayOneShot(playerDeathSound);
    }

    public void PlayEnemyDeathSound()
    {
        audioSource.PlayOneShot(enemyDeathSound);
    }

    public void PlayPlayerAttackSound()
    {
        audioSource.PlayOneShot(playerAttackSound);
    }

    public void PlayFinishGameSound()
    {
        audioSource.PlayOneShot(finishGameSound);
    }
}
