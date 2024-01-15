using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class InputChecker : Singleton<InputChecker>
{
	private List<GameObjInterface> gameObj = new List<GameObjInterface>();

	private List<GameObjInterface> gameObj2 = new List<GameObjInterface>();

	private KeyCode[] joyKeys;

	private const uint countOfJoyKeys = 20u;

	private Sprite[] icons;

	private Sprite[] icons2;

	internal int currentIcon;

	private bool vibro;

	private float vibroTime;

	public void Init()
	{
		SetUse(this);
		base.gameObject.name = "[INPUT]";
		joyKeys = new KeyCode[20];
		joyKeys[0] = KeyCode.JoystickButton0;
		joyKeys[1] = KeyCode.JoystickButton1;
		joyKeys[2] = KeyCode.JoystickButton2;
		joyKeys[3] = KeyCode.JoystickButton3;
		joyKeys[4] = KeyCode.JoystickButton4;
		joyKeys[5] = KeyCode.JoystickButton5;
		joyKeys[6] = KeyCode.JoystickButton6;
		joyKeys[7] = KeyCode.JoystickButton7;
		joyKeys[8] = KeyCode.JoystickButton8;
		joyKeys[9] = KeyCode.JoystickButton9;
		joyKeys[10] = KeyCode.JoystickButton10;
		joyKeys[11] = KeyCode.JoystickButton11;
		joyKeys[12] = KeyCode.JoystickButton12;
		joyKeys[13] = KeyCode.JoystickButton13;
		joyKeys[14] = KeyCode.JoystickButton14;
		joyKeys[15] = KeyCode.JoystickButton15;
		joyKeys[16] = KeyCode.JoystickButton16;
		joyKeys[17] = KeyCode.JoystickButton17;
		joyKeys[18] = KeyCode.JoystickButton18;
		joyKeys[19] = KeyCode.JoystickButton19;
		icons = new Sprite[2];
		for (int i = 1; i <= 2; i++)
		{
			icons[i - 1] = Resources.Load<Sprite>("Sprites/ButtonsIcons/action" + i);
		}
		icons2 = new Sprite[2];
		for (int j = 1; j <= 2; j++)
		{
			icons2[j - 1] = Resources.Load<Sprite>("Sprites/ButtonsIcons/skill" + j);
		}
	}

	public void AddObj(GameObjInterface n)
	{
		gameObj.Add(n);
		n.SetSprite(icons[currentIcon]);
	}

	public void AddObj2(GameObjInterface n)
	{
		gameObj2.Add(n);
		n.SetSprite(icons2[currentIcon]);
	}

	public void RemoveObj(GameObjInterface n)
	{
		for (int i = 0; i < gameObj.Count; i++)
		{
			if (gameObj[i] == n)
			{
				gameObj.RemoveAt(i);
				break;
			}
		}
	}

	public void RemoveObj2(GameObjInterface n)
	{
		for (int i = 0; i < gameObj2.Count; i++)
		{
			if (gameObj2[i] == n)
			{
				gameObj2.RemoveAt(i);
				break;
			}
		}
	}

	public void ClearObj()
	{
		gameObj.Clear();
		gameObj2.Clear();
	}

	private void SetMode(int isKeyboard)
	{
		if (isKeyboard == currentIcon)
		{
			return;
		}
		currentIcon = isKeyboard;
		foreach (GameObjInterface item in gameObj)
		{
			item.SetSprite(icons[isKeyboard]);
		}
		foreach (GameObjInterface item2 in gameObj2)
		{
			item2.SetSprite(icons2[isKeyboard]);
		}
	}

	private void Update()
	{
		if (vibro)
		{
			vibroTime -= Time.deltaTime;
			if (vibroTime < 0f)
			{
				StopVibro();
			}
		}
		if (Mathf.Abs(Input.GetAxis("HorizontalJoy")) > 0f)
		{
			SetMode(0);
		}
		else
		{
			if (!Input.anyKeyDown)
			{
				return;
			}
			for (int i = 0; (long)i < 20L; i++)
			{
				if (Input.GetKeyDown(joyKeys[i]))
				{
					SetMode(0);
					return;
				}
			}
			SetMode(1);
		}
	}

	public void SetVibro(float n)
	{
		vibroTime = n;
		vibro = true;
		StartVibro();
	}

	public void StartVibro()
	{
		//GamePad.SetVibration(PlayerIndex.One, 0.3f, 0.3f);
	}

	public void StopVibro()
	{
		//GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
		vibro = false;
	}

	public void SetSpecialVibro(float power)
	{
		//GamePad.SetVibration(PlayerIndex.One, power, power);
	}
}
