using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ONTO.Models.ONTOModels
{
    public class UserSettings
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public int LocalizationID { get; set; }
    }
}