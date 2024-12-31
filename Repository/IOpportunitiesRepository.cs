using Recruitment_Platform.Models;

namespace Recruitment_Platform.Repository
{
    public interface IOpportunitiesRepository
    {
        void AddNewOpportunities(OpportunitiesOfferedModel NewOffer);
        List<OpportunitiesOfferedModel> GetLastThree(int UserId);
        List<OpportunitiesOfferedModel> GetLastThreeActive();
        List<OpportunitiesOfferedModel> GetAllActive();
        List<OpportunitiesOfferedModel> LoadMore(int offset);

        List<OpportunitiesOfferedModel> GetAll(int UserId);
        int OffersNumber(int OpportunitiesId);
        OpportunitiesOfferedModel GetOne(int OppoId);
        void CloseOpportunity(int OpportunitiesId);
        void ActiveOpportunity(int OpportunitiesId);
        int GetActiveOpportunitiesCountByUser(int userId);


    }
}
