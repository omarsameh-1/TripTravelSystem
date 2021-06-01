using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TripTravelSystem.Models
{
    public class SavedPostsMetaData
    {

        public int USERid { get; set; }
        public int POSTid { get; set; }

    }

    [MetadataType(typeof(SavedPostsMetaData))]
    public partial class SavedPost
    {

    }
}