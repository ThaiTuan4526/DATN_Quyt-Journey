using System.Collections.Generic;
using UnityEngine;

public class Sounds : Singleton<Sounds>
{
	public AudioSource soundSource;

	public float currentVolume;

	private List<AudioSource> audioSources = new List<AudioSource>();

	private AudioClip soAudio;

	private Dictionary<string, AudioClip> soArr = new Dictionary<string, AudioClip>();

	private Dictionary<string, float> soArrTimes = new Dictionary<string, float>();

	private int countOfSources;

	public void Init()
	{
		SetUse(this);
		base.gameObject.name = "[SFX]";
		soundSource = GetComponent<AudioSource>();
		AddSounds();
		soundSource.clip = soArr["gypno"];
		currentVolume = Singleton<Saves>.use.saveF("volumeSFX");
		soundSource.volume = currentVolume;
	}

	public void AddAudioObj(AudioSource s)
	{
		audioSources.Add(s);
		countOfSources = audioSources.Count;
	}

	public void RemoveAudioObj(AudioSource s)
	{
		for (int i = 0; i < countOfSources; i++)
		{
			if (audioSources[i] == s)
			{
				audioSources.RemoveAt(i);
				countOfSources--;
				break;
			}
		}
	}

	public void RemoveAllAudio()
	{
		audioSources.Clear();
		countOfSources = 0;
	}

	public void SetVolumeToAudioObj()
	{
		for (int i = 0; i < countOfSources; i++)
		{
			audioSources[i].volume = currentVolume;
		}
	}

	public void StopAllSounds()
	{
		soundSource.Stop();
		for (int i = 0; i < countOfSources; i++)
		{
			audioSources[i].Stop();
		}
	}

	private void AddAudio(string n)
	{
		AudioClip[] array = Resources.LoadAll<AudioClip>(n);
		foreach (AudioClip audioClip in array)
		{
			soArr.Add(audioClip.name, audioClip);
		}
	}

	private void AddSounds()
	{
		AddAudio("Sounds/sfx/main");
		AddAudio("Sounds/sfx/hero");
		for (int i = 0; i < 9; i++)
		{
			AddAudio("Sounds/sfx/lev" + i);
		}
		soArrTimes.Add("leafGo", 0f);
	}

	public void So(string s)
	{
		soundSource.PlayOneShot(soArr[s]);
	}

	public void So2(string s, float n)
	{
		soundSource.PlayOneShot(soArr[s], n);
	}

	public void StopS()
	{
		soundSource.Stop();
	}

	public AudioClip GetSound(string n)
	{
		return soArr[n];
	}

	public void CanTimeSoundLong(string n)
	{
		if (Time.realtimeSinceStartup > soArrTimes[n])
		{
			soArrTimes[n] = Time.realtimeSinceStartup + 0.5f;
			So(n);
		}
	}

	public void CanTimeSoundMedium(string n)
	{
		if (Time.realtimeSinceStartup > soArrTimes[n])
		{
			soArrTimes[n] = Time.realtimeSinceStartup + 0.25f;
			So(n);
		}
	}

	public void CanTimeSoundFast(string n)
	{
		if (Time.realtimeSinceStartup > soArrTimes[n])
		{
			soArrTimes[n] = Time.realtimeSinceStartup + 0.1f;
			So(n);
		}
	}
}
