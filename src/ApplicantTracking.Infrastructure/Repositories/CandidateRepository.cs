using ApplicantTracking.Domain.Interfaces;
using ApplicantTracking.Domain.Models;
using ApplicantTracking.Infrastructure.Context;

namespace ApplicantTracking.Infrastructure.Repositories
{
    public class CandidateRepository : Repository<Candidate>, ICandidateRepository
    {
        public CandidateRepository(AppDbContext db) : base(db) { }
    }
}
