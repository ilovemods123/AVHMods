global using UnityEngine;
global using uObject=UnityEngine.Object;
using MelonLoader;
using HarmonyLib;
using UnityStandardAssets.Characters.FirstPerson;
using System;
[assembly:MelonInfo(typeof(BetterCharController.ModMain),"BetterCharController","1.0.0","Silentstorm")]
[assembly:MelonGame("Sayan","Apes vs Helium")]
namespace BetterCharController{
    public class ModMain:MelonMod{
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
            Log("BetterCharController loaded!");
        }
        [HarmonyPatch(typeof(EquipmentScript),"Start")]
        public class EquipmentScriptStart{
            [HarmonyPostfix]
            public static void Postfix(EquipmentScript __instance){
                GameObject player=__instance.gameObject;
                uObject.Destroy(player.GetComponent<FirstPersonController>());
                player.AddComponent<CustomCharController>().charController=player.GetComponent<CharacterController>();
                Cursor.lockState=CursorLockMode.Locked;
            }
        }
    }
}
