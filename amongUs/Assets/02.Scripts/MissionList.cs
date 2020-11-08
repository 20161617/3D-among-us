using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Mission : MonoBehaviour
{
    public enum PLACE
    {
        RESTAURANT,//식당
        NAVIGATIONALCHAMBER,//항해실
        WEAPONSTORAGE,//무기고
        OXYGENSUPPLY,//산소공급실
        MANAGEMENT,//관리실
        PROTECTIVEBARRIER,//보호막
        COMMUNICATION,//통신실
        WAREHOUSE,//창고
        ELECTRIC,//전기실
        MEDICAL,//의무실
        UPPERENGINE,//상부엔진
        LOWERENGINE,//하부엔진
        SECURITY,//보안실
        ATOMICREACTOR,//원자로
    }


    public abstract void setMission();
    public abstract void getMission();
    public abstract string getText(); // 해당미션 
    protected string missionText;
    protected int missionCount;//이 미션이 몇개가 있는가 ? ex)쓰레기버리기 0/2
    protected PLACE place { get; set; } //객체 오브젝트에서 장소지정해주기 

    protected string placeReturn()
    {
        string _place;
       switch(place)
        {
            case PLACE.RESTAURANT:
            _place = "식당";
            break;
            case PLACE.NAVIGATIONALCHAMBER:
                _place = "항해실";
            break;
            case PLACE.WEAPONSTORAGE:
                _place = "무기고";
                break;
            case PLACE.OXYGENSUPPLY:
                _place = "산소공급실";
                break;
            case PLACE.MANAGEMENT:
                _place = "관리실";
                break;
            case PLACE.PROTECTIVEBARRIER:
                _place = "보호막";
                break;
            case PLACE.COMMUNICATION:
                _place = "통신실";
                break;
            case PLACE.WAREHOUSE:
                _place = "창고";
                break;
            case PLACE.ELECTRIC:
                _place = "전기실";
                break;
            case PLACE.MEDICAL:
                _place = "의무실";
                break;
            case PLACE.UPPERENGINE:
                _place = "상부엔진";
                break;
            case PLACE.LOWERENGINE:
                _place = "하부엔진";
                break;
            case PLACE.SECURITY:
                _place = "보안실";
                break;
            case PLACE.ATOMICREACTOR:
                _place = "원자로";
                break;
            default:
                _place = "ERROR";
                break;
        }
        return _place;
    }
}
public class ElectricFix : Mission
{
    public void Awake()
    {
        missionText =placeReturn()+": 배선 수리하기" + "(0 /" + missionCount+")";
    }
    public override void setMission()
    {
       
    }
    public override void getMission()
    {
        
    }
    public override string getText()
    {
        return missionText;
    }
}
public class MissionList : MonoBehaviour
{

    enum COMMON_MISSIONLIST
    {  //공통임무리스트
        ELECTRICFIX, // 전기 배선 고치기 
        SCRATCHINGCARD, //카드긁기
    }
    enum SIMPLE_MISSIONLIST //단순임무 리스트 
    {
        SWITCH, //스위치 켜서 고치기 
        NAVIGATION, // 네비
        ROUTEFIXING, //항로조종하기 
        DOWNLOADING, //다운로드 
        DECIDUOUSLY, //낙엽버리기 

    }
    enum DIFFUCLT_MISSIONLIST //복잡임무 리스트 
    {
        SCANNING,//스캔
        SHOOTING, //총쏘기 
        TRASHING, // 쓰레기버리기 
        DNASEARCHING, //의무실 누르고 60초 기다렸다 오는거 
        FILLOIL, //기름채우기 
    }
    enum SAVOTAGE_MISSIONLIST //사보타지 미션 
    {
        ELECTRICSWITCHFIX, //전기 고치기
        OXYGENFIX,//산소고치기
        FURNACEFIX, //용광로고치기
    }
    struct MissionStrcut 
    {
        COMMON_MISSIONLIST comMission;
        SIMPLE_MISSIONLIST simMission;
        DIFFUCLT_MISSIONLIST difMission;
    };


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
