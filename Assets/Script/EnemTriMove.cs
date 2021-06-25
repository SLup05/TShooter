using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemTriMove : MonoBehaviour
{
    private Vector2 target1stPos = Vector2.zero;
    private Vector2 target2ndPos = Vector2.zero;
    private Vector2 targetPos = Vector2.zero;

    private float MoveToX = 1;
    private float MoveToY = 1;

    private bool CheckArrive = false;

    private GameManager gameManager = null;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager.EnemTriSpawnLeft)
            MoveToX = 3;
        else
            MoveToX = -3;
        
        
        target1stPos = new Vector2(transform.position.x + MoveToX, transform.position.y + MoveToY); MoveToX *= -1;
        target2ndPos = new Vector2(transform.position.x + MoveToX, transform.position.y + MoveToY);
        
    }

    void Update()
    {
        
        TriMove();
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
       // if( transform.position.x = )
    }
    
}
