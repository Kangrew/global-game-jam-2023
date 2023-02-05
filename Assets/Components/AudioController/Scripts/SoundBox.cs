using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBox : Singleton<SoundBox>
{
    public SoundContainer countainer;
    public AudioSource source;
    public AudioSource music;
    
    private void Start(){
        
    }
    
    public void PlaySound(AudioClip clip, Vector3 pos){
        source.PlayOneShot(clip,1);

    }
    void musicvolume(){
        music.volume = 1f;
    }
}
