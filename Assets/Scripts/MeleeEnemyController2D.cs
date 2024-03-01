using System.Collections;
using UnityEngine;

public class MeleeEnemyController2D : MonoBehaviour
{
    public AudioClip painSound;
    public AudioClip deathSound;
    private AudioSource enemyAudioSource;
    private bool isAttackingAnimationPlaying = false;
    public int damage;
    public int maxHealth;
    private int enemyCurrentHealth;
    public EnemyHealthBar healthBar; // Присвойте в инспекторе

    public float speed;
    public float attackRange;
    public float timeBetweenAttacks;
    public float jumpForce;
    public float jumpCooldown;

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private float timeSinceLastAttack;
    private float timeSinceLastJump;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public float attackCooldown;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyCurrentHealth = maxHealth;
        enemyAudioSource = GetComponent<AudioSource>();
        groundCheck = transform.Find("GroundCheck"); // Эта строка добавлена
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < attackRange && Time.time - timeSinceLastAttack > attackCooldown)
        {
            // Запускаем анимацию атаки
            animator.SetBool("IsAttacking", true);

            // Производим атаку, только если анимация атаки проигрывается
            if (isAttackingAnimationPlaying)
            {
                AttackEnemy();
                timeSinceLastAttack = Time.time;
            }
        }
        else
        {
            Vector2 direction = player.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime);

            if (direction.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            animator.SetBool("IsMoving", true);

            animator.SetBool("IsAttacking", false);
            TryJump(); // Добавлен вызов метода TryJump()

        }
    }

    void TryJump()
    {
        if (Time.time - timeSinceLastJump > jumpCooldown)
        {
            Jump();
            timeSinceLastJump = Time.time;
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        enemyCurrentHealth -= damage;
        // Обновление интерфейса
        healthBar.SetHealth(enemyCurrentHealth);

        if (enemyCurrentHealth <= 0)
        {
            Die();
        }

        else
        {
            // Воспроизводим звук боли врага
            if (enemyAudioSource != null && painSound != null)
            {
                enemyAudioSource.PlayOneShot(painSound);
            }

            // Запускаем анимацию ранения
            animator.SetTrigger("IsHurt");
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        animator.SetTrigger("IsDead");
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<MeleeEnemyController2D>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(DestroyEnemyAfterAnimation());

        if (enemyAudioSource != null && deathSound != null)
        {
            enemyAudioSource.PlayOneShot(deathSound, 1f);
        }
    }

    IEnumerator DestroyEnemyAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);

        Destroy(gameObject);
    }

    void AttackEnemy()
    {
        isAttackingAnimationPlaying = true;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                if (Time.time - timeSinceLastAttack > attackCooldown)
                {
                    PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();

                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(damage);
                        Debug.Log("Player took damage from the enemy!");
                    }
                }
            }
        }
        // После завершения атаки сбрасываем флаг анимации атаки
        isAttackingAnimationPlaying = false;
    }

}