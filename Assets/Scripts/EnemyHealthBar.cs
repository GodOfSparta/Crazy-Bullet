using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider enemySlider;

    // ��������� ������������� �������� ������ ��������
    public void SetMaxHealth(int maxHealth)
    {
        enemySlider.maxValue = maxHealth;
        enemySlider.value = maxHealth;
    }

    // ��������� �������� �������� ������ ��������
    public void SetHealth(int currentHealth)
    {
        enemySlider.value = currentHealth;
        Debug.Log("Enemy health set to: " + currentHealth);  // �������� ��� ������
    }
}
