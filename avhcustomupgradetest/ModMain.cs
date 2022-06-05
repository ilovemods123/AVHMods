using MelonLoader;
using HarmonyLib;
using CustomUpgrades;
[assembly:MelonInfo(typeof(avhcustomupgradetest.ModMain),"avhcustomupgradetest","1.0.0","Silentstorm")]
[assembly:MelonGame("Sayan","Apes vs Helium")]
namespace avhcustomupgradetest{
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
            Log("template loaded!");
        }
        public class test:CustomUpgrade{
            public override string Name=>"test";
            public override int Cost=>100;
        }
    }
}
