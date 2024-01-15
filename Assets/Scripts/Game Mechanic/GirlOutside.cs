public class GirlOutside : AnimationCharacter
{
	private void Start()
	{
		anPos.Init();
	}

	public void SetDirect(bool n)
	{
		sp.flipX = !n;
	}
}
