using UnityEngine;

public class BridgeParts : MonoBehaviour, InterfaceBreakObj
{
	public GameObject[] parts;

	public TakePartBridge[] takeObj;

	private void Start()
	{
		BreakObj();
		Main.game.arrBreakObj.Add(this);
	}

	public void BreakObj()
	{
		if (Main.locationStep != 3)
		{
			parts[0].SetActive(value: true);
			parts[1].SetActive(value: true);
			parts[2].SetActive(value: false);
			parts[3].SetActive(value: false);
			parts[4].SetActive(value: false);
			parts[5].SetActive(value: false);
			parts[6].SetActive(value: false);
			parts[7].SetActive(value: false);
		}
		else if (!parts[5].activeSelf && !parts[6].activeSelf && !parts[7].activeSelf)
		{
			Main.game.inv.RemoveItem(Items.bridgepart);
			parts[0].SetActive(value: false);
			parts[1].SetActive(value: false);
			parts[2].SetActive(value: false);
			parts[3].SetActive(value: false);
			parts[4].SetActive(value: false);
			parts[5].SetActive(value: false);
			parts[6].SetActive(value: true);
			parts[7].SetActive(value: true);
		}
		UpdateParts();
	}

	public void SetNextPart(int p, bool direct)
	{
		p = ((!direct) ? (p - 1) : (p + 1));
		parts[p].SetActive(value: true);
		UpdateParts();
	}

	public void TakePart(int p, bool direct)
	{
		p = ((!direct) ? (p - 1) : (p + 1));
		parts[p].SetActive(value: false);
		UpdateParts();
	}

	private void UpdateParts()
	{
		TakePartBridge[] array = takeObj;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].UpdateCollider();
		}
	}
}
