using UnityEngine;

public class LevelChoseBt : MonoBehaviour
{
	public void UpdateText()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			base.transform.GetChild(i).gameObject.SetActive(value: false);
		}
		base.transform.GetChild(Main.typeLanguageID).gameObject.SetActive(value: true);
	}
}
