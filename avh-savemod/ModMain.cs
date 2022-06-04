using MelonLoader;
using UnityEngine;
using System.Collections.Generic;
[assembly:MelonInfo(typeof(avh_savemod.ModMain),"SaveMod","1.0.0","Silentstorm")]
[assembly:MelonGame("Sayan","Apes vs Helium")]
namespace avh_savemod{
    public class ModMain:MelonMod{
        private static MelonLogger.Instance mllog=new MelonLogger.Instance("SaveMod");
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
        //PlayerHealth, Currency, vector3 position
        public static List<object>saveData=new List<object>(){typeof(PlayerHealth),typeof(Currency),typeof(Vector3)};
        public override void OnUpdate(){
            if(Input.GetKey(KeyCode.RightShift)){
                //saving
                if(Input.GetKeyDown(KeyCode.I)){
                    saveData[0]=PlayerHealth.instance;
                    saveData[1]=Currency.instance;
                    saveData[2]=PlayerHealth.instance.gameObject.transform.position;
                }
                //loading
                if(Input.GetKeyDown(KeyCode.O)&&saveData[0]!=null){
                    PlayerHealth.instance=(PlayerHealth)saveData[0];
                    Currency.instance=(Currency)saveData[1];
                    Log(PlayerHealth.instance.gameObject.name+" "+PlayerHealth.instance.gameObject.transform.position.x+" "+(Vector3)saveData[2]);
                    PlayerHealth.instance.gameObject.transform.position=(Vector3)saveData[2];
                    Log(PlayerHealth.instance.gameObject.name+" "+PlayerHealth.instance.gameObject.transform.position.x+" "+(Vector3)saveData[2]);
                }
            }
        }
    }
}
