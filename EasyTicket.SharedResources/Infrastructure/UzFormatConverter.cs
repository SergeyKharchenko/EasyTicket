using System.Reflection;
using EasyTicket.SharedResources.Enums;

namespace EasyTicket.SharedResources.Infrastructure {
    public static class UzFormatConverter {
        public static string Get(WagonType wagonType) {
            MemberInfo[] memberInfos = typeof(WagonType).GetMember(wagonType.ToString());
            object[] attributes = memberInfos[0].GetCustomAttributes(typeof(UzWagonTypeAttribute), false);
            string typeCode = ((UzWagonTypeAttribute) attributes[0]).TypeCode;
            return typeCode;
        }
    }
}