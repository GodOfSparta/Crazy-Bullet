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

    // ��������� ���� �����, ����� ������������� ���� ������
    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    // ��������� ���� �����, ����� ������������� ���� ��������
    public void PlayShootSound()
    {
        // ������������� ���� ��������
        audioSource.PlayOneShot(shootSound, 0.1f);
    }

    // ��������� ���� ����� ��� ��������������� ����� ������
    public void PlayFootstepsSound()
    {
        audioSource.PlayOneShot(footstepsSound, 0.1f);
        isWalkingSoundPlaying = true;
        StartCoroutine(ResetWalkingSoundFlag());
    }

    // ���������, ��������������� �� ���� ������ � ������ ������
    public bool IsWalkingSoundPlaying()
    {
        return isWalkingSoundPlaying;
    }

    // ���������� ���� ��������������� ����� ������ ����� ��������� ��������
    IEnumerator ResetWalkingSoundFlag()
    {
        yield return new WaitForSeconds(footstepsSound.length);
        isWalkingSoundPlaying = false;
    }
}
