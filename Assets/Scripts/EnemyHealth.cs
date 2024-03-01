using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyMaxHealth;
    private int enemyCurrentHealth;
    public EnemyHealthBar enemyHealthBar;

    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;

        // Инициализация полосы здоровья
        if (enemyHealthBar != null)
        {
            enemyHealthBar.SetMaxHealth(enemyMaxHealth);
        }
        else
        {
            Debug.LogWarning("HealthBar not assigned to EnemyHealth script.");
        }
    }

    public void TakeDamage(int damage)
    {
        enemyCurrentHealth -= damage;
        Debug.Log("Enemy current health: " + enemyCurrentHealth);  // Добавьте эту строку
        enemyHealthBar.SetHealth(enemyCurrentHealth);

        // Дополнительные действия при получении урона, например, воспроизведение звука, анимации и т. д.
        Debug.Log("Enemy took damage!");

        if (enemyCurrentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Дополнительные действия при смерти врага, например, воспроизведение анимации и т. д.
        Debug.Log("Enemy died!");

        // Уничтожаем объект врага
        Destroy(gameObject);
    }
}
