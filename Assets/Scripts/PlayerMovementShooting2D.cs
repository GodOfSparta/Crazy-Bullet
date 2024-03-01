using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementShooting2D : MonoBehaviour
{
    public float timeBetweenShots = 1f;
    private float timeSinceLastShot;
    private bool isWalking = false;
    private SoundManager soundManager;
    public float speed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 10f;
    private bool isAlive = true;  // Добавьте переменную для отслеживания состояния "жив/мертв"
    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        soundManager = GetComponent<SoundManager>();
    }

    void Update()
    {
        if (isAlive)  // Проверка на живучесть перед выполнением управления
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Воспроизводим звук ходьбы, если игрок двигается и находится на земле
            if (isWalking && isGrounded && !soundManager.IsWalkingSoundPlaying())
            {
                soundManager.PlayFootstepsSound();
            }

            // Управление анимацией ходьбы
            if (Mathf.Abs(horizontalInput) > 0.1f && isGrounded)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }

            animator.SetBool("IsWalking", isWalking);

            // Управление анимацией движения
            animator.SetFloat("Horizontal", Mathf.Abs(horizontalInput));
            animator.SetFloat("Vertical", verticalInput);
            animator.SetFloat("Speed", Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput)));

            Vector2 movement = new Vector2(horizontalInput, verticalInput);
            rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);

            // Проверка на нахождение на земле
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

            // Прыжок
            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                soundManager.PlayJumpSound();
                // Установка параметра аниматора для воспроизведения анимации прыжка
                animator.SetBool("IsJumping", true);
            }
            else
            {
                // Сброс параметра аниматора, если персонаж на земле
                animator.SetBool("IsJumping", false);
            }

            // Падение
            if (!isGrounded && rb.velocity.y < 0)
            {
                // Установка параметра аниматора для воспроизведения анимации падения
                animator.SetBool("IsFalling", true);
            }
            else
            {
                // Сброс параметра аниматора, если персонаж не падает
                animator.SetBool("IsFalling", false);
            }

            // Поворот персонажа в направлении движения
            if (movement.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Поворот вправо
            }
            else if (movement.x < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Поворот влево
            }

            // Если нажата клавиша "Fire1" и прошло достаточно времени с последнего выстрела
            if (Input.GetButtonDown("Fire1") && Time.time - timeSinceLastShot > timeBetweenShots)
            {
                soundManager.PlayShootSound();
                Shoot();
                timeSinceLastShot = Time.time; // Запоминаем время последнего выстрела
            }
        }
    }
    void Shoot()
    {
        // Управление анимацией стрельбы
        animator.SetTrigger("Shoot");

        // Создаем снаряд из префаба
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Получаем компонент Rigidbody2D снаряда
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        // Поворачиваем спрайт пули в зависимости от направления движения игрока
        bullet.transform.localScale = new Vector3(transform.localScale.x, bullet.transform.localScale.y, bullet.transform.localScale.z);

        // Применяем силу выстрела в направлении, куда смотрит персонаж
        bulletRb.AddForce(firePoint.right * bulletForce * Mathf.Sign(transform.localScale.x), ForceMode2D.Impulse);
    }
    void Die()
    {
        // Выставляем флаг "мертв"
        isAlive = false;
        Debug.Log("Player died!");

        // Возможно, здесь будет код для загрузки экрана проигрыша или других действий.
    }
}
