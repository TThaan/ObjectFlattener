using System;
using System.Collections;
using System.Collections.Generic;

namespace ObjectFlattener
{
    public static class Extentions
    {
        public static IEnumerable<object> FlattenToObjects(this object obj)
        {
            IEnumerable result;

            if (obj.GetType().IsPrimitive || obj is string)
            {
                yield return obj;
                yield break;
            }
            else if (obj is IEnumerable)
                result = obj as IEnumerable;
            else
                result = obj.FlattenClassToObjects();

            foreach (var item in result.FlattenIEnumerableToObjects())
            {
                yield return item;
            }
        }
        public static IEnumerable<object> FlattenIEnumerableToObjects(this IEnumerable iEnum)
        {
            foreach (var obj in iEnum)
            {
                foreach (var potentialCollectionOrObject in obj.FlattenToObjects())
                {
                    yield return potentialCollectionOrObject;
                }
            }
        }
        public static IEnumerable<object> FlattenTupleToObjects(this IEnumerable iEnum)
        {
            foreach (var obj in iEnum)
            {
                foreach (var potentialCollectionOrObject in obj.FlattenToObjects())
                {
                    yield return potentialCollectionOrObject;
                }
            }
        }
        public static IEnumerable<object> FlattenKeyValuePairToObjects(this IEnumerable iEnum)
        {
            foreach (var obj in iEnum)
            {
                foreach (var potentialCollectionOrObject in obj.FlattenToObjects())
                {
                    yield return potentialCollectionOrObject;
                }
            }
        }
        public static IEnumerable<object> FlattenClassToObjects(this object obj)
        {
            var pis = obj.GetType().GetProperties();

            foreach (var pi in pis)
            {
                var p = pi.GetValue(obj);
                yield return p.FlattenToObjects();
            }
        }

        public static IEnumerable<object> RemoveEveryNthElement(this IEnumerable<object> iEnum)
        {
            throw new NotImplementedException();
        }
    }
}
