using UnityEngine;

public class WindowHome : ActiveObj
{
	public DoorHome doorHome;

	public Basket busket;

	public BasketFallen basketFallen;

	private bool firstView = true;

	private GameObject house;

	private Shack shack;

	public bool isSee;

	public BoxCollider2D boxCollider2DStump;

	public BoxCollider2D boxCollider2DDoor;

	private void Start()
	{
		Init();
		busket.gameObject.SetActive(value: false);
		basketFallen.gameObject.SetActive(value: false);
		isSee = false;
		house = Object.Instantiate(Resources.Load("Prefabs/Other/Shack")) as GameObject;
		shack = house.GetComponent<Shack>();
		shack.SetMode(n: false, inside: false);
		boxCollider2DStump.enabled = false;
		boxCollider2DDoor.enabled = false;
		Main.game.objects.Add(house);
	}

	public override void DoAction()
	{
		if (Main.hero.goHero.GoTo(base.transform.position.x, this))
		{
			OverObj(n: false);
		}
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(n: false);
		Main.game.cam.isFollow = false;
		if (firstView)
		{
			Main.hero.SetAn("windowSee2");
			Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
		}
		else
		{
			Main.hero.SetAn("windowSee");
			Singleton<ManagerFunctions>.use.addFunction(RuntimeAction2);
			isSee = true;
		}
	}

	private void RuntimeAction2()
	{
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction2);
			shack.SetMode(n: true, doorHome.monster.inside);
			Singleton<ManagerFunctions>.use.addFunction(RuntimeAction3);
			Main.game.scenes["forest"].SetActive(value: false);
			Main.hero.gameObject.SetActive(value: false);
		}
	}

	private void RuntimeAction3()
	{
		if (Input.GetButtonDown("Action"))
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction3);
			Main.game.scenes["forest"].SetActive(value: true);
			Main.hero.gameObject.SetActive(value: true);
			Main.hero.ReturnToGame();
			shack.SetMode(n: false, inside: false);
			isSee = false;
			Main.game.cam.isFollow = true;
		}
	}

	private void RuntimeAction()
	{
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.SetDirect(n: false);
			firstView = false;
			doorHome.windowEnd = true;
			boxCollider2DStump.enabled = true;
			boxCollider2DDoor.enabled = true;
			Main.hero.SetHeroAn(1);
			Main.hero.ReturnToGame();
			Main.hero.gameObject.SetActive(value: false);
			Main.game.scenes["forest"].SetActive(value: false);
			GameObject obj = Object.Instantiate(Resources.Load("Prefabs/Other/WindowCutScene")) as GameObject;
			obj.GetComponent<WindowCutScene>().windowHome = this;
			obj.transform.position = new Vector3(Main.game.cam.transform.position.x, Main.game.cam.transform.position.y, 0f);
		}
	}
}
