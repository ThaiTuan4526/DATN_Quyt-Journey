using UnityEngine;

public class ActiveObj : MonoBehaviour, GameObjInterface, GameObjInterfaceOver
{
	public GameObject icon;

	private bool isActive;

	private bool canOver;

	protected void Init(bool isOver = true)
	{
		canOver = isOver;
		if (canOver)
		{
			icon.gameObject.SetActive(value: false);
		}
		isActive = true;
	}

	public void OverObj(bool n)
	{
		if (canOver && Main.TIPS_MODE)
		{
			icon.gameObject.SetActive(n);
		}
	}

	public virtual void DoAction()
	{
		if (isActive && Main.hero.goHero.GoTo(base.transform.position.x, this))
		{
			OverObj(n: false);
		}
	}

	public virtual void DoActionOnPos()
	{
	}

	protected void Activated()
	{
		isActive = true;
		base.gameObject.GetComponent<Trigger2DInterface>().SetActive = true;
	}

	protected void DeActivated()
	{
		isActive = false;
		base.gameObject.GetComponent<Trigger2DInterface>().SetActive = false;
	}

	public void SetSprite(Sprite n)
	{
	}
}
