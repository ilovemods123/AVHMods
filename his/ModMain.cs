using MelonLoader;
using HarmonyLib;
using UnityEngine;
using uObject=UnityEngine.Object;
using UnityEngine.UI;
using System.Linq;
[assembly:MelonInfo(typeof(avhhis.ModMain),"H.I.S","1.0.0","Silentstorm")]
[assembly:MelonGame("Sayan","Apes vs Helium")]
namespace avhhis{
    public class ModMain:MelonMod{
        public static bool HISEnabled=false;
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
            Log("H.I.S Loaded!");
        }
        [HarmonyPatch(typeof(BananaScript),"OnTriggerEnter")]
        public class BananaScriptOnTriggerEnter_Patch{
            [HarmonyPrefix]
            public static bool Prefix(BananaScript __instance,Collider other){
		        if(HISEnabled==true){
                    if(other.tag=="Player"){
			            InteractScript.instance.audioSource.PlayOneShot(__instance.pickUpSound);
			            uObject.Destroy(__instance.gameObject);
                        return false;
                    }
                }
                return true;
	        }
        }
        [HarmonyPatch(typeof(BananaScript),"Decay")]
        public class BananaScriptDecay_Patch{
            [HarmonyPrefix]
            public static bool Prefix(BananaScript __instance){
                if(HISEnabled==true){
                    uObject.Destroy(__instance.gameObject);
                    return false;
                }
                return true;
            }
        }
        [HarmonyPatch(typeof(Currency),"CashBack")]
        public class CurrencyCashBack_Patch{
            [HarmonyPrefix]
            public static void Prefix(ref Currency __instance){
		        if(HISEnabled==true){
                    __instance.moneyBack=0;
                    __instance.moneySpent=0;
                }
	        }
        }
        [HarmonyPatch(typeof(PlayerHealth),"Start")]
        public class PlayerHealthStart_Patch{
            [HarmonyPostfix]
            public static void Postfix(ref PlayerHealth __instance){
                if(HISEnabled==true){
                    __instance.UpdateHealth(-__instance.health+1);
                }
            }
        }
        [HarmonyPatch(typeof(GameManagerScript),"Start")]
        public class GameManagerScriptStart_Patch{
            [HarmonyPostfix]
            public static void Postfix(GameManagerScript __instance){
                var idfk=Resources.FindObjectsOfTypeAll<GridLayoutGroup>().First(a=>a.name.Contains("DifficultiesGroup"));
                idfk.constraint=GridLayoutGroup.Constraint.FixedColumnCount;
                idfk.constraintCount=5;
                Button hisButton=uObject.Instantiate(idfk.GetComponentsInChildren<Button>().First(a=>a.name=="Impoppable"),idfk.gameObject.transform);
                hisButton.gameObject.name="HIS";
                hisButton.gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text="H.I.S";
                hisButton.onClick.RemoveAllListeners();
                hisButton.onClick.AddListener(()=>__instance.SelectDifficulty(5));
                hisButton.onClick.AddListener(()=>__instance.StartGame());
                hisButton.onClick.AddListener(()=>idfk.transform.parent.gameObject.SetActive(!idfk.gameObject.activeSelf));
                hisButton.onClick.AddListener(()=>HISEnabled=true);
            }
        }
     }
}
