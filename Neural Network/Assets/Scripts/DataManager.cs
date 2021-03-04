﻿using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Xml.Serialization;

[XmlRoot("Data")]
public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public string path;

    XmlSerializer serializer = new XmlSerializer(typeof(Data));
    Encoding encoding = Encoding.GetEncoding("UTF-8");

    public void Awake()
    {
        instance = this;
        SetPath();

    }

    public void Save(List<NeuralNetwork> _nets)
    {
        StreamWriter streamWriter = new StreamWriter(path, false, encoding); //créé la liste de neural networks au chemin, saute des lignes, et encodé
        Data data = new Data { nets = _nets };

        serializer.Serialize(streamWriter, data);
    }

    public Data Load()
    {
        if (File.Exists(path))
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);

            return serializer.Deserialize(fileStream) as Data;
        }

        return null;
    }

    public void SetPath()
    {
        path = Path.Combine(Application.persistentDataPath, "Data.xml"); //combine combine 2 string dans ce cas, 2 strings de chemins
    }
}
