using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyMaxHealth;
    private int enemyCurrentHealth;
    public EnemyHealthBar enemyHealthBar;

    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;

        // ������������� ������ ��������
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
        Debug.Log("Enemy current health: " + enemyCurrentHealth);  // �������� ��� ������
        enemyHealthBar.SetHealth(enemyCurrentHealth);

        // �������������� �������� ��� ��������� �����, ��������, ��������������� �����, �������� � �. �.
        Debug.Log("Enemy took damage!");

        if (enemyCurrentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // �������������� �������� ��� ������ �����, ��������, ��������������� �������� � �. �.
        Debug.Log("Enemy died!");

        // ���������� ������ �����
        Destroy(gameObject);
    }
}
