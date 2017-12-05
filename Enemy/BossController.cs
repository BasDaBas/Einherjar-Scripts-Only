using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    public enum BossActionType
    {
        Idle,
        Patrolling,        
        Moving,
        Attacking
    }

    public Transform target;
    public Transform target2;
    public float bossSpeed = 0.1f;

    //Facing
    public GameObject enemyGraphic;
    public bool canFlip = true;
    private bool facingRight = false;

    [HideInInspector]
    public bool startBossFight = false;         //Bool that checks if the boss is hit by a bullet which means the fight start need to be public so It accesable from an other script.

    //private BossActionType eCurrentState;

    // Use this for initialization
    void Start ()
    {
        //StartCoroutine(moveRight());

    }

    // Update is called once per frame
    void Update ()
    {

        
    }
   


    IEnumerator moveRight()
    {

        Debug.Log("IEnumerator moveRight");
        yield return new WaitForSeconds(1);


        while (Vector3.Distance(transform.position, target.position) > 0.5f)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, bossSpeed * Time.deltaTime);
            yield return 0;
        }
        flipFacing();
        StartCoroutine(moveLeft());
    }

    IEnumerator moveLeft()
    {
        Debug.Log("IEnumerator moveLeft");
        yield return new WaitForSeconds(1);


        while (Vector3.Distance(transform.position, target2.position) > 0.5f)
        {
            transform.position = Vector3.Lerp(transform.position, target2.position, bossSpeed * Time.deltaTime);
            yield return 0;
        }
        flipFacing();

        StartCoroutine(moveRight());
    }

    void flipFacing()
    {
        if (!canFlip)
        {
            return;
        }

        float facingX = enemyGraphic.transform.localScale.x;
        facingX *= -1f;
        enemyGraphic.transform.localScale = new Vector3(facingX, enemyGraphic.transform.localScale.y, enemyGraphic.transform.localScale.z);
        facingRight = !facingRight;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
        }
    }
}
