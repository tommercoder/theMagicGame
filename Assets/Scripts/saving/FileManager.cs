using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    
    //Zapisuje się do pliku dane
    public static bool WriteToFile(string a_FileName,string a_FileContents)
    {
        //persistentDataPath -> Contains the path to a persistent data directory.
        //C/Users/name/AppData/LocalLow/DefaultCompany/nameOfProject
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);

        try
        {
            File.WriteAllText(fullPath, a_FileContents);
           
            return true;
        }
        catch(Exception e)
        {
            Debug.LogError($"Failed to write to {fullPath} with Exception {e}");

        }
        return false;
    }
    //Ładuje dane z pliku.
    public static bool LoadFromFile(string a_FileName,out string result)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);
        try
        {
            result = File.ReadAllText(fullPath);

            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read from {fullPath} with Exception {e}");
            
            result = "";
            return false;
        }
        
    }
    public static bool ClearSaveData(string name)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, name);
        if (File.Exists(fullPath))
        {
            File.WriteAllText(fullPath, "");
            return true;
        }
        else
        {
            logShow.instance.showText("you are now on start,there is no any savings yet");
        }
        return false;
    }
}
