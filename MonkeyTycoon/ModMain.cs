using MelonLoader;
using HarmonyLib;
using System.Reflection;
using System.Linq;
using TMPro;
[assembly:MelonInfo(typeof(MonkeyTycoon.ModMain),"MonkeyTycoon","1.0.0","Silentstorm")]
[assembly:MelonGame("Sayan","Apes vs Helium")]
namespace MonkeyTycoon{
    public class ModMain:MelonMod{
        public static SellButtonScript sellButton=null;
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
            Log("MonkeyTycoon loaded!");
        }
        [HarmonyPatch(typeof(Currency),"UpdateCashBack")]
        public class CurrencyUpdateCashBack_Patch{
            [HarmonyPrefix]
            public static bool Prefix(Currency __instance){
                __instance.moneyBack=__instance.moneySpent;
                if(sellButton==null){
                    sellButton=UnityEngine.Object.FindObjectOfType<SellButtonScript>();
                }
                sellButton.UpdateText();
                return false;
            }
        }
    }
}
