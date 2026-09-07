using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float kecepatan = 5f;

    private Vector2 arahGerak;

    public int skor = 0;

    public GameManager gameManager;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        if (gameManager == null)
        {
            Debug.LogError("GameManager tidak ditemukan. Pasang GameManager di inspector atau buat satu objek dengan script GameManager.");
        }
    }

    void OnMove(InputValue value)
    {
        arahGerak = value.Get<Vector2>();
    }

    void Update()
    {
        Vector3 arah = new Vector3(
            arahGerak.x,
            arahGerak.y,
            0
        );

        transform.position += arah * kecepatan * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Coin"))
            return;

        Destroy(other.gameObject);

        skor++;
        Debug.Log("Skor: " + skor);

        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        if (gameManager != null)
        {
            gameManager.AmbilKoin();
        }
        else
        {
            Debug.LogError("GameManager tidak ditemukan saat mengambil koin.");
        }
    }
}
