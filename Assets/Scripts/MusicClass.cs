using UnityEngine;
 
public class MusicClass : MonoBehaviour
{
    private AudioSource audio;
    
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        audio = GetComponent<AudioSource>();
    }
 
    public void PlayMusic()
    {
        if (audio.isPlaying) 
            return;
        audio.Play();
    }
 
    public void StopMusic()
    {
        audio.Stop();
    }
}