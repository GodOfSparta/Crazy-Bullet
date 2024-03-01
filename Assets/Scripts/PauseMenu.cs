using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    void Start()
    {
        // Начинаем с того, что скрываем меню паузы при старте
        Resume();
    }

    void Update()
    {
        // Отслеживаем нажатие клавиши "Escape" для открытия/закрытия меню паузы
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuUI.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Возобновляем время
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Останавливаем время
    }

    public void Restart()
    {
        // Получаем сохраненную позицию Checkpoint
        Vector3 respawnPosition = GameManager.GetRespawnPosition();

        // Если позиция не была сохранена, просто перезапускаем текущую сцену
        if (respawnPosition == Vector3.zero)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            // Загружаем текущую сцену, и когда она загружена, устанавливаем позицию игрока
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);

            GameManager.OnSceneLoaded += () =>
            {
                GameObject playerObject = GameObject.FindWithTag("Player");

                if (playerObject != null)
                {
                    PlayerMovementShooting2D player = playerObject.GetComponent<PlayerMovementShooting2D>();

                    if (player != null)
                    {
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
        }
    }

    public void Exit()
    {
        // Выход из игры (работает в режиме Play в редакторе)
        Application.Quit();
    }
}
