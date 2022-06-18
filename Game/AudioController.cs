using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance {get; private set;}

    [SerializeField]
    AudioSource music_source;
    [SerializeField]
    AudioSource sound_source;

    void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic(AudioClip p_clip) {
        music_source.clip = p_clip;
        music_source.Play();
    }

    public void PlaySound(AudioClip p_clip) {
        sound_source.PlayOneShot(p_clip);
    }
}
