using MelonLoader;
using HarmonyLib;
[assembly:MelonInfo(typeof(avhtemplate.ModMain),"avhtemplate","1.0.0","Silentstorm")]
[assembly:MelonGame("Sayan","Apes vs Helium")]
namespace avhtemplate{
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
            Log("template loaded!");
        }
    }
}
