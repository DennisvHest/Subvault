using Subvault_Domain.Entities;
using System.Collections.Generic;

namespace Subvault_Domain.APIEntities {

    public class MovieCreditsRoot {

        public int Id { get; set; }
        public List<CastMember> Cast { get; set; }
        public List<CrewMemberResult> Crew { get; set; }
    }
}
