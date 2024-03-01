using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // ���������, ���� �� ����������� ������� ���������� Check Point
        if (PlayerPrefs.HasKey("RespawnX") && PlayerPrefs.HasKey("RespawnY") && PlayerPrefs.HasKey("RespawnZ"))
        {
            // ��������� ��������� ������� Check Point
            float respawnX = PlayerPrefs.GetFloat("RespawnX");
            float respawnY = PlayerPrefs.GetFloat("RespawnY");
            float respawnZ = PlayerPrefs.GetFloat("RespawnZ");

            Vector3 respawnPosition = new Vector3(respawnX, respawnY, respawnZ);

            // ������������� ������� ������ ����� �������� �����
            GameManager.OnSceneLoaded += () =>
            {
                // ������� ������ ������ � �����
                GameObject playerObject = GameObject.FindWithTag("Player");

                // ���������, ������ �� ������
                if (playerObject != null)
                {
                    // �������� ��������� PlayerMovementShooting2D �� ���������� �������
                    PlayerMovementShooting2D player = playerObject.GetComponent<PlayerMovementShooting2D>();

                    // ���������, ������ �� ���������
                    if (player != null)
                    {
                        // ������������� ������� ������
                        player.transform.position = respawnPosition;
                    }
                    else
                    {
                        Debug.LogWarning("PlayerMovementShooting2D component not found on the player object.");
                    }
                }
                else
                {
                    Debug.LogWarning("Player object not found in the scene.");
                }
            };

            // ��������� �����
            SceneManager.LoadScene("1_level");
        }
        else
        {
            // ���� ��� ����������� �������, ������ ��������� �������
            SceneManager.LoadScene("1_level");
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}