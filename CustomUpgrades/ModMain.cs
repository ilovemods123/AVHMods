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
        public static List<CustomUpgradeInternal>PurchasedUpgrades=new List<CustomUpgradeInternal>();
        public static Dictionary<string,CustomUpgradeInternal>Upgrades=new Dictionary<string,CustomUpgradeInternal>();
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
                foreach(var thing in mod.Assembly.GetValidTypes().Where(a=>a.BaseType.BaseType!=null&&a.BaseType.BaseType.Name=="CustomUpgradeInternal")){
                    Upgrades.Add(thing.Name,(CustomUpgradeInternal)Activator.CreateInstance(thing));
                }
            }
        }
        public override void OnUpdate(){
            foreach(CustomUpgradeInternal upgrade in PurchasedUpgrades){
                upgrade.OnUpdate();
            }
        }
        [HarmonyPatch(typeof(EquipmentScript),"PurchaseEquipment")]
        public class EquipmentScriptPurchaseEquipment_Patch{
            [HarmonyPrefix]
            public static bool Prefix(string equipmentID){
                if(Upgrades.ContainsKey(equipmentID)){
                    Upgrades[equipmentID].OnStart();
                    PurchasedUpgrades.Add(Upgrades[equipmentID]);
                    return false;
                }
                return true;
            }
        }
        public override void OnSceneWasInitialized(int buildIndex,string sceneName){
            if(sceneName!="MainMenu"){
                foreach(CustomUpgradeInternal customUpgrade in Upgrades.Values){
                    GridLayoutGroup grid=null;
                    Log(customUpgrade.GetType().BaseType.Name);
                    switch(customUpgrade.GetType().BaseType.Name){
                        case"CustomSupport":
                            grid=Resources.FindObjectsOfTypeAll<GridLayoutGroup>().First(a=>a.name=="SupportButtons");
                            UpgradeButtonScript newubs=uObject.Instantiate(grid.GetComponentInChildren<UpgradeButtonScript>().gameObject,grid.transform).GetComponent<UpgradeButtonScript>();
                            newubs.setNextButtonActive=false;
                            newubs.nextUpgradeButtons=new Button[0];
                            newubs.interactableFromStart=true;
                            newubs.upgradeName=customUpgrade.Name;
                            newubs.mediumCost=customUpgrade.Cost;
                            if(customUpgrade.Bundle!=null&&customUpgrade.Icon!=null){
                                AssetBundle bundle=AssetBundle.LoadFromMemory(customUpgrade.Bundle);
                                newubs.GetComponent<Image>().sprite=Sprite.Create((Texture2D)bundle.LoadAsset(customUpgrade.Icon),newubs.GetComponent<Image>().sprite.rect,
                                    newubs.GetComponent<Image>().sprite.pivot);
                                bundle.Unload(false);
                            }
                            break;
                        case"CustomClass":
                            CustomClass customClass=(CustomClass)customUpgrade;
                            RectTransform[]rectArray=Resources.FindObjectsOfTypeAll<RectTransform>();
                            GameObject upgradeMenu=rectArray.First(a=>a.name=="Upgrades Menu").gameObject;
                            //upgradepath
                            UpgradePathScript newUpgradePath=uObject.Instantiate(Resources.FindObjectsOfTypeAll<UpgradePathScript>()[0],upgradeMenu.transform);
                            newUpgradePath.name=customClass.Name+" Upgrades";
                            uObject.Destroy(newUpgradePath.GetComponentsInChildren<RectTransform>().First(a=>a.name=="Basic Path").gameObject);
                            uObject.Destroy(newUpgradePath.GetComponentsInChildren<RectTransform>().First(a=>a.name=="Paths").gameObject);
                            //upgradebuttongrid
                            GameObject newUpgradeButtonGridObject=uObject.Instantiate(new GameObject("CustomUpgradePath"),newUpgradePath.GetComponent<RectTransform>());
                            newUpgradeButtonGridObject.AddComponent<RectTransform>();
                            newUpgradeButtonGridObject.transform.position=new Vector3(200,640,0);
                            GridLayoutGroup newUpgradeButtonGrid=newUpgradeButtonGridObject.AddComponent<GridLayoutGroup>();
                            newUpgradeButtonGrid.cellSize=new Vector2(300,300);
                            newUpgradeButtonGrid.spacing=new Vector2(100,0);
                            newUpgradeButtonGrid.constraintCount=5;
                            newUpgradeButtonGrid.constraint=GridLayoutGroup.Constraint.FixedColumnCount;
                            //upgradebutton
                            UpgradeButtonScript newUpgradeButton=uObject.Instantiate(Resources.FindObjectsOfTypeAll<UpgradeButtonScript>()[0].gameObject,newUpgradeButtonGrid.transform).
                                GetComponent<UpgradeButtonScript>();
                            newUpgradeButton.nameText.text=customClass.Name;
                            newUpgradeButton.name=customClass.Name+" Button";
                            newUpgradeButton.costText.text=customClass.Cost.ToString();
                            newUpgradeButton.mediumCost=customClass.Cost;
                            newUpgradeButton.upgradeName=customClass.Name;
                            newUpgradeButton.interactableFromStart=true;
                            newUpgradeButton.GetComponent<Button>().interactable=true;
                            //newUpgradeButton.GetComponent<Image>().color=new(255,255,255,255);
                            newUpgradeButton.GetComponent<Button>().onClick.SetPersistentListenerState(0,UnityEngine.Events.UnityEventCallState.Off);
                            newUpgradeButton.GetComponent<Button>().onClick.AddListener(()=>newUpgradeButton.PurchaseUpgrade());
                            //classbutton
                            GameObject classButtons=rectArray.First(a=>a.name=="ClassButtons").gameObject;
                            Button newClassButton=uObject.Instantiate(classButtons.GetComponentsInChildren<Button>()[0],classButtons.transform);
                            if(customClass.Bundle!=null&&customClass.Icon!=null){
                                AssetBundle bundle=AssetBundle.LoadFromMemory(customClass.Bundle);
                                Texture2D texture=(Texture2D)bundle.LoadAsset(customClass.Icon);
                                newClassButton.GetComponent<Image>().canvasRenderer.SetTexture(texture);
                                newClassButton.GetComponent<Image>().sprite=Sprite.Create(texture,new(0,0,texture.width,texture.height),new());
                                newUpgradeButton.GetComponent<Image>().canvasRenderer.SetTexture(texture);
                                newUpgradeButton.GetComponent<Image>().sprite=Sprite.Create(texture,new(0,0,texture.width,texture.height),new());
                                bundle.Unload(false);
                            }
                            newClassButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text=customUpgrade.Name.ToUpper();
                            newClassButton.onClick.AddListener(()=>newUpgradePath.gameObject.SetActive(true));
                            newClassButton.onClick.AddListener(()=>classButtons.transform.parent.gameObject.SetActive(false));
                            newClassButton.onClick.SetPersistentListenerState(0,UnityEngine.Events.UnityEventCallState.Off);
                            newClassButton.onClick.SetPersistentListenerState(1,UnityEngine.Events.UnityEventCallState.Off);
                            //upgradepathbackbutton
                            Button newUpgradePathBackButton=newUpgradePath.GetComponentsInChildren<Button>().First(a=>a.name=="BackButton");
                            newUpgradePathBackButton.GetComponent<BackButtonScript>().backButton=newUpgradePathBackButton;
                            newUpgradePathBackButton.onClick.RemoveAllListeners();
                            newUpgradePathBackButton.onClick.AddListener(()=>newUpgradePath.gameObject.SetActive(false));
                            newUpgradePathBackButton.onClick.AddListener(()=>classButtons.transform.parent.gameObject.SetActive(true));
                            newUpgradePathBackButton.onClick.SetPersistentListenerState(0,UnityEngine.Events.UnityEventCallState.Off);
                            newUpgradePathBackButton.onClick.SetPersistentListenerState(1,UnityEngine.Events.UnityEventCallState.Off);
                            customClass.PreStart(newUpgradeButton);
                            break;
                    }
                    //EquipmentScript.instance.PurchaseEquipment(customUpgrade.Name,"CustomUpgrades");
                }
            }
        }
    }
}
