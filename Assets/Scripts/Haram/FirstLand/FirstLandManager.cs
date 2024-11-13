using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using DG.Tweening;
using UnityEngine;
using Unity.AI.Navigation;

public class FirstLandManager : MonoBehaviour
{
    public static FirstLandManager firstLandManager;
    [SerializeField]
    private GameObject SecondLand;
    [SerializeField]
    private GameObject Maze;
    [SerializeField]
    private GameObject FoodWall;
    [SerializeField]
    private GameObject PathtoSecondLand;
    private GameObject Player;
    [SerializeField]
    private Transform playerTPPoint;
    public bool isMazeFin = false;
    public bool isUnderStart = false;
    public bool isUnderFin = false;

    void Awake()
    {
        firstLandManager = this;
    }
    void Start()
    {
        SecondLand.SetActive(false);
        SoundManager.instance.PlayMusic("puzzle-game-bright-casual-video-game-music-249202");
        StartCoroutine(FirstLandMission());
    }

    IEnumerator FirstLandMission()
    {
        yield return new WaitUntil(() => isMazeFin);
        //UserMap.BuildNavMesh();
        isUnderStart = true;
        //Player.transform.position = playerTPPoint.transform.position;
        yield return new WaitForSeconds(1f);
        Maze.SetActive(false);

        //미로의 아래 지형 미션을 클리어 하길 대기
        yield return new WaitUntil(() => isUnderFin);
        FoodWall.SetActive(false);
        SecondLand.SetActive(true);
    }
}
