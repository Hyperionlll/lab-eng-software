using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool isMainGame;
    public float localMusicVolume, localSFXVolume;
    public static GameManager Instance;
    public string localChosenClass = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject);
        SetSoundData();

        if (isMainGame)
        {
            localChosenClass = LoadChosenClass();

            if (localChosenClass == "Mage")
            {
                GameObject.Find("Stamina").SetActive(false);
                GameObject.Find("Archer").SetActive(false);
                GameObject.Find("Knight").SetActive(false);
            }
            else if (localChosenClass == "Archer")
            {
                GameObject.Find("Mana").SetActive(false);
                GameObject.Find("Mage").SetActive(false);
                GameObject.Find("Knight").SetActive(false);
            }
            else if (localChosenClass == "Knight")
            {
                GameObject.Find("Mana").SetActive(false);
                GameObject.Find("Mage").SetActive(false);
                GameObject.Find("Archer").SetActive(false);
            }
        }
    }

    [System.Serializable]
    public class ChosenClass
    {
        public string chosenClass;
    }

    [System.Serializable]
    public class SoundData
    {
        public float musicVolume;
        public float sfxVolume;
    }

    public void GetSFXVolume(Slider sender)
    {
        localSFXVolume = sender.value;
    }

    public void GetMusicVolume(Slider sender)
    {
        localMusicVolume = sender.value;
    }

    public void SaveSoundData()
    {
        SoundData data = new()
        {
            musicVolume = localMusicVolume,
            sfxVolume = localSFXVolume
        };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/sound-data.json", json);
    }

    public SoundData LoadSoundData()
    {
        string path = Application.persistentDataPath + "/sound-data.json";
        SoundData data;

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<SoundData>(json);
        }
        else
        {
            data = null;
        }

        return data;
    }

    public void SetSoundData()
    {
        SoundData data = LoadSoundData();

        Slider musicSlider = GameObject.Find("Music").GetComponentInChildren<Slider>();
        Slider sfxSlider = GameObject.Find("Sound Effects").GetComponentInChildren<Slider>();

        
        if (data != null)
        {
            musicSlider.fillRect.gameObject.SetActive(true);
            musicSlider.value = data.musicVolume;

            sfxSlider.fillRect.gameObject.SetActive(true);
            sfxSlider.value = data.sfxVolume;
        }
    }

    public void SaveChosenClass(string className)
    {
        ChosenClass chosenClass = new()
        {
            chosenClass = className
        };

        string json = JsonUtility.ToJson(chosenClass);

        File.WriteAllText(Application.persistentDataPath + "chosen-class.json", json);
    }

    public string LoadChosenClass()
    {
        string path = Application.persistentDataPath + "chosen-class.json";
        ChosenClass chosenClass;

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            chosenClass = JsonUtility.FromJson<ChosenClass>(json);
        }
        else
        {
            chosenClass = null;
        }

        return chosenClass.chosenClass;
    }
}
