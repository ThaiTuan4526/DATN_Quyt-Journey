using UnityEngine;

public class Bro2An : MonoBehaviour
{
	public TakeBro takeBro;

	public void ShowWorm()
	{
		takeBro.worm.SetActive(value: true);
	}

	public void Puke()
	{
		Singleton<Sounds>.use.So("puke");
	}
}
