using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StartParamsPlayer : ScriptableObject
{
    public float healthPoints = 3;
    public float playerStartX = -1f;
    public float playerStartY = -1.3f;
    public string firstSceneName = "Level1";
}
