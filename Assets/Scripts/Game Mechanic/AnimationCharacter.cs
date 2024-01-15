using UnityEngine;

public class AnimationCharacter : MonoBehaviour
{
	[Header("Hoạt ảnh")]
	public AnPos anPos;

	[Header("Liên kết")]
	public Animator an;

	public SpriteRenderer sp;

	public GameObject skinObj;

	private string myAn;

	public void SetAn(string n)
	{
		if (myAn != n)
		{
			an.Play(n, -1, 0f);
			myAn = n;
			Vector2 pos = anPos.GetPos(n);
			if (sp.flipX)
			{
				pos.x *= -1f;
			}
			skinObj.transform.localPosition = pos;
		}
	}

	public bool AnCompleted()
	{
		return an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
	}
}
