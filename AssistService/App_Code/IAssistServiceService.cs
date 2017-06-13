using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAssistServiceService" in both code and config file together.
[ServiceContract]
public interface IAssistServiceService
{
    [OperationContract]
    bool RegisterPerson(string lastname, string firstname, string email, string password, string aptnumber, string street, string city, string state, string zipcode, string homephone, string workphone);

    [OperationContract]
    int Login(string email, string password);

    [OperationContract]
    int RequestGrant(int granttype, string requestexplanation, decimal amount, int userkey);

    [OperationContract]
    List<GrantRequestInfo> ViewGrants(int userkey);


}

[DataContract]
public class GrantRequestInfo
{
    [DataMember]
    public DateTime GrantRequestDate { get; set; }
    [DataMember]
    public string GrantRequestType { get; set; }
    [DataMember]
    public string GrantRequestExplanation { get; set; }
    [DataMember]
    public decimal GrantRequestAmount { get; set; }
    [DataMember]
    public List<GrantReview> GrantReviews { get; set; }

}

