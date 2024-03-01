using UnityEngine;

public class Bullet2D : MonoBehaviour
{
    public int damage = 10;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // При столкновении с врагом, вызываем метод TakeDamage
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Получаем компонент управления врагом
            MeleeEnemyController2D enemyController = collision.gameObject.GetComponent<MeleeEnemyController2D>();

            // Проверяем, что враг имеет компонент управления
            if (enemyController != null)
            {
                // Наносим урон врагу
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

        // Уничтожаем пулю после столкновения с любым объектом
        Destroy(gameObject);
    }
}
