using UnityEngine;

public  class SaveLoader
{
    public  void Save(string name, string data)
    {
        PlayerPrefs.SetString(name, data);
    } 
    public  string Load(string name)
    {
        return PlayerPrefs.GetString(name);
    }

}
