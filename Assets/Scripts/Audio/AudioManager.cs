using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] soundList;
    private static Dictionary<string, AudioSource> sources = new Dictionary<string, AudioSource>();

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        foreach (Sound s in soundList)
        {
            if (sources.ContainsKey(s.soundName)) continue;

            AudioSource source = s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            sources.Add(s.soundName, source);
        }
    }

    // Update is called once per frame
    public void Play(string name)
    {
        sources[name].Play();
    }
}
