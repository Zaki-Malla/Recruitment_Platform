using Microsoft.EntityFrameworkCore;
using Recruitment_Platform.Models;

namespace Recruitment_Platform.Repository
{
    public class OpportunitiesRepository : IOpportunitiesRepository
    {
        private readonly rpContext _rpContext;
        public OpportunitiesRepository (rpContext rpContext)
        { _rpContext = rpContext; }
        public void AddNewOpportunities(OpportunitiesOfferedModel NewOffer)
        {
            _rpContext.Add(NewOffer);
            _rpContext.SaveChanges();
        }
        public List<OpportunitiesOfferedModel> GetLastThree(int UserId)
        {
            return _rpContext.TbOpportunitiesOffered
                      .Where(o => o.PublisherId == UserId)
                      .OrderByDescending(o => o.PublishDate) 
                      .Take(3)
                      .ToList();
        }
        public List<OpportunitiesOfferedModel> GetLastThreeActive()
        {
            return _rpContext.TbOpportunitiesOffered
                .Where(o=> o.Status == "Active")
                      .OrderByDescending(o => o.PublishDate) 
                      .Take(3)
                      .ToList();
        }
        public List<OpportunitiesOfferedModel> LoadMore(int offset)
        {
            return _rpContext.TbOpportunitiesOffered
                .Where(o => o.Status == "Active")
                .OrderByDescending(o => o.PublishDate)
                .Skip(offset)
                .Take(7)
                .ToList();
        }

        public List<OpportunitiesOfferedModel> GetAllActive()
        {
            return _rpContext.TbOpportunitiesOffered
                .Where(o => o.Status == "Active")
                            .OrderByDescending(o => o.PublishDate)
                      .ToList();
        }
        public List<OpportunitiesOfferedModel> GetAll(int UserId)
        {
            return _rpContext.TbOpportunitiesOffered
                      .Where(o => o.PublisherId == UserId)
                      .ToList();
        }
        public int OffersNumber(int OpportunitiesId)
        {
            return _rpContext.TbOffersForOpportunities.Where(x => x.OpportunityId == OpportunitiesId).Count();
        }
        public OpportunitiesOfferedModel GetOne(int OppoId)
        {
            return _rpContext.TbOpportunitiesOffered.FirstOrDefault(x => x.Id == OppoId);
        }
        public void CloseOpportunity(int OpportunitiesId)
        {
           OpportunitiesOfferedModel Oppo = _rpContext.TbOpportunitiesOffered.Find(OpportunitiesId);
            Oppo.Status = "Closed";
            _rpContext.Update(Oppo);
            _rpContext.SaveChanges();
        }
        public void ActiveOpportunity(int OpportunitiesId)
        {
            OpportunitiesOfferedModel Oppo = _rpContext.TbOpportunitiesOffered.Find(OpportunitiesId);
            Oppo.Status = "Active";
            _rpContext.Update(Oppo);
            _rpContext.SaveChanges();
        }
        public int GetActiveOpportunitiesCountByUser(int userId)
        {
            return _rpContext.TbOpportunitiesOffered
                .Where(o => o.PublisherId == userId && o.Status == "Active")
                .Count();
        }
    }
}
