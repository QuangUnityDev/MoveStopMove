using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadData : Singleton<SaveLoadData>
{
    public string saveName = "savedGame";
    public string directoryName = "Saves";  

    public void SaveToFile()
    {       
        // To save in a directory, it must be created first
        if (!Directory.Exists(directoryName))
            Directory.CreateDirectory(directoryName);

        // The formatter will convert our unity data type into a binary file
        BinaryFormatter formatter = new BinaryFormatter();

        // Choose the save location
        FileStream saveFile = File.Create(directoryName + "/" + saveName + ".bin");

        // Write our C# Unity game data type to a binary file
    Data data = new Data();
    formatter.Serialize(saveFile, data);

        saveFile.Close();

        // Success message
        print("Game Saved to " + Directory.GetCurrentDirectory().ToString() + "/Saves/" + saveName + ".bin");
    }
    public string saveDirectory = "Saves";
    public string saveNameLoad = "savedGame";

    public void LoadFromFile()
    {
        // Converts binary file back into readable data for Unity game
        BinaryFormatter formatter = new BinaryFormatter();

        // Choosing the saved file to open
        FileStream saveFile = File.Open(saveDirectory + "/" + saveNameLoad + ".bin", FileMode.Open);

        // Convert the file data into SaveGameData format for use in game
        Data loadData = (Data) formatter.Deserialize(saveFile);

        // Print all of the data (normally you would feed this data into other loaded objects that need it like the Player script)
        print("~~~ LOADED GAME DATA ~~~");
        print("PLAYER NAME: " + loadData.currentWeapon);
        //print("PLAYER NAME: " + loadData.weaponOwner[1]);
        //print("PLAYER NAME: " + loadData.playerName);
        //print("MONEY: " + loadData.money);
        //print("HEALTH: " + loadData.health);

        saveFile.Close();
    }
}
