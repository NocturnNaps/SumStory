using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //essentials
    [SerializeField] int Hitpoints = 25;
    [SerializeField] float timeToHit = 1;
    [SerializeField] public float speed = 3f;
    public float StartingSpeed;
    [SerializeField] float DistanceBetweenPlayer = 1f;
    [SerializeField] float TimeToStopHitting = 0.5f;
    [SerializeField] public float EnemyHP = 100;
    public bool HitTheEnemy = false;
    Vector2 DirectionFacing;

    //states
    private bool InRange;
    private bool InHitbox;
    bool damaging;
    public bool isHit = false;

    //cache
    private Transform target;
    private CircleCollider2D myCircleCollider2D;
    private CapsuleCollider2D myCapsuleCollider2D;
    private BoxCollider2D myBoxCollider2D;
    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;
    private Player player;
    private SpriteRenderer myRenderer; 
    private Vector3 localTarget;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myCapsuleCollider2D = GetComponentInChildren<CapsuleCollider2D>();
        myCircleCollider2D = GetComponentInChildren<CircleCollider2D>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        myRenderer = GetComponent<SpriteRenderer>();
        StartingSpeed = speed;
    }

    void Update()
    {
        if (HitTheEnemy && myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Sword")))
        {
            StartCoroutine(HitEnemy());
            myAnimator.SetBool("Moving", false);
            myRigidbody2D.velocity = Vector2.zero;
        }
        Move();
        Damage();
    }

    private void Move()
    {
        if (myCircleCollider2D.IsTouchingLayers(LayerMask.GetMask("Player")) && !HitTheEnemy)
        {
            if (Vector2.Distance(transform.position, target.position) >= DistanceBetweenPlayer)
            {

                if (Mathf.RoundToInt(transform.position.x) < Mathf.RoundToInt(target.position.x))
                {
                    myRigidbody2D.velocity = new Vector2(speed, myRigidbody2D.velocity.y);
                    DirectionFacing = new Vector2(1, DirectionFacing.y);
                }
                else if (Mathf.RoundToInt(transform.position.x) > Mathf.RoundToInt(target.position.x))
                {
                    myRigidbody2D.velocity = new Vector2(-speed, myRigidbody2D.velocity.y);
                    DirectionFacing = new Vector2(-1, DirectionFacing.y);
                }
                else
                {
                    myRigidbody2D.velocity = new Vector2(0, myRigidbody2D.velocity.y);
                }

                if (Mathf.RoundToInt(transform.position.y) < Mathf.RoundToInt(target.position.y))
                {
                    myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, speed);
                    DirectionFacing = new Vector2(DirectionFacing.x, 1);
                }
                else if (Mathf.RoundToInt(transform.position.y) > Mathf.RoundToInt(target.position.y))
                {
                    myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, -speed);
                    DirectionFacing = new Vector2(DirectionFacing.x, -1);
                }
                else
                {
                    myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, 0);
                }

                myAnimator.SetFloat("X", myRigidbody2D.velocity.x);
                myAnimator.SetFloat("Y", myRigidbody2D.velocity.y);
                myAnimator.SetBool("Moving", true);
            }
            else
            {
                myAnimator.SetBool("Moving", false);
                myRigidbody2D.velocity = Vector3.zero;
            }
        }
        else
        {
            myAnimator.SetBool("Moving", false);
            myRigidbody2D.velocity = Vector2.zero;
        }
    }
    private void Damage()
    {
        if (myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            InHitbox = true;
            myAnimator.SetBool("Attacking", true);
            myAnimator.SetFloat("X", DirectionFacing.x);
            myAnimator.SetFloat("Y", DirectionFacing.y);
            StartCoroutine(DamagePlayer());
        }
        else if (!myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            InHitbox = false;
            myAnimator.SetBool("Attacking", false);
        }
    }

    IEnumerator DamagePlayer()
    {
        if (damaging) yield break;

        damaging = true;

        if(player.GetHealth() <= 0) {damaging = false; yield break;}
        player.DemageHealth(Hitpoints);
        yield return new WaitForSeconds(timeToHit);
        if(!InHitbox) {damaging = false; yield break;}

        damaging = false;
    }
    public IEnumerator HitEnemy()
    {
        isHit = true;
        myRenderer.color = new Color32(241,135,135,255);
        yield return new WaitForSecondsRealtime(TimeToStopHitting);
        myRenderer.color = new Color(255,255,255,255);
        isHit = false;
        if (EnemyHP <= 0)
        {
            myAnimator.SetTrigger("Dead");
        }
        speed = StartingSpeed;
        HitTheEnemy = false;
    }

    public void DeadEnemy()
    {
        Hitpoints = 0;
        speed = 0;
    }
}
