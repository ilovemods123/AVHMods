namespace AVHWarMod{
    public abstract class CustomUpgrade{
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
        public static List<CustomUpgrade>PurchasedUpgrades=new List<CustomUpgrade>();
    }
}
