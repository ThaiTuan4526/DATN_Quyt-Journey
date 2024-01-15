using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(GameObj))]
public class HollowTree : ActiveObj
{
	public GameObject hollow;

	public GameObject pipe;

	public GameObject amber;

	public PipeAby aby;

	private void Start()
	{
		Init();
		hollow.SetActive(value: false);
		amber.SetActive(value: false);
	}

	public override void DoAction()
	{
		Main.hero.goHero.GoTo(base.transform.position.x, this);
	}

	public override void DoActionOnPos()
	{
		Main.game.cam.isFollow = false;
		Main.hero.SetDirect(n: true);
		Main.hero.SetAn("windowSee");
		Singleton<ManagerFunctions>.use.addFunction(ToSee);
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
		else if (Input.GetButtonDown("Action"))
		{
			if (pipe.activeSelf)
			{
				Singleton<Sounds>.use.So("takePipe");
				pipe.SetActive(value: false);
				aby.SetEnable();
			}
			else if (amber.activeSelf)
			{
				Singleton<Sounds>.use.So("newItem");
				Main.game.inv.AddItem(Items.amber2);
				amber.SetActive(value: false);
			}
		}
	}
}
