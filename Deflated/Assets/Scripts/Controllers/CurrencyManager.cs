using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

/**
 * 
 *  Class containing the functions used in order to manage the currency a player obtains in between game sessions.   
 *  The currency manager is created as a singleton, which means that there in only one instance of the manager in
 *  one game session. This facilitates for consistency when moving from scene to scene in the game flow.
 *  The currency manager will be instantiated once the game starts, and is never destroyed on load. Whenever a 
 *  "new" currency manager is added in another scene, it sets to the already created instance of the manager.
 *
 **/
public class CurrencyManager : MonoBehaviour {

    public static CurrencyManager currencyManager;
    public float currency;

    // Check if there already is an instance of a currency manager, if there is one, assign the existing one, if not,
    // create a new one.
    private void Awake() {
        if (currencyManager == null) {
            DontDestroyOnLoad(gameObject);
            currencyManager = this;
        } else if (currencyManager != this) {
            Destroy(gameObject);
        }
    }
    // Function used to store the currency a player obtains during a game session. The currency is stored in
    // in a .dat file which is located on the users PC.
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

    // Function to load the stored currency of a user from the .dat file which stores the currency a player has
    // has obtained in different game sessions.
    public void Load() {
        // Check if the .dat file already exists
        // TODO: This function should create a new file if it does not already exist.
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