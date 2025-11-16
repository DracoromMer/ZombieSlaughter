using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerScript : MonoBehaviour
{
    public float moveSpeed;
    private float Directionx;
    private float Directiony;
    public float attackRange = 0.5f;
    public Transform Hitbox;

    public GameObject GameOverScreen;

        public GameObject VictoryScreen;


    public float InvibilityTime = 1.5f;

        public float invincibilityDeltaTime = 0.15f;
    public Rigidbody2D Rb;
    private Vector2 moveDirection;
    public Animator anim;
    public BarraDeVida barradevida;
    public LayerMask enemyLayer;

    private bool IsInvicible;
public TextMeshProUGUI killCountText;
[SerializeField] private float startTime = 60f; // Set your desired start time in seconds
        private float currentTime;
        // [SerializeField] private TextMeshProUGUI timerText; // Use this if using TextMeshPro
        [SerializeField] private TextMeshProUGUI timerText; // Use this if using UI.Text
private float kills;
    public float maxHealth = 100f;

    public float curHealth;
    // Start is called before the first frame update
    // Update is called once per frame
    void Start()
    {
        currentTime = startTime;
        kills = 0;
        barradevida.MarcarVidaMaxima(maxHealth);
        curHealth = maxHealth;
    }
    void Update()
    {
        Countdown();
        ProcessInputs();
        Move();
        Animate();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY);

    }

    void Move()
    {
        Rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        Hitbox.transform.position = new Vector2(transform.position.x + Directionx/3, transform.position.y + Directiony/3);
        {
        if (Mathf.Abs(moveDirection.x) > 0 || Mathf.Abs(moveDirection.y) > 0)
        {
            anim.SetBool("IsMoving", true);
            Directionx = moveDirection.x;
            Directiony = moveDirection.y;
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
        }
    }

    void Animate()
    {
        anim.SetFloat("Speedx", Directionx);
        anim.SetFloat("Speedy", Directiony);

    }
    void Attack()
    {
        
            anim.SetTrigger("Attack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(Hitbox.position, attackRange, enemyLayer);

            foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<ZombieScript>().zombieDeath(1);
            Kill();
        }
    }
    public void takeDamage(float damage)
    {
        if (IsInvicible == true) return;
        curHealth = curHealth - damage;
        barradevida.MarcarVida(curHealth);
        if (curHealth <= 0)
        {
            GameOverScreen.SetActive(true);
            anim.SetBool("IsDead", true);
        }
        StartCoroutine(BecomeTemporarilyInvincible());
    }
    public void Kill()
    {
        kills ++;
        killCountText.text = kills.ToString();
    }

        private void Countdown()
    {
        if (currentTime > 0)
            {
                currentTime -= Time.deltaTime; // Decrease time by the duration of the last frame
                DisplayTime(currentTime);
            }
            else
            {
                currentTime = 0; // Prevent negative values
                DisplayTime(currentTime);
                // Trigger an event when the timer finishes (e.g., end game, show message)
                if (kills < 100)
                {
                    GameOverScreen.SetActive(true);
                }
                else
            {
                VictoryScreen.SetActive(true);
            }
                Debug.Log("Countdown Finished!");
            }
    }

    void DisplayTime(float timeToDisplay)
        {
            // Calculate minutes and seconds
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            // Format the time as "MM:SS"
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        private IEnumerator BecomeTemporarilyInvincible()
    {
        Debug.Log("Player turned invincible!");
        IsInvicible = true;

        for (float i = 0; i < InvibilityTime; i += invincibilityDeltaTime)
    {   
            yield return new WaitForSeconds(invincibilityDeltaTime);
            yield return new WaitForSeconds(invincibilityDeltaTime);
    }

        IsInvicible = false;
        Debug.Log("Player is no longer invincible!");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SmallChest"))
            {
            curHealth = curHealth + 10;

            barradevida.MarcarVida(curHealth);

            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("BigChest"))
            {
            curHealth = curHealth + 30;

            barradevida.MarcarVida(curHealth);

            Destroy(collision.gameObject);
        }
    }
}
