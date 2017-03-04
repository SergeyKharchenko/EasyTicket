﻿using System.Collections.ObjectModel;

namespace EasyTicket.SharedResources.Models.Responses {
    public class StationsResonse {
        public Collection<Station> Stations { get; set; }

        public class Station {
            public int Id { get; set; }
            public string Title { get; set; }

            protected bool Equals(Station other) {
                return Id == other.Id && string.Equals(Title, other.Title);
            }

            public override bool Equals(object obj) {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj.GetType() == GetType() && Equals((Station) obj);
            }

            public override int GetHashCode() {
                unchecked {
                    return Id * 397 ^ (Title?.GetHashCode() ?? 0);
                }
            }
        }
    }
}