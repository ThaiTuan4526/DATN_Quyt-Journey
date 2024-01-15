using UnityEngine;

public class Curtain : ActiveObj, InterfaceBreakObj
{
	public bool isOpen;

	private bool actionDone;

	public GameObject c1;

	public GameObject c2;

	public CabOpen cab;

	public BoxCollider2D cupCollider2D;

	private void Start()
	{
		Init();
		Main.game.arrBreakObj.Add(this);
		cupCollider2D.enabled = false;
		c1.SetActive(!isOpen);
		c2.SetActive(isOpen);
	}

	public void BreakObj()
	{
	}

	private void SetBox()
	{
		c1.SetActive(!isOpen);
		c2.SetActive(isOpen);
		if (isOpen)
		{
			base.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-1.2f, -1f);
		}
		else
		{
			base.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0f, -1f);
		}
	}

	public override void DoAction()
	{
		if (!cab.isOpen && !isOpen)
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
		}
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(!isOpen);
		Main.hero.SetAn("Take2");
		actionDone = false;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			actionDone = true;
			cupCollider2D.enabled = true;
			GetComponent<BoxCollider2D>().enabled = false;
			isOpen = !isOpen;
			SetBox();
			Singleton<Sounds>.use.So("curtain");
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
			DeActivated();
		}
	}
}
