using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobby
{
    public static class ICollectionExtentions
    {
        public static List<TOutput> ConvertAll<Tin, TOutput>(this ICollection<Tin> collection, Converter<Tin, TOutput> converter)
        {
            if (collection == null)
            {
                return null;
            }

            List<TOutput> result = new List<TOutput>();
                        
            foreach (var item in collection)
            {
                TOutput convertedItem = converter(item);
                result.Add(convertedItem);
            }

            return result;
        }
    }
}