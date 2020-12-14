using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MissionList
{
    public enum COMMON_MISSIONLIST
    {  //공통임무리스트
        ELECTRICFIX = 1, // 전기 배선 고치기 
        SCRATCHINGCARD = 2, //카드긁기 ->구현  MiniGame_SwipeCard
    }
    public enum SIMPLE_MISSIONLIST //단순임무 리스트 
    {
        SWITCH = 1, //스위치 켜서 고치기 -> 구현 MiniGame_DivertPower
        NAVIGATION = 2, // 네비
        ROUTEFIXING, //항로조종하기 -> 구현  MiniGame_Navagation
        DOWNLOADING, //다운로드  ->구현  MiniGame_UploadData
        DECIDUOUSLY, //낙엽버리기  

    }
    public enum DIFFUCLT_MISSIONLIST //복잡임무 리스트 
    {
        SCANNING = 1,//스캔
        SHOOTING = 2, //총쏘기  ->구현  MiniGame_ClearAsteroids
        TRASHING = 3, // 쓰레기버리기  -> 구현  MiniGame_EmptyGarbage
        DNASEARCHING = 4, //의무실 누르고 60초 기다렸다 오는거  ->구현 MiniGame_InspectSample
        FILLOIL = 5, //기름채우기  
        DISTRIBUTOR = 6, // 원 일치시기키기 -> 구현 MiniGame_Distributor
    }
    public enum SAVOTAGE_MISSIONLIST //사보타지 미션 
    {
        ELECTRICSWITCHFIX = 1, //전기 고치기
        OXYGENFIX = 2,//산소고치기
        FURNACEFIX, //용광로고치기
    }

    public enum REPORT
    {
        EMERGENCY_MEETING = 1,
        DEAD_BODY_REPORTED
    }
}
