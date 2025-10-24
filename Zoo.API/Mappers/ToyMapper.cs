using Zoo.API.DTOs;
using Zoo.DL.Entities;

namespace Zoo.API.Mappers
{
    public static class ToyMapper
    {
        public static ToyIndexDto ToToyIndexDto(this Toy toy)
        {
            return new ToyIndexDto()
            {
                Id = toy.Id,
                SpeciesName = toy.Species.Name,
                Name = toy.Name,
                Description = toy.Description,
                ImagePath = toy.ImagePath,
                MinimumAmountperDonation = toy.MinimumAmountperDonation,
                WishedTotalAmount = toy.WishedTotalAmount,
                StartDate = toy.StartDate,
                EndDate = toy.EndDate,
                Status = toy.Status,
                TotalAmountSoFar = toy.TotalAmountSoFar,
            };
        }

        public static ToyDonation ToToyDonation(this ToyDonationFormDto form)
        {
            return new ToyDonation()
            {
                ToyId = form.ToyId,
                UserId = form.UserId,
                DonationDate = form.DonationDate,
                Amount = form.Amount,
            };
        }
    }
}
