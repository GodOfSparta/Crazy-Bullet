using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static Vector3 respawnPositionKey = new Vector3(0, 0, 0); // Ключ для сохранения позиции

    // Добавьте событие для уведомления о загрузке сцены
    public delegate void SceneLoadedAction();
    public static event SceneLoadedAction OnSceneLoaded;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void SetRespawnPosition(Vector3 position)
    {
        respawnPositionKey = position;
        SaveRespawnPosition();
    }

    public static Vector3 GetRespawnPosition()
    {
        return respawnPositionKey;
    }

    private static void SaveRespawnPosition()
    {
        PlayerPrefs.SetFloat("RespawnX", respawnPositionKey.x);
        PlayerPrefs.SetFloat("RespawnY", respawnPositionKey.y);
        PlayerPrefs.SetFloat("RespawnZ", respawnPositionKey.z);
        PlayerPrefs.Save();
    }

    private static void LoadRespawnPosition()
    {
        float x = PlayerPrefs.GetFloat("RespawnX");
        float y = PlayerPrefs.GetFloat("RespawnY");
        float z = PlayerPrefs.GetFloat("RespawnZ");
        respawnPositionKey = new Vector3(x, y, z);
    }

    // Другие методы и логика управления игрой...

    void OnApplicationQuit()
    {
        SaveRespawnPosition();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveRespawnPosition();
        }
    }

    // Добавьте метод для обработки события загрузки сцены
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoadedCallback;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoadedCallback;
    }

    void OnSceneLoadedCallback(Scene scene, LoadSceneMode mode)
    {
        // Вызываем событие после загрузки сцены
        OnSceneLoaded?.Invoke();
    }
}