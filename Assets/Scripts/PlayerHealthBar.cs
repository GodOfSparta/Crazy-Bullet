using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider playerSlider;

    // ��������� ������������� �������� ������ ��������
    public void SetMaxHealth(int maxHealth)
    {
        playerSlider.maxValue = maxHealth;
        playerSlider.value = maxHealth;
    }

    // ��������� �������� �������� ������ ��������
    public void SetHealth(int health)
    {
        playerSlider.value = health;
    }
}
