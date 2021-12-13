using System.Collections.Generic;
using System.Linq;
using PickPointTestApp.Logic.Models;
using PickPointTestApp.DB;
using System.Text.RegularExpressions;

namespace PickPointTestApp.Logic
{
    public class PostomatManager
    {
        public List<PostomatModel> GetPostomats(bool isActive = true)
        {
            using (var dc = new PickPointContext())
            {
                var postomats = dc.Postomats.Where(c => c.IsActive == isActive)
                    .OrderBy(c => c.Number)
                    .Select(c => new PostomatModel
                    {
                        Id = c.Id,
                        Address = c.Address,
                        IsActive = c.IsActive,
                        Number = c.Number
                    }).ToList();

                return postomats;
            }
        }

        public PostomatModel GetPostomat(string number)
        {
            using (var dc = new PickPointContext())
            {
                //TODO make index on postomat number
                var dbPostomat = dc.Postomats.Where(c => c.Number == number).FirstOrDefault();

                if (dbPostomat == null)
                    return null;

                return new PostomatModel
                {
                    Id = dbPostomat.Id,
                    Number = dbPostomat.Number,
                    Address = dbPostomat.Address,
                    IsActive = dbPostomat.IsActive
                };
            }
        }

        public bool CheckPostomatNumber(string number)
        {
            using (var dc = new PickPointContext())
            {
                //format check
                Regex rx = new Regex(@"^[0-9]{4}-[0-9]{3}$",
                             RegexOptions.Compiled | RegexOptions.IgnoreCase);

                // Find matches.
                MatchCollection matches = rx.Matches(number);

                if (matches.Count() != 1)
                {
                    return false;
                }

                var dbPostomat = dc.Postomats.Where(c => c.Number == number).FirstOrDefault();

                if (dbPostomat == null)
                    return true;
                else
                    return false;
            }
        }
    }
}
