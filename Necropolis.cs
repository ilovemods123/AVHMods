namespace AVHWarMod {
    public class Necropolis:CustomUpgrade{
        public override string Name=>"Necropolis";
        public override int Cost=>2350;
        public override byte[]Bundle=>Bundles.Bundles.necropolis;
        public override string Icon=>"NecropolisIcon";
        public override string Type=>"Support";
        public override void Start(){
            AssetBundle bundle=AssetBundle.LoadFromMemory(Bundle);
            GameObject necropolis=(GameObject)uObject.Instantiate(bundle.LoadAsset("NecropolisPrefab"));
            necropolis.transform.position=new Vector3(0,150,0);
            necropolis.transform.parent=uObject.FindObjectOfType<BlinkingScript>().gameObject.transform;
            InteractScript.instance.audioSource.PlayOneShot((AudioClip)bundle.LoadAsset("NecropolisSound"+new System.Random().Next(1,4)));
            bundle.Unload(false);
        }
        public override void Update(){
            
        }
        public override void End(){
        }
    }
}
