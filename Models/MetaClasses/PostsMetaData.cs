using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TripTravelSystem.Models
{
    public class PostsMetaData
    {
        [Display(Name = "Trip Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Trip Title is Required")]
        [StringLength(100)]
        public string tripTitle { get; set; }

        [Display(Name = "Trip Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage ="Trip Date is Required")]
        public System.DateTime tripDate { get; set; }

        [Display(Name = "Trip Image")]
        [Required(ErrorMessage = "Trip Image is Required.")]
        //[RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        public string tripImage { get; set; }

        [Display(Name = "Trip Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Trip Description  is Required")]
        public string tripDescription { get; set; }

        [Display(Name = "Trip Price")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Trip Price  is Required")]
        public string tripPrice { get; set; }

        [Display(Name = "Post Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Post Date is Required")]
        public System.DateTime postDate { get; set; }

        


    }

    [MetadataType(typeof(PostsMetaData))]
    public partial class Post
    {
        public HttpPostedFileBase Imagepost { get; set; }

    }
}