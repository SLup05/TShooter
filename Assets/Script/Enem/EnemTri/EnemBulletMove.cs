using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemBulletMove : MonoBehaviour
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
        transform.Translate(Vector2.down * gameManager.EnemTriBulletSpeed * Time.deltaTime);
        if (transform.localPosition.y < gameManager.MinPos.y)
        {
            BulletPooling();
        }


    }
    private void BulletPooling()
    {
        transform.SetParent(gameManager.enemBulletPool.transform);
        gameObject.SetActive(false);
    }
}
