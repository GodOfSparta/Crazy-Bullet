using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider playerSlider;

    // ”становка максимального значени€ полосы здоровь€
    public void SetMaxHealth(int maxHealth)
    {
        playerSlider.maxValue = maxHealth;
        playerSlider.value = maxHealth;
    }

    // ”становка текущего значени€ полосы здоровь€
    public void SetHealth(int health)
    {
        playerSlider.value = health;
    }
}
