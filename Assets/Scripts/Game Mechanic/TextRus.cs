public class TextRus : TextBase, InterfaceTranslate
{
	public void GetText(int id)
	{
		if (id == 1)
		{
			returnString = "информация";
		}
		else
		{
			returnString = "[нет инфо]";
		}
	}
}
