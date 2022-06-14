using System;
using UnityEngine;
using MelonLoader;
using HarmonyLib;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

[assembly:MelonInfo(typeof(DoggysMod.ModMain),"Doggy's Mod","1.0.0","idfk")]
[assembly:MelonGame("Sayan","Apes vs Helium")]
namespace DoggysMod{
	public class ModMain:MelonMod{
        private static MelonLogger.Instance mllog=new MelonLogger.Instance("Doggy's Mod");
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
            Log("Doggy's Mod Loaded!");
        }
        [HarmonyPatch(typeof(Enemy),"OnTriggerEnter")]
        public class EnemyOnTriggerEnter_Patch{
            [HarmonyPrefix]
            public static bool Prefix(ref Enemy __instance,ref Collider other){
                if(other.tag=="Player"&&!(bool)__instance.GetPrivateValue("dead")){
					if(PlayerHealth.instance.invincible){
						return false;
					}
					PlayerHealth.instance.UpdateHealth(-__instance.damage);
					Vector3 dir=__instance.player.transform.position-__instance.transform.position;
					if(!MyGUIClass.noknockback){
				        __instance.player.GetComponent<KnockbackScript>().AddImpact(dir,__instance.knockBackForce);
			        }
			        __instance.ReceiveDamage(1,"Hit Player",null,false,false);
		        }
                return false;
            }
        }
        [HarmonyPatch(typeof(MenuScript),"OnEnable")]
        public class MenuScriptOnEnable{
            [HarmonyPostfix]
            public static void Postfix(ref MenuScript __instance){
                __instance.gameObject.AddComponent<MyGUIClass>();
            }
        }
        [HarmonyPatch(typeof(FirstPersonController),"GetInput")]
        public class FirstPersonControllerGetInput_Patch{
            [HarmonyPrefix]
            public static bool Prefix(ref FirstPersonController __instance,ref float speed){
                float axis=CrossPlatformInputManager.GetAxis("Horizontal");
	            float axis2=CrossPlatformInputManager.GetAxis("Vertical");
	            bool isWalking=(bool)__instance.GetPrivateValue("m_IsWalking");
	            speed=(bool)__instance.GetPrivateValue("m_IsWalking")?(float)__instance.GetPrivateValue("m_WalkSpeed"):(float)__instance.GetPrivateValue("m_RunSpeed");
                if(MyGUIClass.movementSpeed!=0){
                    speed=MyGUIClass.movementSpeed;
                }
                __instance.SetPrivateValue("m_Input",new Vector2(axis,axis2));
                Vector2 inputVector=(Vector2)__instance.GetPrivateValue("m_Input");
                bool UseFovKick=(bool)__instance.GetPrivateValue("m_UseFovKick");
                FOVKick fovKick=(FOVKick)__instance.GetPrivateValue("m_FovKick");
                CharacterController charController=(CharacterController)__instance.GetPrivateValue("m_CharacterController");
	            if(inputVector.sqrMagnitude>1f){
		            inputVector.Normalize();
	            }
	            if(UseFovKick&&charController.velocity.sqrMagnitude>0f)
	            {
		            __instance.StopAllCoroutines();
		            __instance.StartCoroutine((!isWalking)?fovKick.FOVKickUp():fovKick.FOVKickDown());
	            }
                return false;
            }
        }
    }
}