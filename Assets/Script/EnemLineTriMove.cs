using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemLineTriMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject EnemLineTriLeftBulletPrefab = null;
    [SerializeField] GameObject EnemLineTriRightBulletPrefab = null;
    private GameManager gameManager = null;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(EnemLineTriFire());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * gameManager.EnemLineTriSpeed * Time.deltaTime);
    }
    private IEnumerator EnemLineTriFire()
    {
        while(true)
        {
            GameObject EnemLineTriBullet;

            float posX = transform.localPosition.x;
            float posY = transform.localPosition.y;

            EnemLineTriBullet = Instantiate(EnemLineTriLeftBulletPrefab, new Vector2(posX, posY), Quaternion.identity);
            EnemLineTriBullet = Instantiate(EnemLineTriRightBulletPrefab, new Vector2(posX, posY), Quaternion.identity);

            yield return new WaitForSeconds(0.2f);
        }
    }
}
