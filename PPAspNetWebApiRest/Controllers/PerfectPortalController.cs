using System;
using System.Net;
using System.Web.Http;
using System.ServiceModel.Description;
using System.Web.Configuration;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;

namespace PPAspNetWebApiRest.Controllers
{
    public class PerfectPortalController : ApiController
    {
        // POST: api/PerfectPortal
        public IHttpActionResult Post([FromBody] object json)
        {
            var value = JsonConvert.DeserializeObject<Models.PerfectPortal>(json.ToString());

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var uri = WebConfigurationManager.AppSettings["uri"];
                Uri oUri = new Uri(uri);
                ClientCredentials clientCredentials = new ClientCredentials();
                clientCredentials.UserName.UserName = WebConfigurationManager.AppSettings["username"];
                clientCredentials.UserName.Password = WebConfigurationManager.AppSettings["password"];

                OrganizationServiceProxy _serviceProxy = new OrganizationServiceProxy(oUri, null, clientCredentials, null);
                _serviceProxy.EnableProxyTypes();

                OrganizationServiceContext orgContext = new OrganizationServiceContext(_serviceProxy);

                Entity pp = new Entity("mvs_perfectportaldatacapture");
                pp["mvs_name"] = value.case_id.ToString();

                pp["mvs_guid"] = value.guid;
                pp["mvs_casetype"] = value.case_type;
                pp["mvs_casetypeguid"] = value.case_type_guid;
                pp["mvs_parentcasetype"] = value.parent_case_type;
                pp["mvs_parentcasetypeguid"] = value.parent_case_type_guid;
                pp["mvs_casestatus"] = value.case_status;
                pp["mvs_instructionnotes"] = value.instruction_notes;
                pp["mvs_feescalename"] = value.fee_scale_name;

                pp["mvs_solicitoruserguid"] = value.solicitor.user_guid;
                pp["mvs_solicitorforename"] = value.solicitor.forename;
                pp["mvs_solicitorsurname"] = value.solicitor.surname.Replace("&#39;","'");
                pp["mvs_solicitorbranchguid"] = value.solicitor.branch_guid;
                pp["mvs_solicitorbranchname"] = value.solicitor.branch_name;
                pp["mvs_solicitorcompanyguid"] = value.solicitor.company_guid;
                pp["mvs_solicitorcompanyname"] = value.solicitor.company_name;

                pp["mvs_referreruserguid"] = value.referrer.user_guid;
                pp["mvs_referrerforename"] = value.referrer.forename;
                pp["mvs_referrersurname"] = value.referrer.surname.Replace("&#39;", "'"); ;
                pp["mvs_referrerbranchguid"] = value.referrer.branch_guid;
                pp["mvs_referrerbranchname"] = value.referrer.branch_name;
                pp["mvs_referrercompanyguid"] = value.referrer.company_guid;
                pp["mvs_referrercompanyname"] = value.referrer.company_name;

                pp["mvs_leadsource"] = value.case_details.lead_source;
                pp["mvs_propertyprice"] = value.case_details.property_price;
                pp["mvs_applicants"] = value.case_details.applicants;
                pp["mvs_tenure"] = value.case_details.tenure;
                pp["mvs_propertyaddress"] = value.case_details.property_address;

                pp["mvs_propertysubbuilding"] = value.case_details.property.sub_building;
                pp["mvs_propertybuildingname"] = value.case_details.property.building_name;
                pp["mvs_propertybuildingnumber"] = value.case_details.property.building_number;
                pp["mvs_propertythoroughfare"] = value.case_details.property.thoroughfare;
                pp["mvs_propertydependantlocality"] = value.case_details.property.dependant_locality;
                pp["mvs_propertytown"] = value.case_details.property.town;
                pp["mvs_propertycounty"] = value.case_details.property.county;
                pp["mvs_propertypostcode"] = value.case_details.property.postcode;
                pp["mvs_propertycountry"] = value.case_details.property.country;

                Guid _ppId = _serviceProxy.Create(pp);

                ColumnSet Columns = new ColumnSet("mvs_name");

                Entity ret = _serviceProxy.Retrieve("mvs_perfectportaldatacapture", _ppId, Columns);

                if (value.case_contacts.Count > 0)
                {
                    foreach (var ppc in value.case_contacts)
                    {
                        Entity c = new Entity("mvs_perfectportalcontact");
                        c["mvs_name"] = ppc.forename + " " + ppc.surname.Replace("&#39;", "'");
                        c["mvs_title"] = ppc.title;
                        c["mvs_forename"] = ppc.forename;
                        c["mvs_surname"] = ppc.surname.Replace("&#39;", "'"); ;
                        c["mvs_contacttypeid"] = ppc.contact_type_id;
                        c["mvs_contacttype"] = ppc.contact_type;
                        c["mvs_companyname"] = ppc.company_name;
                        c["mvs_subbuilding"] = ppc.correspondence_address.sub_building;
                        c["mvs_buildingname"] = ppc.correspondence_address.building_name;
                        c["mvs_buildingnumber"] = ppc.correspondence_address.building_number;
                        c["mvs_thoroughfare"] = ppc.correspondence_address.thoroughfare;
                        c["mvs_dependantlocality"] = ppc.correspondence_address.dependant_locality;
                        c["mvs_town"] = ppc.correspondence_address.town;
                        c["mvs_county"] = ppc.correspondence_address.county;
                        c["mvs_postcode"] = ppc.correspondence_address.postcode;
                        c["mvs_country"] = ppc.correspondence_address.country;
                        c["mvs_phone"] = ppc.phone;
                        c["mvs_email"] = ppc.email;
                        c["mvs_dob"] = ppc.dob;
                        c["mvs_dobtimestamp"] = ppc.dob_timestamp;
                        c["mvs_existingclient"] = ppc.existing_client;
                        c["mvs_ppdatacaptureid"] = new EntityReference("mvs_perfectportaldatacapture", _ppId);

                        Guid _contactId = _serviceProxy.Create(c);
                    }
                }

                if (value.case_fees.Count > 0)
                {
                    foreach (var fee in value.case_fees)
                    {
                        Entity f = new Entity("mvs_perfectportalfee");
                        f["mvs_name"] = fee.name;
                        f["mvs_casefeeid"] = fee.case_fee_id;
                        f["mvs_feecategoryid"] = fee.fee_category_id;
                        f["mvs_feecategoryname"] = fee.fee_category_name;
                        f["mvs_amount"] = fee.amount;
                        f["mvs_vat"] = fee.vat;
                        f["mvs_ppdatacaptureid"] = new EntityReference("mvs_perfectportaldatacapture", _ppId);

                        Guid _feeId = _serviceProxy.Create(f);
                    }
                }

                return Ok(new { Message = "OK", ID = ret["mvs_name"] });
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, new { Message = "An error has occurred.", ID = value is null ? null : value.case_id is null ? null : value.case_id, ExceptionMessage = e.Message, ExceptionType = e.GetType().ToString(), StackTrace = e.StackTrace });
            }
        }
        }
}
