using UnityEngine;

public class ScrollBT : MonoBehaviour
{
	protected ButtonUI[] buttons;

	protected int choseBT;

	protected bool switchButton;

	protected int lastBt;

	protected void Unchosed()
	{
		for (int i = 0; i < lastBt; i++)
		{
			buttons[i].SetMode(n: false);
		}
	}

	protected void ScrollActions()
	{
		float axis = Input.GetAxis("Vertical");
		if (Mathf.Abs(axis) > 0.05f)
		{
			if (switchButton)
			{
				return;
			}
			switchButton = true;
			if (axis > 0f)
			{
				Unchosed();
				if (choseBT > 0)
				{
					choseBT--;
				}
				else
				{
					choseBT = lastBt - 1;
				}
				buttons[choseBT].SetMode(n: true);
				Singleton<Sounds>.use.So2("click1", 0.2f);
			}
			else
			{
				Unchosed();
				if (choseBT < lastBt - 1)
				{
					choseBT++;
				}
				else
				{
					choseBT = 0;
				}
				buttons[choseBT].SetMode(n: true);
				Singleton<Sounds>.use.So2("click1", 0.2f);
			}
		}
		else
		{
			switchButton = false;
		}
	}
}
