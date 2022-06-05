namespace CustomUpgrades{
    public abstract class CustomUpgrade:MonoBehaviour{
        public virtual string Name=>"";
        public virtual int Cost=>int.MaxValue;
        public virtual byte[]Bundle=>null;
        public virtual string Icon=>null;
        public virtual string Type=>"";
        public virtual void Start(){
        }
        public virtual void Update(){
        }
        public virtual void End(){
        }
    }
    public abstract class CustomUpgradeMod:MelonMod{
    }
}
