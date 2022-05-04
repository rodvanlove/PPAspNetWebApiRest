using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Web;

namespace PPAspNetWebApiRest.Models
{
    public class PerfectPortal
    {
        public PerfectPortal()
        {
            solicitor = new Solicitor();
            referrer = new Referrer();
            case_contacts = new List<CaseContact>();
            case_details = new CaseDetails();
            case_fees = new List<CaseFee>();
            key_stages = new List<KeyStage>();
        }

        public Int32? case_id { get; set; }
        public String guid { get; set; }
        public String case_type { get; set; }
        public String case_type_guid { get; set; }
        public String parent_case_type { get; set; }
        public String parent_case_type_guid { get; set; }
        public String case_status { get; set; }
        public String instruction_notes { get; set; }
        public String fee_scale_name { get; set; }
        public Solicitor solicitor { get; set; }
        public Referrer referrer { get; set; }
        public List<CaseContact> case_contacts { get; set; }
        public CaseDetails case_details { get; set; }
        public List<CaseFee> case_fees { get; set; }
        public List<KeyStage> key_stages { get; set; }
    }

    public class KeyStage
    {
        public String name { get; set; }
        public String key_stage_static_guid { get; set; }
    }

    public class CaseFee
    {
        public Int32 case_fee_id { get; set; }
        public Int32 fee_category_id { get; set; }
        public String fee_category_name { get; set; }
        public String name { get; set; }
        public String amount { get; set; }
        public String vat { get; set; }
    }

    public class CaseDetails
    {
        public CaseDetails()
        {
            property = new Property();
            questions = new List<Question>();
        }

        public String lead_source { get; set; }
        public String property_price { get; set; }
        public String applicants { get; set; }
        public String tenure { get; set; }
        public String property_address { get; set; }
        public Property property { get; set; }
        public List<Question> questions { get; set; }
    }

    public class Question
    {
        public Question()
        {
            sub_questions = new List<SubQuestion>();
        }

        public String question { get; set; }
        public String answer { get; set; }
        public List<SubQuestion> sub_questions { get; set; }
    }

    public class SubQuestion
    {
        public String question { get; set; }
        public String answer { get; set; }
    }

    public class Property
    {
        public String sub_building { get; set; }
        public String building_name { get; set; }
        public String building_number { get; set; }
        public String thoroughfare { get; set; }
        public String dependant_locality { get; set; }
        public String town { get; set; }
        public String county { get; set; }
        public String postcode { get; set; }
        public String country { get; set; }
    }

    public class CaseContact
    {
        public CaseContact()
        {
            correspondence_address = new CorrespondenceAddress();
        }

        public String title { get; set; }
        public String forename { get; set; }
        public String surname { get; set; }
        public Int32 contact_type_id { get; set; }
        public String contact_type { get; set; }
        public String company_name { get; set; }
        public CorrespondenceAddress correspondence_address { get; set; }
        public String phone { get; set; }
        public String email { get; set; }
        public String dob { get; set; }
        public Int32? dob_timestamp { get; set; }
        public Int32 existing_client { get; set; }
    }

    public class CorrespondenceAddress
    {
        public String sub_building { get; set; }
        public String building_name { get; set; }
        public String building_number { get; set; }
        public String thoroughfare { get; set; }
        public String dependant_locality { get; set; }
        public String town { get; set; }
        public String county { get; set; }
        public String postcode { get; set; }
        public String country { get; set; }
    }

    public class Referrer
    {
        public String user_guid { get; set; }
        public String forename { get; set; }
        public String surname { get; set; }
        public String branch_guid { get; set; }
        public String branch_name { get; set; }
        public String company_guid { get; set; }
        public String company_name { get; set; }
    }

    public class Solicitor
    {
        public String user_guid { get; set; }
        public String forename { get; set; }
        public String surname { get; set; }
        public String branch_guid { get; set; }
        public String branch_name { get; set; }
        public String company_guid { get; set; }
        public String company_name { get; set; }
    }
}