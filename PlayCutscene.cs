using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))]

public class PlayCutscene : MonoBehaviour {

	public MovieTexture cutscene;
	private AudioSource source;

    private GameObject player;

	// Gather all the resources; video and audio.
	// Plays on awake.
	void Start () {

        player = GameObject.Find("Player 1(Clone)");

        GetComponent<RawImage> ().texture = cutscene as MovieTexture;
		source = GetComponent<AudioSource> ();
		source.clip = cutscene.audioClip;

        StartCoroutine(playingCutScene());
		
	}

	// Replace this with an actual trigger in-game.
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) && cutscene.isPlaying) {
			cutscene.Pause ();
            
		} else if (Input.GetKeyDown (KeyCode.Space) && !cutscene.isPlaying) {
			cutscene.Play ();
		}
	}

    IEnumerator playingCutScene()
    {
        cutscene.Play();
        source.Play();

        yield return new WaitForSeconds(source.clip.length);
        Debug.Log("Clip Done");
        
    }


}