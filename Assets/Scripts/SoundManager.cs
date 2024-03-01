using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip jumpSound;
    public AudioClip shootSound;
    public AudioClip footstepsSound;
    private bool isWalkingSoundPlaying = false;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Вызывайте этот метод, чтобы воспроизвести звук прыжка
    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    // Вызывайте этот метод, чтобы воспроизвести звук выстрела
    public void PlayShootSound()
    {
        // Воспроизводим звук выстрела
        audioSource.PlayOneShot(shootSound, 0.1f);
    }

    // Вызывайте этот метод для воспроизведения звука ходьбы
    public void PlayFootstepsSound()
    {
        audioSource.PlayOneShot(footstepsSound, 0.1f);
        isWalkingSoundPlaying = true;
        StartCoroutine(ResetWalkingSoundFlag());
    }

    // Проверяет, воспроизводится ли звук ходьбы в данный момент
    public bool IsWalkingSoundPlaying()
    {
        return isWalkingSoundPlaying;
    }

    // Сбрасывает флаг воспроизведения звука ходьбы после окончания анимации
    IEnumerator ResetWalkingSoundFlag()
    {
        yield return new WaitForSeconds(footstepsSound.length);
        isWalkingSoundPlaying = false;
    }
}
