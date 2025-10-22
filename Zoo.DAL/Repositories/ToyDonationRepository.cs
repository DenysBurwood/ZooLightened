using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.DAL.Contexts;
using Zoo.DL.Entities;

namespace Zoo.DAL.Repositories
{
    public class ToyDonationRepository : BaseRepository<ToyDonation>
    {
        private readonly DbSet<ToyDonation> _toyDonations;
        private readonly ZooContext _context;

        public ToyDonationRepository(ZooContext context) : base(context)
        {
            _context = context;
            _toyDonations = context.ToyDonations;
        }

        public decimal GetTotalAmountperToy(int id)
        {
            return _toyDonations.Where(td => td.ToyId == id).Sum(i => i.Amount);
        }
    }
}
