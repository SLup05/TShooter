using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemLineTriRightBulletMove : EnemTriBeamBulletMove
{
    // Start is called before the first frame update
    private GameManager gameManager = null;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemBeamMove();
    }

    protected override void EnemBeamMove()
    {
        transform.Translate(Vector2.right * gameManager.EnemLineTriBulletSpeed * Time.deltaTime);
        transform.Translate(Vector2.down * gameManager.EnemLineTriSpeed * Time.deltaTime);
        if (transform.localPosition.x > gameManager.MaxPos.x)
        {
            Destroy(gameObject);
        }

    }
}
