using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CIPlatform.DataAccessLayer.Repository;
public class MissionRatingRepository : Repository<MissionRating>, IMissionRatingRepository
{
    public MissionRatingRepository(CIDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(long count, byte rating)> CalculateAverageMissionRating(long missionId)
    {
        var query = dbSet;

        var volunteerCountQuery = FilterByMissionId(query, missionId);
        var avgRating = await volunteerCountQuery.AverageAsync( missionRating => missionRating.Rating );

        var count = await volunteerCountQuery.LongCountAsync();
        return (count, (byte)Math.Round (avgRating));
    }

    private IQueryable<MissionRating> FilterByMissionId( IQueryable<MissionRating> query, long missionId )
    {
        return query.Where(missionRating => missionRating.MissionId == missionId);
    }
    public void UpdateUserMissionRating(MissionRating missionRating)
    {
        var query = dbSet;
        dbSet.Update( missionRating );
    }
}
