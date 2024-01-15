using System;
using UnityEngine;

public class Test : MonoBehaviour
{
	private delegate void SetMethodAction();

	private SetMethodAction actionM;

	private void Start()
	{
		actionM = (SetMethodAction)Delegate.Combine(actionM, new SetMethodAction(A1));
		actionM = (SetMethodAction)Delegate.Combine(actionM, new SetMethodAction(A2));
		actionM = (SetMethodAction)Delegate.Combine(actionM, new SetMethodAction(A3));
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.W))
		{
			actionM();
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			actionM = (SetMethodAction)Delegate.Combine(actionM, new SetMethodAction(A1));
			actionM = (SetMethodAction)Delegate.Combine(actionM, new SetMethodAction(A2));
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			actionM = null;
			actionM = (SetMethodAction)Delegate.Combine(actionM, new SetMethodAction(A1));
		}
	}

	private void A1()
	{
		MonoBehaviour.print("a1");
	}

	private void A2()
	{
		MonoBehaviour.print("a2");
	}

	private void A3()
	{
		MonoBehaviour.print("a3");
	}
}
