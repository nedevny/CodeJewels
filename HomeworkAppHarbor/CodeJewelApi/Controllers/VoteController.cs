using CodeJewelData;
using CodeJewelModels;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace CodeJewelApi.Controllers
{
    public class VoteController : BaseController
    {
        private const double MinVoteValue = -1;

        [HttpPost]
        [ActionName("addvote")]
        public HttpResponseMessage AddVote(int id, [FromBody] Vote vote)
        {
            var response = PerformOperation(() =>
            {
                var context = new CodeContext();
                using (context)
                {
                    var jewel = context.CodeJewels.FirstOrDefault(j => j.Id == id);
                    if (jewel == null)
                    {
                        throw new InvalidOperationException("The jewel does not exists!");
                    }
                    jewel.Votes.Add(vote);
                    context.Votes.Add(vote);
                    context.SaveChanges();
                    ChechForVeryWeekAvgVote(id, vote, context);

                    return vote;
                }
            });
            return response;
        }

        private void ChechForVeryWeekAvgVote(int id, Vote vote, CodeContext context)
        {
            // var context = new CodeContext();
            var jewel = context.CodeJewels.Include("Votes").FirstOrDefault(j => j.Id == id);
            if (jewel != null)
            {
                double avgVote = 0;
                if (jewel.Votes.Count > 0)
                {
                    avgVote = jewel.Votes.Average(v => v.VoteValue);
                }

                if (avgVote < MinVoteValue)
                {
                    context.Votes.Remove(vote);
                    context.CodeJewels.Remove(jewel);
                    context.SaveChanges();
                    //DbEntityEntry entry = context.Entry(vote);
                    //if (entry.State != EntityState.Deleted)
                    //{
                    //    entry.State = EntityState.Deleted;
                    //}
                    //else
                    //{
                    //    context.Votes.Attach(vote);
                    //    context.Votes.Remove(vote);
                    //}
                    //DbEntityEntry entryJewel = context.Entry(jewel);
                    //if (entryJewel.State != EntityState.Deleted)
                    //{
                    //    entryJewel.State = EntityState.Deleted;
                    //}
                    //else
                    //{
                    //    context.CodeJewels.Attach(jewel);
                    //    context.CodeJewels.Remove(jewel);
                    //}
                    //context.SaveChanges();
                }
            }
            else
            {
                throw new InvalidOperationException("Trying to delete, but didn't find codeJewel");
            }
        }
    }
}