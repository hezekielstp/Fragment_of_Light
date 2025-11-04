using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Variabel gerak
    public float speed = 5f;
    public float jumpForce = 7f;

    // --- VARIABEL BARU ---
    // Seberapa kuat kita "memotong" lompatan saat tombol dilepas
    // 0.5f berarti kecepatan vertikal akan dipotong 50%
    public float jumpCutMultiplier = 0.5f;

    // Variabel internal
    private Rigidbody2D rb;
    private float horizontalInput;

    // --- Variabel untuk Cek Tanah ---
    private bool isGrounded;
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        // 1. Cek Pijakan (Ground Check)
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

        // 2. Baca Input Kiri/Kanan (A/D)
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // --- 3. LOGIKA LOMPAT (BAGIAN 1: SAAT DITEKAN) ---
        // Ini masih sama: hanya bisa lompat jika di tanah
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            // Terapkan gaya lompat penuh
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // --- 4. LOGIKA LOMPAT (BAGIAN 2: SAAT DILEPAS) ---
        // Ini adalah LOGIKA BARU yang Anda minta
        if (Input.GetKeyUp(KeyCode.W))
        {
            // Cek: Apakah tombol lompat dilepas SAAT kita sedang naik ke atas?
            if (rb.velocity.y > 0)
            {
                // Jika iya, potong lompatannya!
                // Kita kalikan kecepatan Y saat ini dengan multiplier
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutMultiplier);
            }
        }
    }

    void FixedUpdate()
    {
        // Terapkan Gerakan Kiri/Kanan
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }
}