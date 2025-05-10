using System.Reflection;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain
{
    public static class Util
    {
        public static Type[] GetTypesInNamespace(string nameSpace)
        {
            return Assembly.GetExecutingAssembly()
                            .GetTypes()
                            .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                            .ToArray();
        }
    }
}
