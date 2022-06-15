using MelonLoader;
using HarmonyLib;
using UnityEngine;
[assembly:MelonInfo(typeof(RandomUpgrades.ModMain),"RandomUpgrades","1.0.0","Silentstorm")]
[assembly:MelonGame("Sayan","Apes vs Helium")]
namespace RandomUpgrades{
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
            Log("RandomUpgrades loaded!");
        }
        [HarmonyPatch(typeof(EquipmentScript),"ChangeWeapon")]
        public class EquipmentScriptChangeWeapon_Patch{
            [HarmonyPrefix]
            public static bool Prefix(EquipmentScript __instance){
                __instance.currentWeapon.SetActive(false);
                GameObject selectedweapon=__instance.weapons[new System.Random().Next(0,__instance.weapons.Length+1)];
                selectedweapon.SetActive(true);
				__instance.currentWeapon=selectedweapon;
				InteractScript.instance.audioSource.PlayOneShot(InteractScript.instance.interactSound);
                return false;
            }
        }
    }
}
