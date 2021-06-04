using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TripTravelSystem.Models
{
    public class QuestionsMetaData
    {
        [Display(Name ="Question")]
        [Required(ErrorMessage = "Type Your Qusetion!")]
        public string question1 { get; set; }

        [Display(Name = "Answer")]
        [Required(ErrorMessage = "Answer is required!")]
        public string answer { get; set; }

        [Display(Name = "Question Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Question Date is required!")]
        public System.DateTime questionDate { get; set; }

        [Display(Name = "Agency Name ")]
        [Required(ErrorMessage ="Select the Agency you want to send the Question for it!")]
        public int agencyID { get; set; }
    }

    [MetadataType(typeof(QuestionsMetaData))]
    public partial class Question
    {

    }
}