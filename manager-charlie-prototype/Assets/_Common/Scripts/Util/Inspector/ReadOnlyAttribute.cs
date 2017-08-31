namespace Util.Inspector
{
    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
    public class ReadOnlyAttribute : UnityEngine.PropertyAttribute
    {
    }
}