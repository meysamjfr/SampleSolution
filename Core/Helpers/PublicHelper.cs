using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Project.Helpers
{
    public static class PublicHelper
    {
        public const string SessionCaptcha = "_Captcha";

        public const string ApiUriForSwagger = "api/[controller]/[action]";
        public static string DefaultAvatar => "/uploads/avatars/default.png";

        private static readonly Random random = new((int)DateTime.Now.Ticks);

        public static int GetRandomInt(int length = 5)
        {
            int from = 1, to = 9;
            for (int i = 0; i < length; i++)
            {
                from += (i * 10);
                to += (i * 10);
            }
            return random.Next(from, to);
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static string GetDisplayName<T>(this T value)
        {
            Type type = typeof(T);
            MemberInfo info = value
                .GetType()
                .GetMember(value.ToString())
                .First();

            if (info != null && info.CustomAttributes.Any())
            {
                DisplayAttribute nameAttr = info.GetCustomAttribute<DisplayAttribute>();
                return nameAttr != null ? nameAttr.Name : value.ToString();
            }

            return value.ToString();

        }

        public static string GetDisplayAttributeFrom(this Enum enumValue)
        {
            MemberInfo info = enumValue
                .GetType()
                .GetMember(enumValue.ToString())
                .First();
            if (info != null && info.CustomAttributes.Any())
            {
                DisplayAttribute nameAttr = info.GetCustomAttribute<DisplayAttribute>();
                return nameAttr != null ? nameAttr.Name : enumValue.ToString();
            }
            return enumValue.ToString();
        }
    }
}