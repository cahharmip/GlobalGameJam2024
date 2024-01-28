using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
  public Sound[] sounds;
  public static AudioManager instance;
  private bool setup;
  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Destroy(gameObject);
      return;
    }
    DontDestroyOnLoad(gameObject);

    foreach(Sound s in sounds)
    {
      s.source = gameObject.AddComponent<AudioSource>();
      s.source.clip = s.clip;

      s.source.volume = s.volumn;
      s.source.pitch = s.pitch;
      s.source.loop = s.loop;
    }
  }

  public void Play(string name)
  {
    Sound s = Array.Find(sounds, s => s.name == name);
    if (s == null)
    {
      Debug.LogWarning("Cannot find the sound:" + name);
      return;
    }
    s.source.Play();
  }

  public void Stop(string name)
  {
    AudioSource[] availabelSource = transform.GetComponents<AudioSource>();
    foreach(AudioSource source in availabelSource)
    {
      if (source.clip.name == name)
      {
        source.Stop();
      }
    }
  }
}
