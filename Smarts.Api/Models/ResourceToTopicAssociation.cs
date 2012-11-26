using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Smarts.Api.Models
{
    [Table("MapResourceToTopic")]
    public class ResourceToTopicAssociation
    {
        [Key, Column(Order = 0), ForeignKey("Resource")]
        public int ResourceId { get; set; }

        [Key, Column(Order = 1), ForeignKey("Topic")]
        public string Tag { get; set; }

        [ForeignKey("Contributor")]
        public Guid ContributorGuid { get; set; }

        public DateTime Created { get; set; }

        public virtual Resource Resource { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual WebUser Contributor { get; set; }
    }
}