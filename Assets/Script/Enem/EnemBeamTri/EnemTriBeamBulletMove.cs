using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemTriBeamBulletMove : MonoBehaviour
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
        if(gameManager.MinPos.y > transform.position.y)
        {
            BeamPooling();
        }
    }

    protected virtual void EnemBeamMove()
    {
        transform.Translate(Vector2.down * gameManager.EnemBeamTriBulletSpeed * Time.deltaTime);
    }

    private void BeamPooling()
    {
        transform.SetParent(gameManager.enemBeamPool.transform);
        gameObject.SetActive(false);
    }
}
