using MelonLoader;
using HarmonyLib;
[assembly:MelonInfo(typeof(DoubleCash.ModMain),"DoubleCash","1.0.1","Silentstorm")]
[assembly:MelonGame("Sayan","Apes vs Helium")]
namespace DoubleCash{
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
            Log("DoubleCash loaded!");
        }
        [HarmonyPatch(typeof(Currency),"UpdateCurrency")]
        public class CurrencyUpdateCurrency_Patch{
            [HarmonyPrefix]
            public static void Prefix(ref int amount){
                if(amount>0){
                    amount=amount*2;
                }
            }
        }
    }
}
