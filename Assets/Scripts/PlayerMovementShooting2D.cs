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
    private bool isAlive = true;  // �������� ���������� ��� ������������ ��������� "���/�����"
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
        if (isAlive)  // �������� �� ��������� ����� ����������� ����������
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // ������������� ���� ������, ���� ����� ��������� � ��������� �� �����
            if (isWalking && isGrounded && !soundManager.IsWalkingSoundPlaying())
            {
                soundManager.PlayFootstepsSound();
            }

            // ���������� ��������� ������
            if (Mathf.Abs(horizontalInput) > 0.1f && isGrounded)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }

            animator.SetBool("IsWalking", isWalking);

            // ���������� ��������� ��������
            animator.SetFloat("Horizontal", Mathf.Abs(horizontalInput));
            animator.SetFloat("Vertical", verticalInput);
            animator.SetFloat("Speed", Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput)));

            Vector2 movement = new Vector2(horizontalInput, verticalInput);
            rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);

            // �������� �� ���������� �� �����
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

            // ������
            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                soundManager.PlayJumpSound();
                // ��������� ��������� ��������� ��� ��������������� �������� ������
                animator.SetBool("IsJumping", true);
            }
            else
            {
                // ����� ��������� ���������, ���� �������� �� �����
                animator.SetBool("IsJumping", false);
            }

            // �������
            if (!isGrounded && rb.velocity.y < 0)
            {
                // ��������� ��������� ��������� ��� ��������������� �������� �������
                animator.SetBool("IsFalling", true);
            }
            else
            {
                // ����� ��������� ���������, ���� �������� �� ������
                animator.SetBool("IsFalling", false);
            }

            // ������� ��������� � ����������� ��������
            if (movement.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // ������� ������
            }
            else if (movement.x < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // ������� �����
            }

            // ���� ������ ������� "Fire1" � ������ ���������� ������� � ���������� ��������
            if (Input.GetButtonDown("Fire1") && Time.time - timeSinceLastShot > timeBetweenShots)
            {
                soundManager.PlayShootSound();
                Shoot();
                timeSinceLastShot = Time.time; // ���������� ����� ���������� ��������
            }
        }
    }
    void Shoot()
    {
        // ���������� ��������� ��������
        animator.SetTrigger("Shoot");

        // ������� ������ �� �������
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // �������� ��������� Rigidbody2D �������
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        // ������������ ������ ���� � ����������� �� ����������� �������� ������
        bullet.transform.localScale = new Vector3(transform.localScale.x, bullet.transform.localScale.y, bullet.transform.localScale.z);

        // ��������� ���� �������� � �����������, ���� ������� ��������
        bulletRb.AddForce(firePoint.right * bulletForce * Mathf.Sign(transform.localScale.x), ForceMode2D.Impulse);
    }
    void Die()
    {
        // ���������� ���� "�����"
        isAlive = false;
        Debug.Log("Player died!");

        // ��������, ����� ����� ��� ��� �������� ������ ��������� ��� ������ ��������.
    }
}
