using MelonLoader;
using HarmonyLib;
[assembly:MelonInfo(typeof(avhdeflation.ModMain),"Deflation","1.0.0","Silentstorm")]
[assembly:MelonGame("Sayan","Apes vs Helium")]
namespace avhdeflation{
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
            Log("Deflation Loaded!");
        }
        [HarmonyPatch(typeof(Currency),"Start")]
        public class CurrencyStart_Patch{
            [HarmonyPrefix]
            public static bool Prefix(ref Currency __instance){
		        if(GameManagerScript.instance.difficultyLevel==GameManagerScript.DifficultyLevel.easy){
			        __instance.UpdateCurrency(10000);
			        return false;
		        }
		        if(GameManagerScript.instance.difficultyLevel==GameManagerScript.DifficultyLevel.medium){
                    __instance.UpdateCurrency(15000);
			        return false;
		        }
		        if(GameManagerScript.instance.difficultyLevel==GameManagerScript.DifficultyLevel.hard){
			        __instance.UpdateCurrency(20000);
			        return false;
		        }
		        if(GameManagerScript.instance.difficultyLevel==GameManagerScript.DifficultyLevel.impoppable){
			        __instance.UpdateCurrency(40000);
                    return false;
		        }
                return true;
            }
        }
        [HarmonyPatch(typeof(Currency),"UpdateCurrency")]
        public class CurrencyUpdateCurrency_Patch{
            public static bool PrefixRan=false;
            [HarmonyPrefix]
            public static void Prefix(ref int amount){
                if(PrefixRan==true){
                    amount=0;
                }
                PrefixRan=true;
            }
        }
    }
}
