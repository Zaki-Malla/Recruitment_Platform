using Recruitment_Platform.Models;
using Recruitment_Platform.Models.HelperModels;

namespace Recruitment_Platform.Repository
{
    public interface IOffersRepository
    {
        void AddNewOffer(OffersForOpportunitiesModel model);
        List<OffersForOpportunitiesModel> GetLastFourOffers(int EmployerId);
        List<OffersForOpportunitiesModel> GetAllOffers(int EmployerId);

        int GetTotalApplicationsForUserOpportunities(int userId);
        int GetTotalAcceptedApplicationsForUserOpportunities(int userId);
        OfferViewModel GetOfferById(int OfferId);
        OffersForOpportunitiesModel GetOfferByUserAndOpportunity(int userId, int opportunityId);
        void AcceptOffer(int OfferId);
        void RejectOffer(int OfferId);



    }
}
