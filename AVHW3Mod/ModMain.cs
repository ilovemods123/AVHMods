global using System;
global using MelonLoader;
global using System.Linq;
global using uObject=UnityEngine.Object;
global using HarmonyLib;
global using UnityEngine;
global using System.Reflection;
global using System.Collections.Generic;
global using static AVHWarMod.ModMain;
global using UnityEngine.UI;
global using AVHWarMod;
[assembly:MelonInfo(typeof(ModMain),"AVHWarMod","1.0.0","Silentstorm")]
[assembly:MelonGame("Sayan","Apes vs Helium")]
namespace AVHWarMod{
    public class ModMain:MelonMod{
        private static MelonLogger.Instance mllog=new MelonLogger.Instance("AVHWarMod");
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
        public static Dictionary<string,CustomUpgrade>Upgrades=new Dictionary<string,CustomUpgrade>();
        public static string name;
        public override void OnApplicationStart(){
            foreach(var thing in Assembly.GetTypes()){
                if(thing.BaseType==typeof(CustomUpgrade)){
                    Upgrades.Add(thing.Name,(CustomUpgrade)Activator.CreateInstance(thing));
                }
            }
        }
        public override void OnUpdate(){
            foreach(CustomUpgrade upgrade in CustomUpgrade.PurchasedUpgrades){
                if(GetMethodInfo(upgrade.Update).GetMethodBody().GetILAsByteArray().Count()!=1){
                    upgrade.Update();
                }
            }
        }
        //https://stackoverflow.com/questions/9469612/how-to-get-methodinfo-from-a-method-symbol
        public static MethodInfo GetMethodInfo(Delegate d){
            return d.Method;
        }
        [HarmonyPatch(typeof(EquipmentScript),"PurchaseEquipment")]
        public class EquipmentScriptPurchaseEquipment_Patch{
            [HarmonyPrefix]
            public static bool Prefix(string equipmentID){
                if(Upgrades.ContainsKey(equipmentID)){
                    if(GetMethodInfo(Upgrades[equipmentID].Start).GetMethodBody().GetILAsByteArray().Count()!=1){
                        Upgrades[equipmentID].Start();
                    }
                    CustomUpgrade.PurchasedUpgrades.Add(Upgrades[equipmentID]);
                    return false;
                }
                return true;
            }
        }
        public override void OnSceneWasInitialized(int buildIndex,string sceneName){
            if(sceneName!="MainMenu"){
                Currency.instance.UpdateCurrency(99999);
                foreach(UpgradeButtonScript ubs in Resources.FindObjectsOfTypeAll<UpgradeButtonScript>()){
                    if(ubs.upgradeName=="Monkey Ace"){
                        foreach(var customUpgrade in Upgrades){
                            if(customUpgrade.Value.Bundle!=null&&customUpgrade.Value.Icon!=null&&customUpgrade.Value.Cost!=int.MaxValue&&customUpgrade.Value.Type!=""){
                                GameObject newubs=uObject.Instantiate(ubs.gameObject);
                                AssetBundle bundle=AssetBundle.LoadFromMemory(customUpgrade.Value.Bundle);
                                newubs.GetComponent<Image>().sprite=Sprite.Create((Texture2D)bundle.LoadAsset(customUpgrade.Value.Icon),newubs.GetComponent<Image>().sprite.rect,
                                    newubs.GetComponent<Image>().sprite.pivot);
                                newubs.GetComponent<UpgradeButtonScript>().upgradeName=customUpgrade.Value.Name;
                                newubs.GetComponent<UpgradeButtonScript>().mediumCost=customUpgrade.Value.Cost;
                                newubs.GetComponent<UpgradeButtonScript>().setNextButtonActive=false;
                                newubs.GetComponent<UpgradeButtonScript>().nextUpgradeButtons=new Button[0];
                                newubs.GetComponent<UpgradeButtonScript>().interactableFromStart=true;
                                newubs.gameObject.transform.parent=ubs.gameObject.transform.parent.gameObject.transform;
                                newubs.transform.localScale=new Vector3(1,1,1);
                                newubs.GetComponent<RectTransform>().position=new Vector3(newubs.GetComponent<RectTransform>().position.x+440,newubs.GetComponent<RectTransform>().position.y,
                                    newubs.GetComponent<RectTransform>().position.z);
                                bundle.Unload(false);
                                EquipmentScript.instance.PurchaseEquipment(customUpgrade.Value.Name,"cock");
                            }
                        }
                        break;
                    }
                }
            }
        }
        [HarmonyPatch(typeof(FollowerWeaponScript),nameof(FollowerWeaponScript.AttackTarget))]
        public class test{
            [HarmonyPostfix]
            public static void Postfix(){
                Log("test");
            }
        }
    }
}