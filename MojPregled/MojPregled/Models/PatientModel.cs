using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojPregled
{
    public class PatientModel
    {
        public int patient_id { get; set; }
        public string patient_name { get; set; }
        public string patient_surname { get; set; }
        public DateTime patient_dateofbirth { get; set; }
        public string patient_address { get; set; }
        public string patient_phonenumber { get; set; }
        public string patient_city { get; set; }
    }
}