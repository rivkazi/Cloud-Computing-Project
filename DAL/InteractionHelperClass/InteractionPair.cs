using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.InteractionHelperClass
{
    public class InteractionPair
    {
        public List<InteractionConcept> interactionConcept { get; set; }
        public string severity { get; set; }
        public string description { get; set; }
    }
}
