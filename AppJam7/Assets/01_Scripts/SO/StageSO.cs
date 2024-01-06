using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stage")]
public class StageSO : ScriptableObject
{
    public int stageNum;
    public List<GameObject> obstacleList; // 나중에 장애물들 공통된 스크립트 따로 있으면 Obstacle 
}
