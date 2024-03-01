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
        animator = GetComponent<Animator>(); // �������� ��� ������
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // �������������� �������� ��� ��������� �����, ��������, ��������������� �����, �������� � �. �.
        Debug.Log("Player took damage!");

        // ���������� ����������
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
        // ����� ����� �������� ��������������� �������� ������ � ������ ��������

        // ������������� ������� respawn
        transform.position = GameManager.GetRespawnPosition();
        currentHealth = maxHealth; // ��������, ��� ����� �������� �������� ����� ������

        Debug.Log("Player died!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, ��� ������, �������������� � �������, �������� ������
        if (other.CompareTag("Enemy"))
        {
            // �������� ��������� ���������� ������
            MeleeEnemyController2D enemyController = other.GetComponent<MeleeEnemyController2D>();

            // ���������, ��� ���� ����� ��������� ����������
            if (enemyController != null)
            {
                // ������� ���� ������
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
