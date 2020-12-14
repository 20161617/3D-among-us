using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManger : MonoBehaviour
{
    public static SoundManger instance;

    /* enum SOUND
     {
         FOOTSTEP = 0,
         ENGINE_ROOM,
         MED_BAY,
         ADMIN,
         SECURITY,
         REACTOR,
         STORAGE,
         ARMORY,
         RESTAURENT,
         COMMUNICATE,
         SHIELD,
         OXYZEN,
         ELECTRIC,
         NAVI
     };*/

     AudioSource[] audioSources = new AudioSource[14];
     AudioClip[] audioClips = new AudioClip[14];
    
    public AudioClip footStep;
    public AudioClip engineRoom;
    public AudioClip reactor;
    public AudioClip restaurant;
    public AudioClip armory;
    public AudioClip oxygenSupplyRoom;
    public AudioClip electricChamber;
    public AudioClip protectiveMembraneControlRoom;
    public AudioClip navigationalChamber;
    public AudioClip medBay;
    public AudioClip storage;
    public AudioClip admin;
    public AudioClip security;
    public AudioClip communications;
    private float time = 0;
    private int indexNum;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        for (int i = 0; i < 14; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].Stop();
        }
        audioClips[0] = Resources.Load("FootstepMetal08-sharedassets0.assets.split0-631.wav", typeof(AudioClip)) as AudioClip;
        audioClips[1] = Resources.Load("AMB_EngineRoom-sharedassets0.assets.split0-836", typeof(AudioClip)) as AudioClip;
        audioClips[2] = Resources.Load("AMB_MedbayRoom-sharedassets0.assets.split0-682", typeof(AudioClip)) as AudioClip;
        audioClips[3] = Resources.Load("AMB_Admin-sharedassets0.assets.split0-711", typeof(AudioClip)) as AudioClip;
        audioClips[4] = Resources.Load("AMB_SecurityRoom-sharedassets0.assets.split0-803", typeof(AudioClip)) as AudioClip;
        audioClips[5] = Resources.Load("AMB_Reactor-sharedassets0.assets.split0-708", typeof(AudioClip)) as AudioClip;
        audioClips[6] = Resources.Load("AMB_Storage-sharedassets0.assets.split0-779", typeof(AudioClip)) as AudioClip;
        audioClips[7] = Resources.Load("AMB_Weapons-sharedassets0.assets.split0-624", typeof(AudioClip)) as AudioClip;
        audioClips[8] = Resources.Load("AMB_Cafeteria-sharedassets0.assets.split0-610", typeof(AudioClip)) as AudioClip;
        audioClips[9] = Resources.Load("AMB_CommsRoom-sharedassets0.assets.split0-602", typeof(AudioClip)) as AudioClip;
        audioClips[10] = Resources.Load("AMB_ShieldRoom-sharedassets0.assets.split0-592", typeof(AudioClip)) as AudioClip;
        audioClips[11] = Resources.Load("AMB_O2Room-sharedassets0.assets.split0-712", typeof(AudioClip)) as AudioClip;
        audioClips[12] = Resources.Load("AMB_ElectricRoom-sharedassets0.assets.split0-751", typeof(AudioClip)) as AudioClip;
        audioClips[13] = Resources.Load("AMB_LabHallway-sharedassets0.assets.split0-828", typeof(AudioClip)) as AudioClip;
        for (int j = 0; j < 14; j++)
        {
            audioSources[j].clip = audioClips[j];
            audioSources[j].loop = false;
            audioSources[j].playOnAwake = false;
        }
    }
   

    public void PlayFootStep()
    {
        //발소리 출력 -> 일정시간이상 움직일 시 사운드 출력됨
        time += Time.deltaTime;
        if (time >= 0.5f)
        {
            audioSources[0].PlayOneShot(footStep);
            time = 0;
            
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "UpperEngine" || other.tag == "LowerEngine")
        {
            audioSources[1].clip = engineRoom;
            audioSources[1].loop = true;
            audioSources[1].Play();
            indexNum = 1;
            Debug.Log("UpperEngine collider");
        }
        else if (other.tag == "MedBay")
        {
            audioSources[2].clip = medBay;
            audioSources[2].loop = true;
            audioSources[2].Play();
            indexNum = 2;
            Debug.Log("security collider");
        }
        else if (other.tag == "Admin")
        {
            audioSources[3].clip = admin;
            audioSources[3].loop = true;
            audioSources[3].Play();
            indexNum = 3;
            Debug.Log("security collider");
        }
        else if (other.tag == "Security")
        {
            audioSources[4].clip = security;
            audioSources[4].loop = true;
            audioSources[4].Play();
            indexNum = 4;
            Debug.Log("security collider");
        }
        else if (other.tag == "Reactor")
        {
            audioSources[5].clip = reactor;
            audioSources[5].loop = true;
            audioSources[5].Play();
            indexNum = 5;
            Debug.Log("reactor collider");
        }
        else if (other.tag == "Storage")
        {
            audioSources[6].clip = storage;
            audioSources[6].loop = true;
            audioSources[6].Play();
            indexNum = 6;
            Debug.Log("security collider");
        }
        else if (other.tag == "Armory")
        {
            audioSources[7].clip = armory;
            audioSources[7].loop = true;
            audioSources[7].Play();
            indexNum = 7;
            Debug.Log("security collider");
        }
        else if (other.tag == "Restaurant")
        {
            audioSources[8].clip = restaurant;
            audioSources[8].loop = true;
            audioSources[8].Play();
            indexNum = 8;
            Debug.Log("security collider");
        }
        else if (other.tag == "Communications")
        {
            audioSources[9].clip = communications;
            audioSources[9].loop = true;
            audioSources[9].Play();
            indexNum = 9;
            Debug.Log("security collider");
        }
        else if (other.tag == "ProtectiveMembraneControlRoom")
        {
            audioSources[10].clip = protectiveMembraneControlRoom;
            audioSources[10].loop = true;
            audioSources[10].Play();
            indexNum = 10;
            Debug.Log("security collider");
        }
        else if (other.tag == "OxygenSupplyRoom")
        {
            audioSources[11].clip = oxygenSupplyRoom;
            audioSources[11].loop = true;
            audioSources[11].Play();
            indexNum = 11;
            Debug.Log("security collider");
        }
        else if (other.tag == "ElectricChamber")
        {
            audioSources[12].clip = electricChamber;
            audioSources[12].loop = true;
            audioSources[12].Play();
            indexNum = 12;
            Debug.Log("security collider");
        }
        else if (other.tag == "NavigationalChamber")
        {
            audioSources[13].clip = navigationalChamber;
            audioSources[13].loop = true;
            audioSources[13].Play();
            indexNum = 13;
            Debug.Log("security collider");
        }
    }
    public void OnTriggerExit(Collider other)
    {
        audioSources[indexNum].loop = false;
        audioSources[indexNum].Stop();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
