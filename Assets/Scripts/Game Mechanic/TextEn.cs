public class TextEn : TextBase, InterfaceTranslate
{
	public void GetText(int id)
	{
		if (id == 1)
		{
			returnString = "info";
		}
		else
		{
			returnString = "[none info]";
		}
	}
}
