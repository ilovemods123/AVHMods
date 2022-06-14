namespace CustomUpgrades{
    //You really shouldn't inherit from this class, use CustomClass or CustomSupport
    //This class is only public so the compiler can stfu about access modifiers
    public abstract class CustomUpgradeInternal{
        public virtual string Name=>GetType().Name;
        public virtual int Cost=>0;
        public virtual byte[]Bundle=>null;
        public virtual string Icon=>null;
        public virtual void OnStart(){}
        public virtual void OnUpdate(){}
        public virtual void OnEnd(){}
    }
    public abstract partial class CustomClass:CustomUpgradeInternal{
        public virtual void PreStart(UpgradeButtonScript ubs){}
    }
    public abstract partial class CustomSupport:CustomUpgradeInternal{
    }
    public abstract class CustomUpgradeMod:MelonMod{}
}
