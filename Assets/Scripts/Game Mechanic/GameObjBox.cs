using System;
using UnityEngine;

public class GameObjBox : MonoBehaviour, Trigger2DInterface
{
	private bool haveLink;

	private GameObjInterface objIn;

	private GameObjInterfaceOver objOverIn;

	internal bool isActive;

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
				Main.hero.actionM = null;
				Hero hero = Main.hero;
				hero.actionM = (Hero.SetMethodAction)Delegate.Combine(hero.actionM, new Hero.SetMethodAction(Main.hero.EmptyFunction));
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
			if (haveLink && !Main.hero.catchMode)
			{
				Main.hero.SetLink(objIn, n: false);
				haveLink = false;
			}
			objOverIn.OverObj(n: false);
		}
	}
}
