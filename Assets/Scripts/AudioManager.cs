using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
  public Sound[] sounds;
  public static AudioManager instance;
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
    if (s==null)
    {
      Debug.LogWarning("Cannot find the sound:" + name);
      return;
    }
    s.source.Play();
  }
}
