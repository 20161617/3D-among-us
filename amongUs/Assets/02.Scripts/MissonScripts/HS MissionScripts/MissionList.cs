using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MissionList
{
    public enum COMMON_MISSIONLIST
    {  //공통임무리스트
        ELECTRICFIX = 1, // 전기 배선 고치기 
        SCRATCHINGCARD = 2, //카드긁기
    }
    public enum SIMPLE_MISSIONLIST //단순임무 리스트 
    {
        SWITCH = 1, //스위치 켜서 고치기 
        NAVIGATION = 2, // 네비
        ROUTEFIXING, //항로조종하기 
        DOWNLOADING, //다운로드 
        DECIDUOUSLY, //낙엽버리기 

    }
    public enum DIFFUCLT_MISSIONLIST //복잡임무 리스트 
    {
        SCANNING = 1,//스캔
        SHOOTING = 2, //총쏘기 
        TRASHING, // 쓰레기버리기 
        DNASEARCHING, //의무실 누르고 60초 기다렸다 오는거 
        FILLOIL, //기름채우기 
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
