using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemTriMove : MonoBehaviour
{
    private Vector2 target1stPos = Vector2.zero;
    private Vector2 target2ndPos = Vector2.zero;
    private Vector2 targetPos = Vector2.zero;

    protected float life = 3;
    private bool isDamaged = false;
    private bool isDead = false;

    private float MoveToX = 1;
    private float MoveToY = 1;

    private bool CheckArrive = false;

    protected GameManager gameManager = null;
    protected Collider2D col = null;
    protected Animator animator = null;
    protected SpriteRenderer spriteRenderer = null;

    [SerializeField]
    private GameObject EnemBulletPrefab = null;
    protected virtual void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (gameManager.EnemTriSpawnLeft)
            MoveToX = 4;
        else
            MoveToX = -4;        
        
        target1stPos = new Vector2(transform.position.x + MoveToX, transform.position.y + MoveToY); MoveToX *= -1;
        target2ndPos = new Vector2(transform.position.x + MoveToX, transform.position.y + MoveToY);

        StartCoroutine(bulletPoolOrSpawn());
    }

    void Update()
    {        
        TriMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.CompareTag("playerbullet"))
        {
            collision.GetComponent<BulletMove>().Pooling();
            StartCoroutine(DeadCheck());
        }
    }

    protected  IEnumerator DeadCheck()
    {
        life--;
        if(life <= 0)
        {
            gameManager.AddScore(50);
            animator.Play("EnemExpAnim");
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
    }

    

    

    private void TriMove()
    {
        if (!CheckArrive)
            targetPos = target1stPos;
        else
            targetPos = target2ndPos;

        transform.localPosition = Vector2.MoveTowards(transform.localPosition, targetPos,
                gameManager.EnemTriSpeed * Time.deltaTime);
        if(transform.localPosition.x == target1stPos.x)
        {
            CheckArrive = true;
        }
        if(transform.localPosition.x == target2ndPos.x)
        {
            Destroy(gameObject);
        }
       // if( transform.position.x = )
    }

    private IEnumerator bulletPoolOrSpawn()
    {
        float fireRate = Random.Range(0.3f, 0.6f);
        
        yield return new WaitForSeconds(1f);
            
        GameObject Enembullet;
        if(gameManager.enemBulletPool.transform.childCount > 0)
        {
            Enembullet = 
                gameManager.enemBulletPool.transform.GetChild(0).gameObject;
            Enembullet.transform.position = transform.position;
                Enembullet.SetActive(true);
        }
         else
         {
          Enembullet = 
                 Instantiate(EnemBulletPrefab, transform.position, Quaternion.identity);
          }
                Enembullet.transform.SetParent(null);
                yield return new WaitForSeconds(fireRate);
            

    }


    
}
