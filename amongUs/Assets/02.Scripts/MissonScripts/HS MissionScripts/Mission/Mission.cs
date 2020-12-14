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
        switch (place)
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
