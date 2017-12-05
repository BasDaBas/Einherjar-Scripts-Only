using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    public AudioClip[] levelMusicChangeArray;
    private AudioSource audioSource;

    void Awake()
    {
        
        Debug.Log("Dont destroy on load: " + name);
        DontDestroyOnLoad(gameObject);
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();


        QualitySettings.masterTextureLimit = 0;//somehow the game sets it on 1 if you didn't go to the Option screen before starting the game.
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip thisLevelMusic = levelMusicChangeArray[scene.buildIndex];
        Debug.Log("Playing clip: " + thisLevelMusic);

        if (thisLevelMusic) //if there is music attached
        {
            audioSource.clip = thisLevelMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
	}

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }    
}
