using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour {

    public static CurrencyManager currencyManager;
    public float currency;

    private void Awake() {
        if (currencyManager == null) {
            DontDestroyOnLoad(gameObject);
            currencyManager = this;
        } else if (currencyManager != this) {
            Destroy(gameObject);
        }
    }

    public void Save() {
        // Create binary formatter
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        // Create file
        FileStream file = File.Create(Application.persistentDataPath + "/currencyInfo.dat");

        // Create container for currency
        CurrencyData data = new CurrencyData();
        data.currency = currency;

        // Write the container to the selected file
        binaryFormatter.Serialize(file, data);
        // Close the file
        file.Close();
    }

    public void Load() {
        if (File.Exists(Application.persistentDataPath + "/currencyInfo.dat")) {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/currencyInfo.dat", FileMode.Open);
            CurrencyData data = (CurrencyData)binaryFormatter.Deserialize(file);
            file.Close();

            currency = data.currency;
        }
    }

}

// Class acting as a data container allowing writing to file
[Serializable]
class CurrencyData {
    public float currency;
}