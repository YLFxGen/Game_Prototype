using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    public FloatVaule currentHealth;
    public MySignal playerHealthSignal;

    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Vector3 changeFix;
    private Animator myAnimator;


    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator.SetFloat("moveX", 0);
        myAnimator.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }else if(currentState == PlayerState.walk)
        {
            UpdateAnimation();
        }

    }

    void FixedUpdate()
    {
        changeFix = Vector3.zero;
        changeFix.x = Input.GetAxisRaw("Horizontal");
        changeFix.y = Input.GetAxisRaw("Vertical");
        if (currentState == PlayerState.walk)
        {
            if (changeFix != Vector3.zero)
            {
                MoveCharacter();
            }
        }
    }

    private IEnumerator AttackCo()
    {
        myAnimator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return new WaitForSeconds(.2f);
        myAnimator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;

    }

    void UpdateAnimation()
    {
        if (change != Vector3.zero)
        {
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
            myAnimator.SetFloat("moveX", change.x);
            myAnimator.SetFloat("moveY", change.y);
            myAnimator.SetBool("moving", true);
        }
        else
        {
            myAnimator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        changeFix.Normalize();
        myRigidbody.MovePosition(
            transform.position + changeFix * speed * Time.deltaTime
            );
    }

    public void hurt(float damage)
    {
        currentHealth.RuntimeValue -= damage;
        
        if (currentHealth.RuntimeValue >= 0)
        {
            playerHealthSignal.Raise();
        }

        if (currentHealth.RuntimeValue <= 0)
        {
            //this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }

    public void heal(float healAmount)
    {
        currentHealth.RuntimeValue += healAmount;
        if (currentHealth.RuntimeValue >= 0 && currentHealth.RuntimeValue <= currentHealth.MaxValue)
        {
            playerHealthSignal.Raise();
        }else if (currentHealth.RuntimeValue > currentHealth.MaxValue)
        {
            currentHealth.RuntimeValue = currentHealth.MaxValue;
            playerHealthSignal.Raise();
        }
    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.walk;
        }
    }
}
