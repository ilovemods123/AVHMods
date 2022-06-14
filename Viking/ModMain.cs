global using HarmonyLib;
global using CustomUpgrades;
global using UnityEngine;
global using System.Collections.Generic;
global using uObject=UnityEngine.Object;
global using System.Linq;
global using UnityStandardAssets.Characters.FirstPerson;
using MelonLoader;
[assembly:MelonInfo(typeof(Viking.ModMain),"Viking","1.0.0","Silentstorm")]
[assembly:MelonGame("Sayan","Apes vs Helium")]
namespace Viking{
    public class ModMain:CustomUpgradeMod{
        private static MelonLogger.Instance mllog=null;
        public static void Log(object thingtolog,string type="msg"){
            switch(type){
                case"msg":
                    mllog.Msg(thingtolog);
                    break;
                case"warn":
                    mllog.Warning(thingtolog);
                    break;
                 case"error":
                    mllog.Error(thingtolog);
                    break;
            }
        }
        public override void OnApplicationStart(){
            mllog=LoggerInstance;
            Log("Viking loaded!");
        }
        public class Viking:CustomClass{
            public override string Name=>"Viking";
            public override int Cost=>0;
            public override byte[]Bundle=>Bundles.viking;
            public override string Icon=>"Viking-Icon";
            public override void PreStart(UpgradeButtonScript ubs){
                GameObject player=ubs.equipmentScript.gameObject;
                AssetBundle bundle=AssetBundle.LoadFromMemory(Bundle);
                Log("bundle loaded");
                GameObject viking=(GameObject)uObject.Instantiate(bundle.LoadAsset("Viking-RootPrefab"),player.GetComponentInChildren<WeaponMovement>().gameObject.transform);
                viking.GetComponentInChildren<Camera>().gameObject.SetActive(false);
                viking.SetActive(false);
                viking.name="Viking";
                EquipmentScript.instance.weapons.AddItem(viking);
                Log("VRS object loaded");
                Transform[]animatorArray=viking.GetComponentsInChildren<Transform>();
                Log("transform array set");
                foreach(var thing in animatorArray){
                    Log(thing.gameObject.name);
                }
                Log("viking set");
                VikingGroundScript VGS=animatorArray.First(a=>a.gameObject.name=="Viking-GroundModel").gameObject.AddComponent<VikingGroundScript>();
                VikingTransformScript VTS=animatorArray.First(a=>a.gameObject.name=="Viking-TransformModel").gameObject.AddComponent<VikingTransformScript>();
                VikingAirScript VAS=animatorArray.First(a=>a.gameObject.name=="Viking-AirModel").gameObject.AddComponent<VikingAirScript>();
                Log(player.GetComponentInChildren<WeaponMovement>().gameObject);
                VikingRootScript VRS=viking.AddComponent<VikingRootScript>();
                Log("vars set");
                ubs.interactableFromStart=true;
                VRS.GroundScript=VGS;
                VRS.TransformScript=VTS;
                VRS.AirScript=VAS;
                VRS.CharControllerInstance=player.GetComponent<CharacterController>();
                VRS.audioSource=VRS.GetComponent<AudioSource>();
                Log("instances set");
                VRS.GroundScript.CharControllerInstance=VRS.CharControllerInstance;
                VRS.GroundScript.gameObject.SetActive(false);
                Log("VGS CCi set and false");
                VRS.TransformScript.animInstance=VRS.TransformScript.GetComponent<Animator>();
                VRS.TransformScript.gameObject.SetActive(false);
                Log("VTS CCI set and false");
                VRS.AirScript.CharControllerInstance=VRS.CharControllerInstance;
                VRS.AirScript.gameObject.SetActive(false);
                Log("VAS CCI set and false");
                Log("clip");
                foreach(AudioClip audioClip in bundle.LoadAllAssets<AudioClip>()){
                    VRS.audioClips.Add(audioClip);
                    Log(audioClip.name);
                }
            }
        }
    }
}
