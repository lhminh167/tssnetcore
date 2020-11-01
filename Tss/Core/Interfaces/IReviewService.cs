using Tss.Core.Entities;
using System.Threading.Tasks;
using Tss.Core.Models;

namespace Tss.Core.Interfaces
{
    public interface IReviewService
    {
        Task<Review> GetOne(int reviewId);
        Task<Review> Create(ReviewCreateRequest input);
        Task<Review> Update(ReviewUpdateRequest input);
        Task<Review> Delete(int reviewId);
    }
}