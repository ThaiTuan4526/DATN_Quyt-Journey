using UnityEngine;

public class CabOpen : ActiveObj, InterfaceBreakObj
{
	public Curtain curtain;

	public GameObject jar;

	public bool isOpen;

	public GameObject c1;

	public GameObject c2;

	public BoxCollider2D jarCollider2D;

	private void Start()
	{
		Init();
		c2.SetActive(value: false);
		c1.SetActive(value: true);
		Main.game.arrBreakObj.Add(this);
		jarCollider2D.enabled = false;
	}

	public void BreakObj()
	{
		isOpen = false;
		c1.SetActive(!isOpen);
		c2.SetActive(isOpen);
		jarCollider2D.enabled = isOpen;
		GetComponent<BoxCollider2D>().enabled = !isOpen;
	}

	public override void DoAction()
	{
		if (curtain.isOpen && (!jar.activeSelf || !isOpen))
		{
			Invoke("DoActionOnPos", 0.1f);
			Main.hero.stopActions = true;
			Main.hero.SetDirect(n: true);
		}
	}

	public override void DoActionOnPos()
	{
		if (Main.game.inv.HaveItem(Items.keycab))
		{
			Main.hero.ReturnToGame();
			isOpen = !isOpen;
			c1.SetActive(!isOpen);
			c2.SetActive(isOpen);
			jarCollider2D.enabled = isOpen;
			GetComponent<BoxCollider2D>().enabled = !isOpen;
			Singleton<Sounds>.use.So("openCab");
		}
		else
		{
			Main.hero.ReturnToGame();
			Singleton<Sounds>.use.So("lock");
		}
	}
}
