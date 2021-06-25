using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private PlayerMove playerMove = null;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveLeftDown()
    {
        playerMove.MoveLeft = true;
    }

    public void MoveLeftup()
    {
        playerMove.MoveLeft = false;
    }

    public void MoveRightDown()
    {
        playerMove.MoveRight = true;
    }

    public void MoveRightUp()
    {
        playerMove.MoveRight = false;
    }

    public void StartFire()
    {
        playerMove.DoFire = true;
        
    }

    public void StopFire()
    {
        playerMove.DoFire = false;
    }

    public void DodgeStart()
    {
        playerMove.isDodge = true;
    }
    public void DodgeStop()
    {
        playerMove.isDodge = false;
    }

}
