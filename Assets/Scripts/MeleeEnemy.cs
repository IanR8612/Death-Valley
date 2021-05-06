using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{

    public float AttackDistance; //minumum distance for attack
    public float Timer;
    public float damage = 1;
    //public float MoveSpeed;

    private bool attackMode = false;
    private bool inRange; //check if player is in range
    private bool cooling; //check enemy cooldown after attack
    private float intTimer;
    private float Distance;

    public Transform Raycast;
    public LayerMask RaycastMask;
    public float RaycastLength;
    private RaycastHit2D hit;

    private Animator anim;

    private GameObject target;
    private GameObject player;
    private Player playerHealth;

    void Awake()
    {
        intTimer = Timer; //store the initial value of timer
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            hit = Physics2D.Raycast(Raycast.position, Vector2.left, RaycastLength, RaycastMask);
            RaycastDebugger();
        }

        //when player is detected
        if(hit.collider != null)
        {
            EnemyLogic();
        }
        else if(hit.collider == null)
        {
            inRange = false;
        }

        if(inRange == false)
        {
            anim.SetBool("canWalk", false);
            StopAttack();
        }
    }

    void EnemyLogic()
    {
        Distance = Vector2.Distance(transform.position, target.transform.position);
        if(Distance > AttackDistance)
        {
            //Move();
            StopAttack();
        }
        else if(AttackDistance >= Distance && cooling == false)
        {
            Attack();
            
            playerHealth.TakeDamage(damage);
            Debug.Log("HP:" + playerHealth);
        }

        if (cooling)
        {
            CoolDown();
            anim.SetBool("Attack", false);
        }

    }

    /*void Move()
    {
        anim.SetBool("canWalk", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Melee_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, MoveSpeed * Time.deltaTime);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            target = collision.gameObject;
            inRange = true;
        }
    }

    void Attack()
    {
        Timer = intTimer; //reset timer when player enter attack range
        attackMode = true; //checks if enemt can still attack or not

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    void RaycastDebugger()
    {
        if(Distance > AttackDistance)
        {
            Debug.DrawRay(Raycast.position, Vector2.left * RaycastLength, Color.red);
        }
        else if(AttackDistance > Distance)
        {
            Debug.DrawRay(Raycast.position, Vector2.left * RaycastLength, Color.green);
        }
    }

    void CoolDown()
    {
        Timer -= Time.deltaTime;

        if(Timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            Timer = intTimer;
        }
    }

    public void TriggerCooling()
    {
        cooling = true;
    }
}
