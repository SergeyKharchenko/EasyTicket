using System;

namespace EasyTicket.SharedResources {
    public enum WagonType {
        [UzWagonType("П")] Economy = 0,
        [UzWagonType("К")] Coupe = 1,
        [UzWagonType("С1")] IntercityFirstClass,
        [UzWagonType("С2")] IntercitySecondClass
    }

    public class UzWagonTypeAttribute : Attribute {
        public UzWagonTypeAttribute(string typeCode) {
            TypeCode = typeCode;
        }

        public string TypeCode { get; private set; }
    }
}