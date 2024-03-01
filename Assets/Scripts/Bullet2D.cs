using UnityEngine;

public class Bullet2D : MonoBehaviour
{
    public int damage = 10;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ��� ������������ � ������, �������� ����� TakeDamage
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // �������� ��������� ���������� ������
            MeleeEnemyController2D enemyController = collision.gameObject.GetComponent<MeleeEnemyController2D>();

            // ���������, ��� ���� ����� ��������� ����������
            if (enemyController != null)
            {
                // ������� ���� �����
                enemyController.TakeDamage(damage);
                Debug.Log("Enemy took damage!");
            }
            else
            {
                Debug.LogError("Enemy controller is null!");
            }
        }
        else
        {
            Debug.Log("Bullet hit something other than an enemy.");
        }

        // ���������� ���� ����� ������������ � ����� ��������
        Destroy(gameObject);
    }
}
