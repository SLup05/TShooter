using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemTriBeamMove : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gameManager = null;
    [SerializeField] private GameObject EnemBeamTriBulletPrefab = null;
    private Animator animator = null;
    private Collider2D col = null;
    private float BeamTriLife = 5;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        StartCoroutine(Beam());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("playerbullet"))
        {
            collision.GetComponent<BulletMove>().Pooling();
            StartCoroutine(DeadCheck());
        }
    }

    private IEnumerator DeadCheck()
    {
        BeamTriLife--;

        if(BeamTriLife <= 0)
        {
            gameManager.AddScore(500);
            col.enabled = false;
            animator.Play("EnemExpAnim");
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
    }

    private IEnumerator Beam()
    {
        yield return new WaitForSeconds(1f);
        animator.Play("EnemBeamTriOpAnim");

        yield return new WaitForSeconds(1f);
        StartCoroutine(BeamFire());

        Invoke("BeamEnd", 3f);
        yield return new WaitForSeconds(4f);

        animator.Play("EnemBeamTri2ndAnim");
        yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        
        yield return null;

    }
    private IEnumerator BeamFire()
    {
        float posX = transform.position.x;
        float posY = transform.position.y + 1;
        for(int i = 0; i < 3 / 0.05; i++)
        {        
            GameObject BeamTriBullet;
            
            if(gameManager.enemBeamPool.transform.childCount > 0)
            {
                BeamTriBullet =
                    gameManager.enemBeamPool.transform.GetChild(0).gameObject;
                BeamTriBullet.transform.position
                    = new Vector2(posX, posY);
                BeamTriBullet.SetActive(true);
            }
            else
            {
            BeamTriBullet = Instantiate(EnemBeamTriBulletPrefab,
                new Vector2(posX, posY), Quaternion.identity);
            }
            BeamTriBullet.transform.SetParent(null);

            
            yield return new WaitForSeconds(0.05f);

        }
    }
    private void BeamEnd()
    {
        animator.Play("EnemBeamTriEdAnim");
    }


}
