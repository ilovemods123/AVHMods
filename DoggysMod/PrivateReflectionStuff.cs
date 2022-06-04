//TY Python#1829 for this, probs would've figured it out myself after like 2 hours tho lmao
using System.Reflection;
namespace DoggysMod{
    public static class ReflectionHelpers{
        public static FieldInfo GetPrivate<T>(this T type,string field){
            return type.GetType().GetField(field,BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Public);
        }
        public static object GetPrivateValue<T>(this T type,string field){
            return type.GetType().GetField(field,BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Public).GetValue(type);
        }
        public static void SetPrivateValue<T>(this T type, string field, object value){
            type.GetType().GetField(field,BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Public).SetValue(type,value);
        }
    }
}