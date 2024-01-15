using UnityEngine;

public class AnMonster : MonoBehaviour
{
	public void JumpSo()
	{
		Singleton<Sounds>.use.So2("jumpBone", 0.7f);
	}

	public void UpSo()
	{
		Singleton<Sounds>.use.So("toUp");
	}
}
