using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
	public static void SaveData(Saves saves)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fileStream = new FileStream(Application.persistentDataPath + "/playerData.deq", FileMode.Create);
		SaveBase graph = new SaveBase(saves);
		binaryFormatter.Serialize(fileStream, graph);
		fileStream.Close();
	}

	public static SaveBase LoadData()
	{
		string path = Application.persistentDataPath + "/playerData.deq";
		if (File.Exists(path))
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			FileStream fileStream = new FileStream(path, FileMode.Open);
			SaveBase result = binaryFormatter.Deserialize(fileStream) as SaveBase;
			fileStream.Close();
			return result;
		}
		Debug.Log("NOT EXIST");
		return null;
	}
}
