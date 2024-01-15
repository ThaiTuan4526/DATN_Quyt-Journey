using UnityEngine;

public class DoorButterfly : MonoBehaviour
{
	public GameObject doorObject;

	private void Start()
	{
		base.gameObject.transform.GetChild(0).gameObject.SetActive(value: false);
		doorObject.SetActive(value: false);
	}

	public void ToOpen()
	{
		base.gameObject.transform.GetChild(0).gameObject.SetActive(value: true);
		doorObject.SetActive(value: true);
		Singleton<ManagerFunctions>.use.addFunction(GoOpen);
	}

	private void GoOpen()
	{
		base.transform.Translate(Vector3.down * Time.deltaTime);
		if (base.transform.position.y < -3.26f)
		{
			Singleton<ManagerFunctions>.use.removeFunction(GoOpen);
			Object.Destroy(base.gameObject);
		}
	}
}
