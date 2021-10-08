using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public static MenuUIHandler Instance;
    public Text recordText;
    public InputField nombreInput;
    public string nombreActual;
    public string nombre;
    public int puntaje;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        LoadRecord();
        MostrarRecord(recordText);
    }    
    public void StartNew()
    {
        nombreActual = nombreInput.text;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

[System.Serializable]
class SaveData
{
    public string nombre;
    public int puntaje;
}

public void SaveRecord(string nuevo_nombre, int nuevo_puntaje)
{
    SaveData data = new SaveData();
    nombre = nuevo_nombre;
    puntaje = nuevo_puntaje;
    data.nombre = nombre;
    data.puntaje = puntaje;
    Debug.Log($"NUEVO RECORD {nombre}: {puntaje}");
    string json = JsonUtility.ToJson(data);
  
    File.WriteAllText(Application.persistentDataPath + "/record.json", json);
}

public void LoadRecord()
{
    string path = Application.persistentDataPath + "/record.json";
    if (File.Exists(path))
    {
        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        nombre = data.nombre;
        puntaje = data.puntaje;
    }
}
public void MostrarRecord(Text recordText)
{
    Debug.Log($"Mostramos Record {nombre}: {puntaje}");
    recordText.text = $"Record {nombre}: {puntaje}";
}
public bool NuevoRecord(int puntos)
{
    return puntos > puntaje;
}
}
