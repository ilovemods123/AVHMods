global using UnityEngine;
global using MelonLoader;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using uObject=UnityEngine.Object;
using UnityEngine.UI;
using System.Linq;
[assembly:MelonInfo(typeof(CustomUpgrades.ModMain),"CustomUpgrades","1.0.0","Silentstorm")]
[assembly:MelonGame("Sayan","Apes vs Helium")]
namespace CustomUpgrades{
    public class ModMain:MelonMod{
        private static MelonLogger.Instance mllog=null;
        public static List<CustomUpgrade>PurchasedUpgrades=new List<CustomUpgrade>();
        public static Dictionary<string,CustomUpgrade>Upgrades=new Dictionary<string,CustomUpgrade>();
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
            Log("CustomUpgrades loaded!");
            foreach(CustomUpgradeMod mod in MelonHandler.Mods.OfType<CustomUpgradeMod>()){
                foreach(var thing in mod.Assembly.GetValidTypes().Where(a=>a.BaseType.Name=="CustomUpgrade")){
                    Upgrades.Add(thing.Name,(CustomUpgrade)Activator.CreateInstance(thing));
                }
            }
        }
        public override void OnUpdate(){
            foreach(CustomUpgrade upgrade in PurchasedUpgrades){
                upgrade.Update();
            }
        }
        [HarmonyPatch(typeof(EquipmentScript),"PurchaseEquipment")]
        public class EquipmentScriptPurchaseEquipment_Patch{
            [HarmonyPrefix]
            public static bool Prefix(string equipmentID){
                if(Upgrades.ContainsKey(equipmentID)){
                    Upgrades[equipmentID].Start();
                    PurchasedUpgrades.Add(Upgrades[equipmentID]);
                    return false;
                }
                return true;
            }
        }
        public override void OnSceneWasInitialized(int buildIndex,string sceneName){
            if(sceneName!="MainMenu"){
                UpgradeButtonScript ubs=Resources.FindObjectsOfTypeAll<UpgradeButtonScript>().First(a=>a.upgradeName=="Monkey Ace");
                foreach(CustomUpgrade customUpgrade in Upgrades.Values){
                    GameObject newubs=uObject.Instantiate(ubs.gameObject);
                    if(customUpgrade.Bundle!=null){
                        AssetBundle bundle=AssetBundle.LoadFromMemory(customUpgrade.Bundle);
                        newubs.GetComponent<Image>().sprite=Sprite.Create((Texture2D)bundle.LoadAsset(customUpgrade.Icon),newubs.GetComponent<Image>().sprite.rect,
                            newubs.GetComponent<Image>().sprite.pivot);
                        bundle.Unload(false);
                    }
                    newubs.GetComponent<UpgradeButtonScript>().upgradeName=customUpgrade.Name;
                    newubs.GetComponent<UpgradeButtonScript>().mediumCost=customUpgrade.Cost;
                    newubs.GetComponent<UpgradeButtonScript>().setNextButtonActive=false;
                    newubs.GetComponent<UpgradeButtonScript>().nextUpgradeButtons=new Button[0];
                    newubs.GetComponent<UpgradeButtonScript>().interactableFromStart=true;
                    newubs.gameObject.transform.parent=ubs.gameObject.transform.parent.gameObject.transform;
                    newubs.transform.localScale=new Vector3(1,1,1);
                    newubs.GetComponent<RectTransform>().position=new Vector3(newubs.GetComponent<RectTransform>().position.x+440,newubs.GetComponent<RectTransform>().position.y,
                        newubs.GetComponent<RectTransform>().position.z);
                    EquipmentScript.instance.PurchaseEquipment(customUpgrade.Name,"CustomUpgrades");
                }
            }
        }
    }
}
