using System;

namespace EasyTicket.SharedResources.Enums {
    public enum WagonType {
        [UzWagonType("П")]
        Economy = 0,

        [UzWagonType("К")]
        Coupe = 1,

        [UzWagonType("С1")]
        IntercityFirstClass = 2,

        [UzWagonType("С2")]
        IntercitySecondClass = 3
    }

    public class UzWagonTypeAttribute : Attribute {
        public UzWagonTypeAttribute(string typeCode) {
            TypeCode = typeCode;
        }

        public string TypeCode { get; private set; }
    }
}