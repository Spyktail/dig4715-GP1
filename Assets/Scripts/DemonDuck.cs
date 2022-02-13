using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonDuck : MonoBehaviour
{
    public float speed;
    public float minXval;
    public float maxXval;
    public float maxYval;
    public float minYval;
    Vector3 target;

    public float timeActive;
    Animator anim;
    SpriteRenderer spriteRend;
    Rigidbody2D rb;
    bool isDead;
    bool startedFalling;
        

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        
    }

    /* void OnColliderStay2D(Collider2D collider2D)
    {
        HunterController controller = GetComponent<HunterController>();
        if (controller != null)
        {
            Debug.Log("shoot");
        }
        //GameObject.FindGameObjectsWithTag("Duck");
    } */

    // Update is called once per frame
    void Update()
    {


        if (!isDead)
        {
            if (Vector3.Distance(transform.position, target) > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
            else
            {
                if (transform.position.x > maxXval || transform.position.x < minXval || transform.position.y > maxYval || transform.position.y < minYval)
                {
                    Destroy(gameObject);
                }

                SetTarget();

            }

            if (timeActive > 0)
            {
                timeActive -= Time.deltaTime;
                
            }

            UpdateSprite();
        }
        else
        {
            if (startedFalling)
                {
                    transform.position = Vector3.MoveTowards(transform.position, transform.position - transform.up, 10 * Time.deltaTime);
                }
        }
        
    }

    void FixedUpdate()
    {
        
    }

     public void Timeup()
    {
        speed *= 2;
        target = transform.position + new Vector3(0, 20, 0);
    }

    private void OnMouseDown()
    {
        if (!isDead)
            StartCoroutine(Dead());
    }

    /* private void OnFire()
    {
        if (!isDead)
            StartCoroutine(Dead());
    } */

    IEnumerator Dead()
    {
        HellHunter.Instance.HitDuck();
        isDead = true;
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(0.4f);
        startedFalling = true;
        Destroy(gameObject, 3f);
    }

    public void UpdateSprite()
        {
            if (transform.position.x - target.x > 0)
            {
                transform.localScale = new Vector3(-0.25f, 0.25f, 0.25f);
            }
            else
            {
                transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            }

            if (Mathf.Abs(transform.position.x - target.x) < 1)
            {
                anim.SetInteger("Fly", 2);
            }
            else if (Mathf.Abs(transform.position.y - target.y) < 1)
            {
                anim.SetInteger("Fly", 0);
            }
            else
            {
                anim.SetInteger("Fly", 1);
            }
        }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SetTarget();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, target);
    }
    public void SetTarget()
    {
        
        if (timeActive > 0)
        {
            

            target = target + new Vector3(Random.Range(-12, 12), Random.Range(-12, 12), 0);
            if (target.x > 8)
            {
                target.x = 8;
            }
                
            if (target.x < -8)
            {
                target.x = -8;
            }
            if (target.y > 4)
            {
                target.y = 4;
            }

            if (target.y < -2)
            {
                target.y = -2;
            }
        }
    }

    /* void TimerReader()
    {
        timeText.text = timeActive.ToString();
    } */
}