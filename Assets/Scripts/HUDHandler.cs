using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_Text NickName;
    [SerializeField]
    private TMP_Text Score;

    [SerializeField]
    private GameObject ModalWindow;
    [SerializeField]
    private TMP_InputField FileName;
    [SerializeField]
    private Transform PlayerTransform;

    [SerializeField]
    private CollectibleSpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        ModalWindow.SetActive(false);
        //Score.text = "" + GameData.PLAYER_DATA.score;
        NickName.text = GameData.PLAYER_DATA.nickName;
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "" + GameData.PLAYER_DATA.score;
    }

    public void OnSaveButtonClick()
    {
        GameData.PLAYER_DATA.goodCollectiblePositions.Clear();
        GameData.PLAYER_DATA.badCollectiblePositions.Clear();

        GameObject[] GoodCollectibles = GameObject.FindGameObjectsWithTag("GoodCollectible");
        GameObject[] BadCollectibles = GameObject.FindGameObjectsWithTag("BadCollectible");

        foreach(GameObject obj in GoodCollectibles)
        {
            GameData.PLAYER_DATA.goodCollectiblePositions.Add(obj.transform.position);
        }
        foreach (GameObject obj in BadCollectibles)
        {
            GameData.PLAYER_DATA.badCollectiblePositions.Add(obj.transform.position);
        }

        string jsonString = JsonUtility.ToJson(GameData.PLAYER_DATA);
        Debug.Log(jsonString);
        System.IO.File.WriteAllText(Application.dataPath + "/Saves/" + GameData.PLAYER_DATA.nickName + ".json", jsonString);
    }

    public void OnLoadButtonClick()
    {
        ModalWindow.SetActive(true);
    }

    public void OnFinalLoadClick()
    {
        ModalWindow.SetActive(false);
        if (System.IO.File.Exists(Application.dataPath + "/Saves/" + FileName.text + ".json"))
        {
            string jsonData = System.IO.File.ReadAllText(Application.dataPath + "/Saves/" + FileName.text + ".json");
            GameData.PLAYER_DATA = JsonUtility.FromJson<PlayerData>(jsonData);

            //Score.text = "" + GameData.PLAYER_DATA.score;
            NickName.text = GameData.PLAYER_DATA.nickName;
            PlayerTransform.position = GameData.PLAYER_DATA.lastPosition;

            spawner.SpawnCollectiblesOnLoad();
        }
        else
        {
            Debug.Log("FILE NOT FOUND!");
        }
    }
}
