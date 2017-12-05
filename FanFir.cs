using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanFir : MonoBehaviour
{
    public BossFight bossFightScript;
    public GameObject[] doors;
    public GameObject boss;

    public GameObject fadeInImage;

    public AudioClip transformationClip;
    private AudioSource source;

    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            Destroy(other.gameObject);            
            DoorsOpen(true);
            TurnOffPlayer(false);
            StartCoroutine(Cinematic());

        }
    }

    IEnumerator Cinematic()
    {
        
        fadeInImage.SetActive(true);
        source.PlayOneShot(transformationClip, 0.8f);
        yield return new WaitForSeconds(transformationClip.length - 3f);

        boss.SetActive(true);

        Destroy(gameObject);
    }

    public void DoorsOpen(bool doorOpen)
    {
        doors[0].SetActive(doorOpen);
        doors[1].SetActive(doorOpen);
    }

    void TurnOffPlayer(bool playerActive)
    {
        player.GetComponent<PlatformController>().enabled = playerActive;
        player.GetComponent<Animator>().enabled = playerActive;        
        player.GetComponent<WeaponBehaviour>().enabled = playerActive;
    }
}
