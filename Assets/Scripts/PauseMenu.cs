using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    void Start()
    {
        // �������� � ����, ��� �������� ���� ����� ��� ������
        Resume();
    }

    void Update()
    {
        // ����������� ������� ������� "Escape" ��� ��������/�������� ���� �����
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
        Time.timeScale = 1f; // ������������ �����
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // ������������� �����
    }

    public void Restart()
    {
        // �������� ����������� ������� Checkpoint
        Vector3 respawnPosition = GameManager.GetRespawnPosition();

        // ���� ������� �� ���� ���������, ������ ������������� ������� �����
        if (respawnPosition == Vector3.zero)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            // ��������� ������� �����, � ����� ��� ���������, ������������� ������� ������
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
        // ����� �� ���� (�������� � ������ Play � ���������)
        Application.Quit();
    }
}
