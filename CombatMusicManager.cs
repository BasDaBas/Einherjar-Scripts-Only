using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class CombatMusicManager : MonoBehaviour {

    public AudioMixerSnapshot outOfCombat;
    public AudioMixerSnapshot inCombat;
    public AudioClip[] stings;
    public AudioSource stingSource;
    public float bpm = 128;


    private float m_TransitionIn;
    private float m_TransitionOut;
    private float m_QuarterNote;

    // Use this for initialization
    void Start()
    {
        m_QuarterNote = 60 / bpm;
        m_TransitionIn = m_QuarterNote;
        m_TransitionOut = m_QuarterNote * 16;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CombatZone"))
        {
            Debug.Log("Enemy Combat music");
            inCombat.TransitionTo(m_TransitionIn);
            PlaySting();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("CombatZone"))
        {
            Debug.Log("Enemy out Combat music");
            outOfCombat.TransitionTo(m_TransitionOut);
        }
    }

    void PlaySting()
    {
        int randClip = Random.Range(0, stings.Length);
        stingSource.clip = stings[randClip];
        stingSource.Play();
    }


}
