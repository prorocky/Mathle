using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveAndLoad : MonoBehaviour
{
    private string path = "";
    private string pPath = "";
    private string streakPath = "";

    // Start is called before the first frame update
    void Start()
    {
        // CreatePlayerData();
        SetPaths();
    }

    private void SetPaths(){
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        pPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";  
        streakPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "StreakData.json";
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.K)) {
        //     SaveData();
        // }

        // if (Input.GetKeyDown(KeyCode.L)) {
        //     LoadData();
        // }

        // if (Input.GetKeyDown(KeyCode.X)) {
        //     DeleteData();
        // }
    }

    public void SaveData(PlayerData playerData) {
        string savePath = pPath;

        Debug.Log ("Save Data at " + savePath);
        string json = JsonUtility.ToJson(playerData);
        Debug.Log("Saving -" + json);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    public PlayerData LoadData() {

        if (File.Exists(pPath)){
            using StreamReader reader = new StreamReader(pPath);
            string json = reader.ReadToEnd();

            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Loaded -" + data.ToString());
            return data;
        }else {
            return null;
        }
    }

    public void DeleteData() {
        if (File.Exists(pPath)) {
            File.Delete(pPath);
        }
    }

    public void SaveStreak(StreakSave streakSave) {

        string jsonStreak = JsonUtility.ToJson(streakSave);
        
        using StreamWriter writer = new StreamWriter(streakPath);
        writer.Write(jsonStreak);
    }

    public StreakSave LoadStreak() {
        if (File.Exists(streakPath)) {
            using StreamReader reader = new StreamReader(streakPath);
            string jsonStreak = reader.ReadToEnd();

            StreakSave streakSave = JsonUtility.FromJson<StreakSave>(jsonStreak);
            return streakSave;
        }else {
            return null;
        }
    }

    
}