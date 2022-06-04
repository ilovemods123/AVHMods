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
        public class test{
            [HarmonyPrefix]
            public static bool Prefix(Currency __instance){
		        if(GameManagerScript.instance.difficultyLevel == GameManagerScript.DifficultyLevel.easy){
			        __instance.currency = 10000;
			        this.currencyText.text = "$" + this.currency.ToString();
			        return false;
		        }
		        if (this.gameManagerScript.difficultyLevel == GameManagerScript.DifficultyLevel.medium)
		        {
			        this.currency = 15000;
			        this.currencyText.text = "$" + this.currency.ToString();
			        return false;
		        }
		        if (this.gameManagerScript.difficultyLevel == GameManagerScript.DifficultyLevel.hard)
		        {
			        this.currency = 20000;
			        this.currencyText.text = "$" + this.currency.ToString();
			        return false;
		        }
		        if (this.gameManagerScript.difficultyLevel == GameManagerScript.DifficultyLevel.impoppable)
		        {
			        this.currency = 40000;
			        this.currencyText.text = "$" + this.currency.ToString();
                    return false;
		        }
                return true;
            }
        }
    }
}
