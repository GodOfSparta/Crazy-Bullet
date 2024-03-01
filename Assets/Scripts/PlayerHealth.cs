using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public AudioClip painSound;
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    private AudioSource audioSource;
    public PlayerHealthBar healthBar;

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>(); // Добавьте эту строку
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Дополнительные действия при получении урона, например, воспроизведение звука, анимации и т. д.
        Debug.Log("Player took damage!");

        // Обновление интерфейса
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
                audioSource.PlayOneShot(painSound, 0.6f);
        }
    }

    void Die()
    {
        // Здесь можно добавить воспроизведение анимации смерти и другие действия

        // Устанавливаем позицию respawn
        transform.position = GameManager.GetRespawnPosition();
        currentHealth = maxHealth; // Возможно, вам нужно сбросить здоровье после смерти

        Debug.Log("Player died!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что объект, сталкивающийся с игроком, является врагом
        if (other.CompareTag("Enemy"))
        {
            // Получаем компонент управления врагом
            MeleeEnemyController2D enemyController = other.GetComponent<MeleeEnemyController2D>();

            // Проверяем, что враг имеет компонент управления
            if (enemyController != null)
            {
                // Наносим урон игроку
                TakeDamage(enemyController.damage);
                Debug.Log("Player took damage from the enemy!");
            }
            else
            {
                Debug.LogError("Enemy controller is null!");
            }
        }
    }
}
