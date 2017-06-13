using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AssistServiceService" in code, svc and config file together.
public class AssistServiceService : IAssistServiceService
{

    Community_AssistEntities db = new Community_AssistEntities();

    public int Login(string email, string password)
    {
        int key = 0;
        
        
        int result = db.usp_Login(email, password);

        if(result != -1)
        {
            var userkey = (from k in db.People
                           where k.PersonEmail.Equals(email)
                           select k.PersonKey).FirstOrDefault();

            key = (int)userkey;
        }

        return key;
    }

    public bool RegisterPerson(string lastname, string firstname, string email, string password, string aptnumber, string street, string city, string state, string zipcode, string homephone, string workphone)
    {

        bool result = true;

        db.usp_Register(lastname, firstname, email, password, aptnumber, street, city, state, zipcode, homephone, workphone);

        return result;
    }

    public int RequestGrant(int granttype, string requestexplanation, decimal amount, int userkey)
    {
        
        int result = db.usp_AddRequest(granttype, requestexplanation, amount, userkey);
        return result;
    }

    public List<GrantRequestInfo> ViewGrants(int userkey)
    {
        

        var grantRequests = from rqs in db.GrantRequests
                            where rqs.PersonKey == userkey
                     select rqs;

        List<GrantRequestInfo> requestsInfo = new List<GrantRequestInfo>();
        foreach(var rq in grantRequests)
        {
                     

                GrantRequestInfo grqst = new GrantRequestInfo();
                grqst.GrantRequestAmount = (decimal)rq.GrantRequestAmount;
                grqst.GrantRequestDate = rq.GrantRequestDate;
                grqst.GrantRequestExplanation = rq.GrantRequestExplanation;
                

                grqst.GrantReviews = new List<GrantReview>();

                foreach (var review in rq.GrantReviews)
                {
                    GrantReview grv = new GrantReview();
                    grv.GrantRequestStatus = review.GrantRequestStatus;
                    grv.GrantAllocationAmount = review.GrantAllocationAmount;
                    grv.GrantReviewDate = review.GrantReviewDate;
                    

                    grqst.GrantReviews.Add(grv);
                }

                    

                requestsInfo.Add(grqst);
            
        }

        return requestsInfo;
    }
}


