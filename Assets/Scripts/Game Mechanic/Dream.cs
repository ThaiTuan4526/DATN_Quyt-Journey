using UnityEngine;

public class Dream : MonoBehaviour
{
	public int dream;

	private GameObject dreamObj;

	private GameObject lightObj;

	private void Start()
	{
		dreamObj = base.transform.GetChild(0).gameObject;
		lightObj = Object.Instantiate(Resources.Load("Prefabs/GameObj/DreamLight"), base.transform) as GameObject;
		SetMode(n: false);
	}

	public void SetMode(bool n)
	{
		lightObj.SetActive(n);
		if (n)
		{
			dreamObj.layer = 18;
		}
		else
		{
			dreamObj.layer = 17;
		}
	}
}
