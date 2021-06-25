using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemTriBeamMove : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gameManager = null;
    [SerializeField] private GameObject EnemBeamTriBulletPrefab = null;
    private Animator animator = null;
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Beam());
    }

    // Update is called once per frame
    void Update()
    {
        
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
        
        for(int i = 0; i < 3 / 0.05; i++)
        {        
            float posX = transform.localPosition.x;
            float posY = transform.localPosition.y+1;

            GameObject BeamTriBullet;

            BeamTriBullet = Instantiate(EnemBeamTriBulletPrefab, 
                new Vector2(posX, posY), Quaternion.identity);
            
            yield return new WaitForSeconds(0.05f);

        }
    }
    private void BeamEnd()
    {
        animator.Play("EnemBeamTriEdAnim");
    }


}
