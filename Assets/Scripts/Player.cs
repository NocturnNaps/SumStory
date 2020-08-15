using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //essential
    [SerializeField] int PlayerHealth = 100;
    [SerializeField] float PlayerSpeed = 5f;
    [SerializeField] float StartingSpeed;
    [SerializeField] float AttackDuration = 0.5f;
    [SerializeField] public float Hitpoints = 25;

    //states
    bool isAlive = true;
    bool Attacking = false;
    public bool CanAttack = true;

    //cache
    Vector3 change;
    Rigidbody2D myRigidbody2d;
    Animator myAnimator;
    HealthText myhealthText;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2d = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myhealthText = FindObjectOfType<HealthText>();
        myhealthText.SetHealthText(PlayerHealth);
        StartingSpeed = PlayerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Move();
            
            if (CanAttack)
            {
                SwingSword();
            }
        }
    }

    private void Move()
    {
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        Vector3 PlayerVelocity = new Vector3(change.x * PlayerSpeed, change.y * PlayerSpeed);

        if (change != Vector3.zero)
        {
            myAnimator.SetFloat("X", change.x);
            myAnimator.SetFloat("Y", change.y);
            myAnimator.SetBool("Moving", true);
        }
        else
        {
            myAnimator.SetBool("Moving", false);
        }

        myRigidbody2d.velocity = PlayerVelocity; 
    }
    
    private void SwingSword()
    {
        if (Input.GetButtonDown("Fire1"))
        {
          StartCoroutine(TriggerAttack());
        }
    }
    IEnumerator TriggerAttack()
    {
        if (Attacking) yield break;
        Attacking = true;
        PlayerSpeed = 0;
        myAnimator.SetBool("Attacking", true);
        yield return new WaitForSeconds(AttackDuration);
        PlayerSpeed = StartingSpeed;
        myAnimator.SetBool("Attacking", false);
        Attacking = false;

    }
    public void DemageHealth(int Hitpoints)
    {
        PlayerHealth -= Hitpoints;
        myhealthText.SetHealthText(PlayerHealth);

        if (PlayerHealth <= 0)
        {
            isAlive = false;
        }
    }

    public int GetHealth()
    {
        return PlayerHealth;
    }
}
