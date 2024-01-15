using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(GameObj))]
public class HollowTree2 : ActiveObj
{
	public GameObject hollow;

	public GameObject amber;

	private void Start()
	{
		Init();
		hollow.SetActive(value: false);
		amber.SetActive(value: true);
	}

	public override void DoAction()
	{
		if (Main.hero.CanUse())
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
		}
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(n: false);
		Main.game.cam.isFollow = false;
		hollow.SetActive(value: true);
		hollow.transform.position = new Vector3(Main.game.cam.transform.position.x, Main.game.cam.transform.position.y, 0f);
		Singleton<ManagerFunctions>.use.addFunction(GoDown);
	}

	private void ToSee()
	{
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(ToSee);
			hollow.SetActive(value: true);
			hollow.transform.position = new Vector3(Main.game.cam.transform.position.x, Main.game.cam.transform.position.y, 0f);
			Singleton<ManagerFunctions>.use.addFunction(GoDown);
		}
	}

	private void GoDown()
	{
		if (Mathf.Abs(Input.GetAxis("Horizontal") + Input.GetAxis("HorizontalJoy")) > 0f || Input.GetButtonDown("Jump"))
		{
			Main.game.cam.isFollow = true;
			hollow.SetActive(value: false);
			Main.hero.ReturnToGame();
			Singleton<ManagerFunctions>.use.removeFunction(GoDown);
		}
		else if (Input.GetButtonDown("Action") && amber.activeSelf)
		{
			Main.game.inv.AddItem(Items.amber1);
			amber.SetActive(value: false);
			Singleton<Sounds>.use.So("newItem");
		}
	}
}
