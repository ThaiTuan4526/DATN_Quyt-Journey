using System.Collections.Generic;

public class ManagerFunctions : Singleton<ManagerFunctions>
{
	public delegate void myMethod();

	private int iF;

	private int jF;

	private int rateF;

	private int rateF3;

	public bool isPause;

	private List<myMethod> arrFunctions = new List<myMethod>();

	private List<myMethod> arrIndieFunctions = new List<myMethod>();

	public void Init()
	{
		SetUse(this);
		base.gameObject.name = "[FUNCTIONS]";
	}

	private void Update()
	{
		if (!isPause)
		{
			for (iF = 0; iF < rateF; iF++)
			{
				arrFunctions[iF]();
			}
		}
		for (iF = 0; iF < rateF3; iF++)
		{
			arrIndieFunctions[iF]();
		}
	}

	public void ClearMain()
	{
		arrFunctions.Clear();
		rateF = 0;
	}

	public void ClearAll()
	{
		arrFunctions.Clear();
		arrIndieFunctions.Clear();
		rateF = 0;
		rateF3 = 0;
	}

	public void addFunction(myMethod n)
	{
		for (jF = 0; jF < rateF; jF++)
		{
			if (arrFunctions[jF] == n)
			{
				return;
			}
		}
		arrFunctions.Add(n);
		rateF = arrFunctions.Count;
	}

	public void removeFunction(myMethod n)
	{
		for (jF = 0; jF < rateF; jF++)
		{
			if (arrFunctions[jF] == n)
			{
				arrFunctions.RemoveAt(jF);
				rateF = arrFunctions.Count;
				break;
			}
		}
	}

	public void addIndieFunction(myMethod n)
	{
		for (jF = 0; jF < rateF3; jF++)
		{
			if (arrIndieFunctions[jF] == n)
			{
				return;
			}
		}
		arrIndieFunctions.Add(n);
		rateF3 = arrIndieFunctions.Count;
	}

	public void removeIndieFunction(myMethod n)
	{
		for (jF = 0; jF < rateF3; jF++)
		{
			if (arrIndieFunctions[jF] == n)
			{
				arrIndieFunctions.RemoveAt(jF);
				rateF3 = arrIndieFunctions.Count;
				break;
			}
		}
	}
}
