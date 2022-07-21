using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroUIHandler : MonoBehaviour
{
    [SerializeField]
    TMP_InputField nickNameField;

    // Start is called before the first frame update
    void Start()
    {
        GameData.PLAYER_DATA = new PlayerData();
        GameData.PLAYER_DATA.score = 0;
        GameData.PLAYER_DATA.nickName = "";
        GameData.PLAYER_DATA.lastPosition = new Vector3(0.0f, 0.0f, 0.0f);

        GameData.PLAYER_DATA.goodCollectiblePositions = new List<Vector3>();
        GameData.PLAYER_DATA.badCollectiblePositions = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnterButtonClick()
    {
        GameData.PLAYER_DATA.nickName = nickNameField.text;
        SceneManager.LoadScene("RealTimeScene");
    }
}
