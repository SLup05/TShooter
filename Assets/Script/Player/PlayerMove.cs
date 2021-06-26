using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update

    private GameManager gameManager = null;
    private Animator animator = null;
    private Collider2D col = null;
    private SpriteRenderer spriteRenderer = null;

    private bool CanMove = true;

    private bool CanMoveLeft = true;
    private bool CanMoveRight = true;


    public bool MoveLeft = false;
    public bool MoveRight = false; 
    
    public bool isDodge = false;
    private float DodgeDelay = 0.8f;
    

    [SerializeField]
    private GameObject bulletPrefab = null;
    [SerializeField] 
    private Transform bulletPosition = null;

    private float fireRate = 0f;
    public bool DoFire = false;
    private bool IfFire = false;

    private bool isDamaged = false;
    


    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        fireRate = 0.1f;
        IfFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.PlMinPos.x > transform.localPosition.x)
        {
            CanMoveLeft = false;
        }
        else
        {
            CanMoveLeft = true;
        }

        if (gameManager.PlMaxPos.x < transform.localPosition.x)
        {
            CanMoveRight = false;
        }
        else
        {
            CanMoveRight = true;
        }
        if (MoveLeft && CanMoveLeft)
        {
            transform.Translate(Vector2.left*gameManager.PlSpeed*Time.deltaTime);
        }
        if(MoveRight && CanMoveRight)
        {
            transform.Translate(Vector2.right*gameManager.PlSpeed*Time.deltaTime);
        }
        if(isDodge && CanMove)
        {
            Dodge();
        }
        if(DoFire && IfFire && CanMove)
        {
            IfFire = false;
            Fire();
            Invoke("Reload", fireRate);
        }
        
                
    }

    //private IEnumerator TFFire()
    //{
        
        
    //    yield return new WaitForSeconds(fireRate);

    //}

    private void Fire()
    {
        PoolOrSpawn();
    }

    private void PoolOrSpawn()
    {
        GameObject bullet;
        if(gameManager.poolManager.transform.childCount > 0)
        {
            bullet = gameManager.poolManager.transform.GetChild(0).gameObject;
            bullet.transform.position = bulletPosition.position;
            bullet.SetActive(true);
        }
        else
        {
            bullet = Instantiate(bulletPrefab, bulletPosition.position, Quaternion.identity);            
        }
        bullet.transform.SetParent(null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDamaged) return;
        if (isDodge) return;
        if(collision.CompareTag("Enemy"))
        {
        isDamaged = true;
        StartCoroutine(Damage());
        }
    }

    private IEnumerator Damage()
    {
        StartCoroutine(gameManager.PlayerDeadCheck());
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        isDamaged = false;
    }


    private void Dodge()
    {
        animator.Play("PlayerDodgeAnim");

        CanMove = false;
        gameManager.PlSpeed = gameManager.PlDodgeSpeed;
        Invoke("DodgeOut", DodgeDelay);
        
    }

    private void DodgeOut()
    {
        gameManager.PlSpeed = gameManager.PlNormalSpeed;
        animator.Play("SpaceShipAnim");

        CanMove = true;
    }

    private void Reload()
    {
        IfFire = true;
    }
    public void Explosion()
    {
        animator.Play("PlayerExpAnim");
    }
}
