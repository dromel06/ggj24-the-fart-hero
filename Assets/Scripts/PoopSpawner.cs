using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PoopSpawner : MonoBehaviour
{
    public AudioSource Audio;
    public GameObject poop;
    public Sprite[] poopsSprites;
    public Transform[] spawnPoints;
    private string archivoDeMusica = "tiemposDeNotas.txt";
    private string filePath = Application.dataPath + "/archivos de cancion/";
    public List<InputData> inputData;
    private float systemTime;
    private int indextemp = 0;
    float tolerancia = 0.1f;

    public event Action OnLoadTimeStamps;
    //private InputData[] inputDataTemp;

    private void Awake()
    {
  
    }

    private void Start()
    {
        Audio.Play();
        LoadTimeStamps();
    }

    private void Update()
    {
        //systemTime += Time.deltaTime;
        systemTime = GetPlaybackTime();
        try
        {
            if (Mathf.Abs((inputData[indextemp].TimeStamp-2) - systemTime) < tolerancia)
            //if(inputData[indextemp].TimeStamp == systemTime)
            {
               // Debug.Log("Tick en el tiempo de juego: " + systemTime);
                InstanciadorDePoops(indextemp);
                indextemp++;
            }
        }
        catch(System.ArgumentOutOfRangeException)
        {
            Debug.Log("termino el archivo");
        }

    }

    public void InstanciadorDePoops(int index)
    {
        switch(inputData[index].KeyIndex)
        {
            case 0:
                poop.GetComponent<SpriteRenderer>().sprite = poopsSprites[0];
                Instantiate(poop, spawnPoints[0].position, Quaternion.identity);
                break;
            case 1:
                poop.GetComponent<SpriteRenderer>().sprite = poopsSprites[1];
                Instantiate(poop, spawnPoints[1].position, Quaternion.identity);
                break;
            case 2:
                poop.GetComponent<SpriteRenderer>().sprite = poopsSprites[2];
                Instantiate(poop, spawnPoints[2].position, Quaternion.identity);
                break;
            case 3:
                poop.GetComponent<SpriteRenderer>().sprite = poopsSprites[3];
                Instantiate(poop, spawnPoints[3].position, Quaternion.identity);
                break;
        }
    }

    public float GetPlaybackTime()
    {
        try
        {
            //Debug.Log(((float)Audio.timeSamples) / Audio.clip.frequency);
            return ((float)Audio.timeSamples) / Audio.clip.frequency;
        }
        catch { return 0f; }
    }

    private void LoadTimeStamps()
    {
        int listIndex = 0;
        if (File.Exists(Path.Combine(filePath, archivoDeMusica))) // Verifica si el archivo existe en la ruta especificada
        {
            string[] contenido = File.ReadAllText(Path.Combine(filePath, archivoDeMusica)).Split('\n'); // Lee todo el contenido del archivo y lo guarda en una cadena
            inputData = new List<InputData>();
            foreach(string input in contenido)
            {
                string[] inputDataString = input.Split('|');
                inputData.Add(new InputData());
                inputData[listIndex].KeyIndex = int.Parse(inputDataString[0]);
                inputDataString[1].Replace("\r", "");
                inputData[listIndex].TimeStamp = float.Parse(inputDataString[1]);
                listIndex++;
                //Debug.Log(inputData[listIndex-1].Index);
            }
            
            OnLoadTimeStamps?.Invoke();
        }
        else
        {
            Debug.LogError("El archivo no existe en la ruta especificada: " + filePath);
            Directory.CreateDirectory(filePath);//se crea carpeta
        }
    }

    public static bool WriteExternalTxt(string filePath, string line, bool append = true)
    {
        StreamWriter test = new StreamWriter(filePath, append);
        //test.WriteLine(line);
        test.Write(line);
        test.Close();
        return true;
    }
}
