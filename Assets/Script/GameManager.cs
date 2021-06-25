using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float PlSpeed { get; set; }
    public float PlNormalSpeed { get; set; }
    public float PlDodgeSpeed { get; set; }
    public float EnemTriSpeed { get; set; }
    public float EnemLineTriSpeed { get; set; }
    public float EnemLineTriBulletSpeed { get; set; }
    public float EnemBeamTriSpeed { get; set; }
    public float EnemBeamTriBulletSpeed { get; set; }

    public Vector2 MaxPos { get; private set; }
    public Vector2 MinPos { get; private set; }

    public Vector2 PlMaxPos { get; set; }
    public Vector2 PlMinPos { get; set; }
    public float BulletSpeed { get; set; }

    public bool EnemTriSpawnLeft { get; set; }
    [SerializeField] private GameObject EnemTriPrefab = null;
    [SerializeField] private GameObject EnemLineTriPrefab = null;
    [SerializeField] private GameObject EnemBeamTriPrefab = null;

    private int PlayerLife = 3;
    private int Score = 0;
    private int HighScore = 0;

    [SerializeField] private Text PlayerLifeText = null;
    [SerializeField] private Text ScoreText = null;
    [SerializeField] private Text HighScoreText = null;

    private BackGroundMove backGroundMove = null;

    void Start()
    {
        backGroundMove = FindObjectOfType<BackGroundMove>();

        MaxPos = new Vector2(2f, 7f);
        MinPos = new Vector2(-2f, -7f);


        PlMaxPos = new Vector2(2.5f, -2.2f);
        PlMinPos = new Vector2(2.5f, -2.2f);

        PlSpeed = 1.2f;
        PlNormalSpeed = 1.5f;
        PlDodgeSpeed = 3;
        BulletSpeed = 20;
        EnemTriSpeed = 2.5f;
        EnemLineTriSpeed = 1f;
        EnemLineTriBulletSpeed = 2f;
        EnemBeamTriSpeed = 1f;
        EnemBeamTriBulletSpeed = 17f;

        EnemTriSpawnLeft = true;
        StartCoroutine(Infinity());
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public void PlayerDeadCheck()
    {
        PlayerLife--;
        if(PlayerLife <= 0)
        {

        }
        UpdateUI();
        
    }

    public void UpdateUI()
    {
        PlayerLifeText.text = string.Format("LIFE {0}", PlayerLife);
        ScoreText.text = string.Format("{0}", Score);

    }

    private IEnumerator SpawnEnemTri()
    {
        
        GameObject EnemTri;

        float posX;
        float posY;
        

        if (EnemTriSpawnLeft)
            posX = -4;
        else
            posX = 4;

        posY = Random.Range(-1f, 2f);

        for (int i = 0; i < 3; i++)
        {
            
            Instantiate(EnemTriPrefab, new Vector2(posX, posY), Quaternion.identity);

            yield return new WaitForSeconds(0.5f);
        }

        if (EnemTriSpawnLeft)
            EnemTriSpawnLeft = false;
        else
            EnemTriSpawnLeft = true;      
        
    }

    private IEnumerator SpawnEnemLineTri()
    {
        
        GameObject EnemLineTri;

        float posX;
        float posY;
        

        posX = Random.Range(-2.5f, 2.5f);
        posY = 7.5f;

        Instantiate(EnemLineTriPrefab, new Vector2(posX, posY), Quaternion.identity);

        yield return new WaitForSeconds(1.5f);

    }

    private IEnumerator SpawnEnemBeamTri()
    {
        float posX = Random.Range(-1.5f, -1);
        float posY = 4.5f;
        for(int i = 0; i < 2; i++)
        {
            GameObject EnemBeamTri;
            EnemBeamTri = Instantiate(EnemBeamTriPrefab, new Vector2(posX, posY), Quaternion.identity);
            posX += 2.5f;
            yield return new WaitForSeconds(0.5f);
        }        
    }

    private IEnumerator Infinity()
    {
        while(true)
        {
            StartCoroutine(SpawnEnemTri());
            StartCoroutine(SpawnEnemTri());

            for(int i = 0; i < 5; i++)

            { 
             StartCoroutine(SpawnEnemLineTri());
            }

            for(int i = 0; i < 5; i++)
            {
                StartCoroutine(SpawnEnemBeamTri());
                StartCoroutine(SpawnEnemTri());
                yield return new WaitForSeconds(1f);
            }

        }
    }

    /*private IEnumerator StageOne()
    {
        StartCoroutine(SpawnEnemTri());
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(SpawnEnemTri());
        yield return new WaitForSeconds(0.25f);

        StartCoroutine(SpawnEnemTri());
        StartCoroutine(SpawnEnemTri());
        yield return new WaitForSeconds(1f);
        StartCoroutine(SpawnEnemTri());
        StartCoroutine(SpawnEnemTri());
        yield return new WaitForSeconds(1f);
        StartCoroutine(SpawnEnemTri());
        StartCoroutine(SpawnEnemTri());
        yield return new WaitForSeconds(1f);
        StartCoroutine(SpawnEnemTri());
        StartCoroutine(SpawnEnemTri());

        //HARD
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < 16; i++)
        {
            StartCoroutine(SpawnEnemTri());
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(2f);
        
        yield return new WaitForSeconds(5f);
        for(int i = 0; i < 3; i++)
        {
            StartCoroutine(SpawnEnemLineTri());
            yield return new WaitForSeconds(1f);
            StartCoroutine(SpawnEnemTri());
            yield return new WaitForSeconds(1f);
        }

        //HARD
        yield return new WaitForSeconds(5f);
        for (int i = 10; i < 10; i++)
        {
            StartCoroutine(SpawnEnemLineTri());
            yield return new WaitForSeconds(1.3f);
            StartCoroutine(SpawnEnemTri());
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(SpawnEnemTri());
        }
        yield return new WaitForSeconds(2f);

        for(int i = 0; i < 3; i++)
        {
            StartCoroutine(SpawnEnemBeamTri());
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(SpawnEnemTri());
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(SpawnEnemTri());
            yield return new WaitForSeconds(0.5f);
        }

        //HARD
        yield return new WaitForSeconds(5f);
        for(int i = 0; i < 5; i++)
        {
            StartCoroutine(SpawnEnemBeamTri());
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(SpawnEnemLineTri());
            yield return new WaitForSeconds(1.2f);
        }

        while(true)
        {
            for (int i = 0; i < 16; i++)
            {
                StartCoroutine(SpawnEnemTri());
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(2f);

            for (int i = 10; i < 10; i++)
            {
                StartCoroutine(SpawnEnemLineTri());
                yield return new WaitForSeconds(1.3f);
                StartCoroutine(SpawnEnemTri());
                yield return new WaitForSeconds(0.2f);
                StartCoroutine(SpawnEnemTri());
            }
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < 5; i++)
            {
                StartCoroutine(SpawnEnemBeamTri());
                yield return new WaitForSeconds(0.2f);
                StartCoroutine(SpawnEnemLineTri());
                yield return new WaitForSeconds(1.2f);
            }
            yield return new WaitForSeconds(2f);


        }*/

    }



