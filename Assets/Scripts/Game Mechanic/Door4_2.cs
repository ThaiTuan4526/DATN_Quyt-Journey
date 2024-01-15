using UnityEngine;

public class Door4_2 : MonoBehaviour
{
	public Door4 door;

	public MonsterCamp monsterCamp;

	private void OnTriggerEnter2D(Collider2D n)
	{
		Main.hero.rb.velocity = Vector2.zero;
		Main.hero.jump = false;
		Main.hero.fall = false;
		Main.hero.heroT.position = new Vector3(9.6f, -1f, 0f);
		Main.hero.rb.isKinematic = true;
		Main.hero.SetDirect(n: true);
		Main.hero.stopActions = true;
		Main.hero.SetAn("Run");
		Main.game.ShowScene("camp");
		Main.hero.SetStepsNames(1);
		Main.game.cam.ToFollow(Main.hero.heroT);
		Main.game.cam.SetFollowPos(new Vector3(12.9829f, 1.739768f, -10f));
		Main.hero.sp.maskInteraction = SpriteMaskInteraction.None;
		Singleton<Music>.use.MusicStart("forest");
		monsterCamp.SetGoDown();
		Singleton<ManagerFunctions>.use.addFunction(GoTo);
	}

	private void GoTo()
	{
		Main.hero.heroT.position = new Vector2(Main.hero.heroT.position.x + 2.3999999f * Time.deltaTime, Main.hero.heroT.position.y - 0.59999996f * Time.deltaTime);
		if (Main.hero.heroT.position.y < -1.268487f)
		{
			Main.hero.ReturnToGame();
			Singleton<ManagerFunctions>.use.removeFunction(GoTo);
			door.CloseDoor();
		}
	}
}
