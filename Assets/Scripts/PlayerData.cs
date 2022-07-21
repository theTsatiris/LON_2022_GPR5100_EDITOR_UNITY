using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
    public string nickName;
    public int score;
    public Vector3 lastPosition;

    public List<Vector3> goodCollectiblePositions;
    public List<Vector3> badCollectiblePositions;
}
