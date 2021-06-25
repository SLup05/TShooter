using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private GameManager gameManager = null;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * gameManager.BulletSpeed * Time.deltaTime);
        if(transform.localPosition.y > gameManager.MaxPos.y)
        {
            Destroy(gameObject);
        }
    }
}
