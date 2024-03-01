using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Проверяем, есть ли сохраненная позиция последнего Check Point
        if (PlayerPrefs.HasKey("RespawnX") && PlayerPrefs.HasKey("RespawnY") && PlayerPrefs.HasKey("RespawnZ"))
        {
            // Загружаем последнюю позицию Check Point
            float respawnX = PlayerPrefs.GetFloat("RespawnX");
            float respawnY = PlayerPrefs.GetFloat("RespawnY");
            float respawnZ = PlayerPrefs.GetFloat("RespawnZ");

            Vector3 respawnPosition = new Vector3(respawnX, respawnY, respawnZ);

            // Устанавливаем позицию игрока после загрузки сцены
            GameManager.OnSceneLoaded += () =>
            {
                // Находим объект игрока в сцене
                GameObject playerObject = GameObject.FindWithTag("Player");

                // Проверяем, найден ли объект
                if (playerObject != null)
                {
                    // Получаем компонент PlayerMovementShooting2D из найденного объекта
                    PlayerMovementShooting2D player = playerObject.GetComponent<PlayerMovementShooting2D>();

                    // Проверяем, найден ли компонент
                    if (player != null)
                    {
                        // Устанавливаем позицию игрока
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

            // Загружаем сцену
            SceneManager.LoadScene("1_level");
        }
        else
        {
            // Если нет сохраненной позиции, просто загружаем уровень
            SceneManager.LoadScene("1_level");
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}