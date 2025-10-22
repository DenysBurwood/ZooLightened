
using Zoo.DAL.Repositories;
using Zoo.DL.Entities;

namespace Zoo.BLL.Services
{
    public class AddressService
    {
        private readonly AddressRepository _addressRepository;

        public AddressService(AddressRepository addressRepository) 
        {
            _addressRepository = addressRepository;
        }

        public void CreateAddress(Address address) 
        {
            _addressRepository.Add(address);
        }
    }
}
