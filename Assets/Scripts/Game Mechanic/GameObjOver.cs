using UnityEngine;

public class GameObjOver : MonoBehaviour, Trigger2DInterface
{
	private bool haveLink;

	private GameObjInterface objIn;

	private GameObjInterfaceOver objOverIn;

	private bool isActive;

	public bool SetActive
	{
		set
		{
			isActive = value;
		}
	}

	private void Start()
	{
		objIn = base.gameObject.GetComponent<GameObjInterface>();
		objOverIn = base.gameObject.GetComponent<GameObjInterfaceOver>();
		isActive = true;
	}

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Hero"))
		{
			if (!haveLink && isActive)
			{
				Main.hero.SetLink(objIn, n: true);
				haveLink = true;
			}
			if (isActive)
			{
				objOverIn.OverObj(n: true);
			}
		}
	}

	private void OnTriggerExit2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Hero"))
		{
			if (haveLink)
			{
				Main.hero.SetLink(objIn, n: false);
				haveLink = false;
			}
			objOverIn.OverObj(n: false);
		}
	}
}
