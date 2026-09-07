using UnityEngine;


public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp = 100;
    public float ms = 2f;

    protected Transform player;

    [Header("pengaturan state machine")]
    [SerializeField] private float jarakDeteksi = 6f;
    [SerializeField] private float jarakSerang = 1.5f;
    [SerializeField] private float jedaSerang = 1f;

    private statezombie currentState = statezombie.IDLE;

    private float lastAttackTime = 0f;

    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if(playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        PeriksaTransisi();

        switch (currentState)
        {
            case statezombie.IDLE:    PerilakuIdle();   break;
            case statezombie.PATROL:  PerilakuPatrol(); break;
            case statezombie.CHASE:   PerilakuChase();  break;
            case statezombie.ATTACK:  PerilakuAttack(); break;
        }

        void PerilakuIdle()   
        {  void PerilakuPatrol() { Debug.Log(name + ": PATROL"); }
            void PerilakuChase()  { Debug.Log(name + ": CHASE"); }
            void PerilakuAttack() { Debug.Log(name + ": ATTACK"); }
        }
           
    }

    private void PeriksaTransisi()
    {
        if (player == null)
        {
            currentState = statezombie.IDLE;
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= jarakSerang)
        {
            currentState = statezombie.ATTACK;
        }
        else if (distanceToPlayer <= jarakDeteksi)
        {
            currentState = statezombie.CHASE;
        }
        else
        {
            currentState = statezombie.PATROL;
        }
    }

    protected virtual void PerilakuIdle()
    {
        // Idle behavior; enemy waits for the player to enter detection range.
    }

    protected virtual void PerilakuPatrol()
    {
        // Patrol behavior placeholder. Extend in subclasses for real patrol routes.
    }

    protected virtual void PerilakuChase()
    {
        Kejar();
    }

    protected virtual void PerilakuAttack()
    {
        if (player == null) return;

        if (Time.time - lastAttackTime >= jedaSerang)
        {
            Serang();
            lastAttackTime = Time.time;
        }
    }

    public void Kejar()
    {
        if (player == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            ms * Time.deltaTime
        );
    }

    public virtual void Serang()
    {
        Debug.Log("Enemy Menyerang");
    }

    public void TakeDamage(int jumlah)
    {
        hp -= jumlah;
        Debug.Log($"{gameObject.name} kena damage {jumlah}, Hp sisa : {hp}");
        if(hp <= 0)
        {
            Mati();
        }
    }

    private void Mati()
    {
        Debug.Log($"{gameObject.name} mati!");
        Destroy(gameObject);
    }
}