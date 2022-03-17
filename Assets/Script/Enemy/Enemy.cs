using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatVaule currentHealth;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public LootTable thisLoot;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
    }

    public void hurt(float damage)
    {
        TakeDamage(damage);
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = EnemyState.walk;
        }
    }

    private void TakeDamage(float damage)
    {
        currentHealth.RuntimeValue -= damage;
        if(currentHealth.RuntimeValue <= 0)
        {
            MakeLoot();
            currentHealth.RuntimeValue = currentHealth.initialValue;
            //this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }

    private void MakeLoot()
    {
        if (thisLoot != null)
        {
            PhysicalInventoryItem loot = thisLoot.LootGenerate();
            if(loot != null)
            {
                Instantiate(loot.gameObject, transform.position, Quaternion.identity);
            }
        }
    }
}
