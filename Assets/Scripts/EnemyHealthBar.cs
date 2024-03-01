using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider enemySlider;

    // Установка максимального значения полосы здоровья
    public void SetMaxHealth(int maxHealth)
    {
        enemySlider.maxValue = maxHealth;
        enemySlider.value = maxHealth;
    }

    // Установка текущего значения полосы здоровья
    public void SetHealth(int currentHealth)
    {
        enemySlider.value = currentHealth;
        Debug.Log("Enemy health set to: " + currentHealth);  // Добавьте эту строку
    }
}
