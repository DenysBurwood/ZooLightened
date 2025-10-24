using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Zoo.DL.Entities;
using Zoo.BLL.Exceptions;

namespace Zoo.BLL.Services
{
    public class ToyService
    {
        private readonly ToyRepository _toyRepository;
        private readonly ToyDonationRepository _toyDonationRepository;

        public ToyService(ToyRepository toyRepository, ToyDonationRepository toyDonationRepository)
        {
            _toyRepository = toyRepository;
            _toyDonationRepository = toyDonationRepository;
        }

        public List<Toy> GetAll(int page, int nbPage)
        {
            List<Toy> toys = _toyRepository.GetAll(page, nbPage, null ,includes: "Species");

            toys.ForEach(toy =>
            {
                toy.TotalAmountSoFar = _toyDonationRepository.GetTotalAmountperToy(toy.Id);
            });

            return toys;
        }

        public Toy GetToy(int id)
        {
            return _toyRepository.GetEntityById(id)!;
        }

        public void Donate(ToyDonation donation)
        {
            Toy? toy = GetToy(donation.ToyId);

            if (toy == null)

            {
                throw new ToyNotFoundException($"The toy {donation.ToyId} doesn't exist.");
            }

            if (donation.Amount < toy.MinimumAmountperDonation)
            {
                throw new ToyNotAllowedException($"The minimum donation for the toy {donation.ToyId} is {toy.MinimumAmountperDonation}.");
            }

            toy.TotalAmountSoFar = _toyDonationRepository.GetTotalAmountperToy(toy.Id);

            if (toy.TotalAmountSoFar + donation.Amount > toy.WishedTotalAmount)
            {
                throw new ToyNotAllowedException(@$"The donation {donation.Amount} for the toy {donation.ToyId} would exceed the maximum of {toy.WishedTotalAmount}. The maximum donation for this toy is {toy.WishedTotalAmount - toy.TotalAmountSoFar}");
            }

            _toyDonationRepository.Add(donation);
        }
    }
}
