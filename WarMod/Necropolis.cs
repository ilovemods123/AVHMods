namespace AVHWarMod {
    public class Necropolis:CustomUpgrade{
        public override string Name=>"Necropolis";
        public override int Cost=>2350;
        public override byte[]Bundle=>Bundles.Bundles.necropolis;
        public override string Icon=>"NecropolisIcon";
        public override string Type=>"Support";
        public GameObject NecropolisProjectile;
        public override void Start(){
            AssetBundle bundle=AssetBundle.LoadFromMemory(Bundle);
            GameObject necropolis=(GameObject)Instantiate(bundle.LoadAsset("NecropolisPrefab"),uObject.FindObjectOfType<BlinkingScript>().gameObject.transform);
            NecropolisProjectile=(GameObject)Instantiate(bundle.LoadAsset("NecropolisGhostProjectilePrefab"),necropolis.transform);
            NecropolisProjectile.SetActive(false);
            necropolis.AddComponent<CustomWeapon>();
            necropolis.GetComponent<CustomWeapon>().
            necropolis.transform.position=new Vector3(0,150,0);
            InteractScript.instance.audioSource.PlayOneShot((AudioClip)bundle.LoadAsset("NecropolisSound"+new System.Random().Next(1,4)));
            bundle.Unload(false);
        }
    }
}