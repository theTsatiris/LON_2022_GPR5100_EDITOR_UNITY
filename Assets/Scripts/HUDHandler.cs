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

    // Start is called before the first frame update
    void Start()
    {
        NickName.text = GameData.PLAYER_DATA.nickName;
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "" + GameData.PLAYER_DATA.score;

    }

    public void OnSaveButtonClick()
    {
        string jsonString = JsonUtility.ToJson(GameData.PLAYER_DATA);
        Debug.Log(jsonString);
        System.IO.File.WriteAllText(Application.dataPath + "/Saves/" + GameData.PLAYER_DATA.nickName + ".json", jsonString);
    }

    public void OnLoadButtonClick()
    {

    }
}
