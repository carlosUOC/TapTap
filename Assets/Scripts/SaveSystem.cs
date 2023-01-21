using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;


public static class SaveSystem
{
	// default path for saving internal data corresponding to the OS in which the app is going to be run
	public static string filePath = Application.persistentDataPath + "/PlayerDadtasassaa.skrt";
    

	public static void SaveGame(PlayerDataMessaging gameData)
	{
		// we create the binary formatter to codify the information making it safer
		BinaryFormatter formatter = new BinaryFormatter(); 

		// creating the stream to write the info in the file
		FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);

		// we load the info of the game into the PlayerDataMessaging class
		PlayerDataMessaging updatedData = PlayerDataManager.Instance.UpdatePlayerData(gameData);

		// writing in the file with serialize function
		formatter.Serialize(stream, updatedData);
		stream.Close();
	}


	public static void LoadGame()
	{
		if(File.Exists(filePath))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

			PlayerDataMessaging data = formatter.Deserialize(stream) as PlayerDataMessaging;
			if(data != null)
				PlayerDataManager.Instance.SetLoadedPlayerData(data);

			stream.Close();
		}
		else
		{
			Debug.Log("Save file not found in " + filePath + ". Creating one.");
			//CreateFile();
		}
	}

	
	public static void CreateFile()
	{
		SaveGame(null);
	}
}
