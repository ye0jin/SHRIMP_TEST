using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stage")]
public class StageSO : ScriptableObject
{
    public int stageNum;
    public List<GameObject> obstacleList; // ���߿� ��ֹ��� ����� ��ũ��Ʈ ���� ������ Obstacle 
}
