using UnityEngine;

public class LiftTrigger : MonoBehaviour
{
	public int liftID;

	public PlatsGame platsGame;

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Hero"))
		{
			platsGame.SetPos(liftID);
			base.gameObject.transform.GetChild(0).gameObject.SetActive(value: false);
			base.gameObject.transform.GetChild(1).gameObject.SetActive(value: true);
			Singleton<Sounds>.use.So("pressbt");
		}
	}

	private void OnTriggerExit2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Hero"))
		{
			base.gameObject.transform.GetChild(1).gameObject.SetActive(value: false);
			base.gameObject.transform.GetChild(0).gameObject.SetActive(value: true);
		}
	}
}
