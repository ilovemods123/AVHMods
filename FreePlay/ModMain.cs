using MelonLoader;
using HarmonyLib;
using System.Reflection;
using MarchingBytes;
using System.Linq;
using System.Collections.Generic;
using uObject=UnityEngine.Object;
[assembly:MelonInfo(typeof(FreePlay.ModMain),"FreePlay","1.0.0","Silentstorm")]
[assembly:MelonGame("Sayan","Apes vs Helium")]
namespace FreePlay{
    public class ModMain:MelonMod{
        public static int round=0;
        public static List<UnityEngine.GameObject>bloonList=new List<UnityEngine.GameObject>();
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
            Log("FreePlay loaded!");
        }
        [HarmonyPatch(typeof(WaveSpawner),"Start")]
        public class WaveSpawnerStart_Patch{
            [HarmonyPostfix]
            public static void Postfix(){
                foreach(PoolInfo poolInfo in uObject.FindObjectOfType<EasyObjectPool>().poolInfo){
                    if(poolInfo.prefab.GetComponent<Enemy>()!=null||poolInfo.prefab.GetComponent<MoabScript>()!=null){
                        string name=poolInfo.prefab.name;
                        if(name.Contains("Ceremic")||name.Contains("Moab")||name.Contains("BAD")||name.Contains("BFB")||name.Contains("DDT")||name.Contains("ZOMG")||
                            name.Contains("Lead")||name.Contains("Purple")){
                            bloonList.Add(poolInfo.prefab);
                        }
                    }
                }
            }
        }
        [HarmonyPatch(typeof(WaveSpawner),"WaveCompleted")]
        public class WaveSpawnerWaveCompleted_Patch{
            [HarmonyPrefix]
            public static bool Prefix(ref WaveSpawner __instance){
                __instance.SetPrivateValue("state",WaveSpawner.SpawnState.COUNTING);
		        __instance.SetPrivateValue("waveCountDown",__instance.timeBetweenWaves);
		        Currency.instance.UpdateCurrency(101+__instance.nextwave,true);
		        __instance.WaveFinishedEvent.Invoke();
                __instance.nextwave++;
                round=__instance.nextwave;
                if(__instance.waves.Length<=__instance.nextwave){
                    WaveSpawner.Wave wave=new WaveSpawner.Wave();
                    wave.name=__instance.nextwave.ToString();
                    List<WaveSpawner.EnemySpawned>enemies=new List<WaveSpawner.EnemySpawned>();
                    for(int i=0;i<5;i++){
                        WaveSpawner.EnemySpawned enemySpawned=new WaveSpawner.EnemySpawned();
                        int amount=new System.Random(enemies.Count()+(int)UnityEngine.Time.realtimeSinceStartup).Next(20,__instance.nextwave+1);
                        enemySpawned.enemySpawned=bloonList[new System.Random(enemies.Count()+1).Next(0,bloonList.Count+1)].transform;
                        if(enemySpawned.enemySpawned.GetComponent<MoabScript>()!=null){
                            enemySpawned.count=amount/2;
                        }else{
                            enemySpawned.count=amount*4;
                        }
                        enemies.Add(enemySpawned);
                    }
                    wave.delay=0.2f;
                    wave.enemies=enemies.ToArray();
                    List<WaveSpawner.Wave>waves=__instance.waves.ToList();
                    waves.Add(wave);
                    __instance.waves=waves.ToArray();
                }
                return false;
            }
        }
        [HarmonyPatch(typeof(Enemy),"Start")]
        public class EnemyStart_Patch{
            [HarmonyPostfix]
            public static void Postfix(Enemy __instance){
                if(round>=100){
                    __instance.health+=(int)(round*0.007f);
                    __instance.nav.speed=__instance.nav.speed+(round*0.001f);
                }
            }
        }
    }
    public static class thing{
        public static void SetPrivateValue<T>(this T type,string field,object value){
            type.GetType().GetField(field,BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Public).SetValue(type,value);
        }
    }
}
