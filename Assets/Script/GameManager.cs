using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public float EnemTriBulletSpeed { get; set; }

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
    public int Score = 0;
    public int HighScore = 0;

    [SerializeField] private Text PlayerLifeText = null;
    [SerializeField] private Text ScoreText = null;
    [SerializeField] private Text HighScoreText = null;

    public PoolManager poolManager = null;
    public EnemLeftLinePool enemLeftLinePool = null;
    public EnemRightLinePool enemRightLinePool = null;
    public EnemBulletPool enemBulletPool = null;
    public EnemBeamPool enemBeamPool = null;

    private BackGroundMove backGroundMove = null;

    private PlayerMove playerMove = null;
    private Animator animator = null;

    void Start()
    {
        backGroundMove = FindObjectOfType<BackGroundMove>();

        MaxPos = new Vector2(3f, 7f);
        MinPos = new Vector2(-3f, -7f);


        PlMaxPos = new Vector2(2.5f, -2.2f);
        PlMinPos = new Vector2(-2.5f, -2.2f);

        PlSpeed = 1.2f;
        PlNormalSpeed = 1.7f;
        PlDodgeSpeed = 4;
        BulletSpeed = 20;
        EnemTriSpeed = 2.5f;
        EnemLineTriSpeed = 1f;
        EnemLineTriBulletSpeed = 2f;
        EnemBeamTriSpeed = 1f;
        EnemBeamTriBulletSpeed = 15f;
        EnemTriBulletSpeed = 2f;

        EnemTriSpawnLeft = true;

        poolManager = FindObjectOfType<PoolManager>();        
        enemLeftLinePool = FindObjectOfType<EnemLeftLinePool>();
        enemRightLinePool = FindObjectOfType<EnemRightLinePool>();
        enemBulletPool = FindObjectOfType<EnemBulletPool>();
        enemBeamPool = FindObjectOfType<EnemBeamPool>();
        playerMove = FindObjectOfType<PlayerMove>();
        animator = GetComponent<Animator>();

        Score = 0;

        StartCoroutine(SpawnEnemTri());
        StartCoroutine(SpawnEnemLineTri());
        StartCoroutine(SpawnEnemBeamTri());

    }

    public void AddScore(int addScore)
    {
        Score += addScore;
        PlayerPrefs.SetInt("SCORE", Score);
        if(Score > HighScore)
        {
            HighScore = Score;
            PlayerPrefs.SetInt("HIGHSCORE", HighScore);
        }
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public IEnumerator PlayerDeadCheck()
    {
        PlayerLife--;
        if(PlayerLife <= 0)
        {
            playerMove.Explosion();
            yield return new WaitForSeconds(0.7f);
            SceneManager.LoadScene("GameOver");
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
        while(true)
        {
        GameObject EnemTri;

        float posX;
        float posY;
        

        if (EnemTriSpawnLeft)
            posX = -4;
        else
            posX = 4;

        posY = Random.Range(0f, 2f);

        for (int i = 0; i < 3; i++)
        {
            
            Instantiate(EnemTriPrefab, new Vector2(posX, posY), Quaternion.identity);

            yield return new WaitForSeconds(0.5f);
        }

        if (EnemTriSpawnLeft)
            EnemTriSpawnLeft = false;
        else
            EnemTriSpawnLeft = true;

            yield return new WaitForSeconds(0.5f);

        }
        
    }

    private IEnumerator SpawnEnemLineTri()
    {
        float posX;
        float posY;
        while(true)
        {
            GameObject EnemLineTri;

            for(int i = 0; i < 1; i++)
            {
                posX = Random.Range(-2.5f, 2.5f);
                posY = 7.5f;

                Instantiate(EnemLineTriPrefab, new Vector2(posX, posY), Quaternion.identity);

                yield return new WaitForSeconds(1.5f);

            }
            yield return new WaitForSeconds(3f);
        }

    }

    private IEnumerator SpawnEnemBeamTri()
    {
        while(true)
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
            yield return new WaitForSeconds(10f);
        }
    }
    }



