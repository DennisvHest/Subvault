using Subvault_Domain.Entities;
using System.Collections.Generic;

namespace Subvault_Domain.APIEntities {

    public class CreditsRoot {

        public int Id { get; set; }
        public List<CastMemberResult> Cast { get; set; }
        public List<CrewMemberResult> Crew { get; set; }
    }
}
