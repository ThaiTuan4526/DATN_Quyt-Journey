using UnityEngine;

public class Book : ActiveObj
{
	public GameObject book;

	private Transform camT;

	private void Start()
	{
		Init();
		book.SetActive(value: false);
	}

	public override void DoAction()
	{
		Main.hero.goHero.GoTo(base.transform.position.x, this);
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(n: false);
		Main.hero.SetAn("Idle");
		camT = Main.game.cam.transform;
		book.transform.position = new Vector3(camT.position.x, camT.position.y, 0f);
		book.SetActive(value: true);
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
		Singleton<Sounds>.use.So("openBook");
	}

	private void RuntimeAction()
	{
		book.transform.position = new Vector3(camT.position.x, camT.position.y, 0f);
		if (Mathf.Abs(Input.GetAxis("Horizontal") + Input.GetAxis("HorizontalJoy")) > 0f || Input.GetButtonDown("Jump"))
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
			book.SetActive(value: false);
		}
	}
}
