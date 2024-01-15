using System.Collections.Generic;
using UnityEngine;

public class Music : Singleton<Music>
{
	public AudioSource musicSource;

	private AudioClip soAudio;

	private Dictionary<string, AudioClip> soArr = new Dictionary<string, AudioClip>();

	private Dictionary<string, float> soPos = new Dictionary<string, float>();

	private int i;

	private float volumeTarget;

	private string currentMusic = "";

	private float volumeSlow;

	public float maxVolume;

	private bool mute;

	private float speedSlowUp;

	public void Init()
	{
		SetUse(this);
		base.gameObject.name = "[MUSIC]";
		musicSource = GetComponent<AudioSource>();
		maxVolume = Singleton<Saves>.use.saveF("volumeMusic");
		MakeSound("forest");
		MakeSound("shackMusic");
		MakeSound("forestCreepy");
		MakeSound("demoEnd");
		MakeSound("houseBones");
		MakeSound("treeHouseMusic");
		MakeSound("caveMusic");
		MakeSound("finalMusic");
	}

	private void MakeSound(string n)
	{
		soAudio = Resources.Load("Sounds/music/" + n) as AudioClip;
		soArr.Add(n, soAudio);
		soPos.Add(n, 0f);
	}

	public void MusicStop()
	{
		musicSource.Stop();
		currentMusic = "";
	}

	private void RemoveMusic()
	{
		if (currentMusic != "")
		{
			soPos[currentMusic] = musicSource.time;
			musicSource.Stop();
		}
	}

	public void MusicStart(string s)
	{
		if (!mute && !(currentMusic == s))
		{
			RemoveMusic();
			currentMusic = s;
			musicSource.clip = soArr[s];
			musicSource.time = 0f;
			musicSource.Play();
			musicSource.time = soPos[s];
			musicSource.loop = true;
			musicSource.volume = maxVolume;
		}
	}
}
