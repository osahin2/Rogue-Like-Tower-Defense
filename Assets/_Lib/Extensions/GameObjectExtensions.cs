using UnityEngine;

namespace GameObjectExtension
{
    public static class GameObjectExtensions
    {
        public static T OrNull<T> (this T obj) where T : Object => obj ? obj : null;

        public static bool IsNull<T> (this T obj) where T : Object => obj ? false : true;
    }
}