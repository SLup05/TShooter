using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemLineTriMove : EnemTriMove
{
    // Start is called before the first frame update
    [SerializeField] GameObject EnemLineTriLeftBulletPrefab = null;
    [SerializeField] GameObject EnemLineTriRightBulletPrefab = null;

    private int LineTrilife = 4;
    private bool isDamaged = false;
    private bool isDead = false;

    protected override void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(LinePoolOrSpawn());
        life = 4;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * gameManager.EnemLineTriSpeed * Time.deltaTime);
        if (transform.localPosition.y < gameManager.MinPos.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if(collision.CompareTag("playerbullet"))
        {
            collision.GetComponent<BulletMove>().Pooling();
            StartCoroutine(DeadCheck());
        }
    } 

    private IEnumerator LinePoolOrSpawn()
    {
        while(true)
        {
            GameObject EnemLineTriLeftBullet;
            GameObject EnemLineTriRightBullet;

            float posX = transform.localPosition.x;
            float posY = transform.localPosition.y;

            if(gameManager.enemLeftLinePool.transform.childCount > 0)
            {
                EnemLineTriLeftBullet =
                    gameManager.enemLeftLinePool.transform.GetChild(0).gameObject;
                EnemLineTriLeftBullet.transform.position
                    = transform.position;
                EnemLineTriLeftBullet.SetActive(true);
            }
            else
            {
                EnemLineTriLeftBullet = 
                    Instantiate(EnemLineTriLeftBulletPrefab, new Vector2(posX, posY), Quaternion.identity);
                
            }
            EnemLineTriLeftBullet.transform.SetParent(null);

            if(gameManager.enemRightLinePool.transform.childCount > 0)
            {
                EnemLineTriRightBullet =
                    gameManager.enemRightLinePool.transform.GetChild(0).gameObject;
                EnemLineTriRightBullet.transform.position
                    = transform.position;
                EnemLineTriRightBullet.SetActive(true);
            }
            else
            {
                EnemLineTriRightBullet = 
                    Instantiate(EnemLineTriRightBulletPrefab, new Vector2(posX, posY), Quaternion.identity);
            }
            EnemLineTriRightBullet.transform.SetParent(null);


            yield return new WaitForSeconds(0.2f);
        }
    }

}